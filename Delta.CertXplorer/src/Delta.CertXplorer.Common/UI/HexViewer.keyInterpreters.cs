using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;

namespace Delta.CertXplorer.UI
{
    partial class HexViewer
    {
        /// <summary>
        /// Defines a user input handler such as for mouse and keyboard input
        /// </summary>
        private interface IKeyInterpreter
        {
            /// <summary>
            /// Activates mouse events
            /// </summary>
            void Activate();

            /// <summary>
            /// Deactivate mouse events
            /// </summary>
            void Deactivate();

            /// <summary>
            /// Preprocesses WM_KEYUP window message.
            /// </summary>
            /// <param name="m">the Message object to process.</param>
            /// <returns>True, if the message was processed.</returns>
            bool PreProcessWmKeyUp(ref Message m);

            /// <summary>
            /// Preprocesses WM_CHAR window message.
            /// </summary>
            /// <param name="m">the Message object to process.</param>
            /// <returns>True, if the message was processed.</returns>
            bool PreProcessWmChar(ref Message m);

            /// <summary>
            /// Preprocesses WM_KEYDOWN window message.
            /// </summary>
            /// <param name="m">the Message object to process.</param>
            /// <returns>True, if the message was processed.</returns>
            bool PreProcessWmKeyDown(ref Message m);

            /// <summary>
            /// Gives some information about where to place the caret.
            /// </summary>
            /// <param name="byteIndex">the index of the byte</param>
            /// <returns>the position where the caret is to place.</returns>
            PointF GetCaretPointF(long byteIndex);
        }

        /// <summary>
        /// Represents an empty input handler without any functionality. 
        /// If is set ByteProvider to null, then this interpreter is used.
        /// </summary>
        private class EmptyKeyInterpreter : IKeyInterpreter
        {
            private HexViewer hexViewer = null;

            /// <summary>
            /// Initializes a new instance of the <see cref="EmptyKeyInterpreter"/> class.
            /// </summary>
            /// <param name="owner">The owner.</param>
            public EmptyKeyInterpreter(HexViewer owner)
            {
                if (owner == null) throw new ArgumentNullException("owner");
                hexViewer = owner;
            }

            #region IKeyInterpreter Members

            /// <summary>
            /// Activates mouse events
            /// </summary>
            public void Activate() { }

            /// <summary>
            /// Deactivate mouse events
            /// </summary>
            public void Deactivate() { }

            /// <summary>
            /// Preprocesses WM_KEYUP window message.
            /// </summary>
            /// <param name="m">the Message object to process.</param>
            /// <returns>True, if the message was processed.</returns>
            public bool PreProcessWmKeyUp(ref Message m)
            {
                return hexViewer.BasePreProcessMessage(ref m);
            }

            /// <summary>
            /// Preprocesses WM_CHAR window message.
            /// </summary>
            /// <param name="m">the Message object to process.</param>
            /// <returns>True, if the message was processed.</returns>
            public bool PreProcessWmChar(ref Message m)
            {
                return hexViewer.BasePreProcessMessage(ref m);
            }

            /// <summary>
            /// Preprocesses WM_KEYDOWN window message.
            /// </summary>
            /// <param name="m">the Message object to process.</param>
            /// <returns>True, if the message was processed.</returns>
            public bool PreProcessWmKeyDown(ref Message m)
            {
                return hexViewer.BasePreProcessMessage(ref m);
            }

            /// <summary>
            /// Gives some information about where to place the caret.
            /// </summary>
            /// <param name="byteIndex">the index of the byte</param>
            /// <returns>
            /// the position where the caret is to place.
            /// </returns>
            public PointF GetCaretPointF(long byteIndex)
            {
                return new PointF();
            }

            #endregion
        }

        /// <summary>
        /// Handles user input such as mouse and keyboard input during hex view edit
        /// </summary>
        private class KeyInterpreter : IKeyInterpreter
        {
            #region Fields

            /// <summary>
            /// Contains the parent HexBox control
            /// </summary>
            protected HexViewer HexViewer;

            /// <summary>
            /// Contains True, if shift key is down
            /// </summary>
            protected bool ShiftDown;

            /// <summary>
            /// Contains True, if mouse is down
            /// </summary>
            private bool mouseDown;

            /// <summary>
            /// Contains the selection start position info
            /// </summary>
            private BytePositionInfo startPositionInfo;

            /// <summary>
            /// Contains the current mouse selection position info
            /// </summary>
            private BytePositionInfo currentPositionInfo;

            #endregion

            #region Construction

            /// <summary>
            /// Initializes a new instance of the <see cref="KeyInterpreter"/> class.
            /// </summary>
            /// <param name="owner">The owner.</param>
            public KeyInterpreter(HexViewer owner)
            {
                if (owner == null) throw new ArgumentNullException("owner");
                HexViewer = owner;
            }

            #endregion

            #region Activate, Deactive methods

            /// <summary>
            /// Activates mouse events
            /// </summary>
            public virtual void Activate()
            {
                HexViewer.MouseDown += new MouseEventHandler(BeginMouseSelection);
                HexViewer.MouseMove += new MouseEventHandler(UpdateMouseSelection);
                HexViewer.MouseUp += new MouseEventHandler(EndMouseSelection);
            }

            /// <summary>
            /// Deactivate mouse events
            /// </summary>
            public virtual void Deactivate()
            {
                HexViewer.MouseDown -= new MouseEventHandler(BeginMouseSelection);
                HexViewer.MouseMove -= new MouseEventHandler(UpdateMouseSelection);
                HexViewer.MouseUp -= new MouseEventHandler(EndMouseSelection);
            }

            #endregion

            #region Mouse selection methods

            /// <summary>
            /// Begins the mouse selection.
            /// </summary>
            /// <param name="sender">The sender.</param>
            /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
            private void BeginMouseSelection(object sender, MouseEventArgs e)
            {
                System.Diagnostics.Debug.WriteLine("BeginMouseSelection()", "KeyInterpreter");

                mouseDown = true;

                if (!ShiftDown)
                {
                    startPositionInfo = new BytePositionInfo(HexViewer.bytePosition, HexViewer.byteCharacterPosition);
                    HexViewer.ReleaseSelection();
                }
                else
                {
                    UpdateMouseSelection(this, e);
                }
            }

            /// <summary>
            /// Updates the mouse selection.
            /// </summary>
            /// <param name="sender">The sender.</param>
            /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
            private void UpdateMouseSelection(object sender, MouseEventArgs e)
            {
                if (!mouseDown)
                    return;

                currentPositionInfo = GetBytePositionInfo(new Point(e.X, e.Y));
                long selEnd = currentPositionInfo.Index;
                long realselStart;
                long realselLength;

                if (selEnd < startPositionInfo.Index)
                {
                    realselStart = selEnd;
                    realselLength = startPositionInfo.Index - selEnd;
                }
                else if (selEnd > startPositionInfo.Index)
                {
                    realselStart = startPositionInfo.Index;
                    realselLength = selEnd - realselStart;
                }
                else
                {
                    realselStart = HexViewer.bytePosition;
                    realselLength = 0;
                }

                if (realselStart != HexViewer.bytePosition || realselLength != HexViewer._selectionLength)
                    HexViewer.InternalSelect(realselStart, realselLength);
            }

            /// <summary>
            /// Ends the mouse selection.
            /// </summary>
            /// <param name="sender">The sender.</param>
            /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
            private void EndMouseSelection(object sender, MouseEventArgs e)
            {
                mouseDown = false;
            }

            #endregion

            #region PreProcessWmKeyDown methods

            /// <summary>
            /// Pres the process wm key down.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            public virtual bool PreProcessWmKeyDown(ref Message m)
            {
                System.Diagnostics.Debug.WriteLine("PreProcessWmKeyDown(ref Message m)", "KeyInterpreter");

                Keys vc = (Keys)m.WParam.ToInt32();

                Keys keyData = vc | Control.ModifierKeys;

                switch (keyData)
                {
                    case Keys.Left:
                    case Keys.Up:
                    case Keys.Right:
                    case Keys.Down:
                    case Keys.PageUp:
                    case Keys.PageDown:
                    case Keys.Left | Keys.Shift:
                    case Keys.Up | Keys.Shift:
                    case Keys.Right | Keys.Shift:
                    case Keys.Down | Keys.Shift:
                    case Keys.Tab:
                    case Keys.Back:
                    case Keys.Delete:
                    case Keys.Home:
                    case Keys.End:
                    case Keys.ShiftKey | Keys.Shift:
                    case Keys.C | Keys.Control:
                    case Keys.X | Keys.Control:
                    case Keys.V | Keys.Control:
                        if (RaiseKeyDown(keyData))
                            return true;
                        break;
                }

                switch (keyData)
                {
                    case Keys.Left:						// move left
                        return PreProcessWmKeyDown_Left(ref m);
                    case Keys.Up:						// move up
                        return PreProcessWmKeyDown_Up(ref m);
                    case Keys.Right:					// move right
                        return PreProcessWmKeyDown_Right(ref m);
                    case Keys.Down:						// move down
                        return PreProcessWmKeyDown_Down(ref m);
                    case Keys.PageUp:					// move pageup
                        return PreProcessWmKeyDown_PageUp(ref m);
                    case Keys.PageDown:					// move pagedown
                        return PreProcessWmKeyDown_PageDown(ref m);
                    case Keys.Left | Keys.Shift:		// move left with selection
                        return PreProcessWmKeyDown_ShiftLeft(ref m);
                    case Keys.Up | Keys.Shift:			// move up with selection
                        return PreProcessWmKeyDown_ShiftUp(ref m);
                    case Keys.Right | Keys.Shift:		// move right with selection
                        return PreProcessWmKeyDown_ShiftRight(ref m);
                    case Keys.Down | Keys.Shift:		// move down with selection
                        return PreProcessWmKeyDown_ShiftDown(ref m);
                    case Keys.Tab:						// switch focus to string view
                        return PreProcessWmKeyDown_Tab(ref m);
                    case Keys.Back:						// back
                        return PreProcessWmKeyDown_Back(ref m);
                    case Keys.Delete:					// delete
                        return PreProcessWmKeyDown_Delete(ref m);
                    case Keys.Home:						// move to home
                        return PreProcessWmKeyDown_Home(ref m);
                    case Keys.End:						// move to end
                        return PreProcessWmKeyDown_End(ref m);
                    case Keys.ShiftKey | Keys.Shift:	// begin selection process
                        return PreProcessWmKeyDown_ShiftShiftKey(ref m);
                    case Keys.C | Keys.Control:			// copy
                        return PreProcessWmKeyDown_ControlC(ref m);
                    case Keys.X | Keys.Control:			// cut
                        return PreProcessWmKeyDown_ControlX(ref m);
                    case Keys.V | Keys.Control:			// paste
                        return PreProcessWmKeyDown_ControlV(ref m);
                    default:
                        HexViewer.ScrollByteIntoView();
                        return HexViewer.BasePreProcessMessage(ref m);
                }
            }

            /// <summary>
            /// Raises a key down event.
            /// </summary>
            /// <param name="keyData">The key data.</param>
            /// <returns></returns>
            protected bool RaiseKeyDown(Keys keyData)
            {
                KeyEventArgs e = new KeyEventArgs(keyData);
                HexViewer.OnKeyDown(e);
                return e.Handled;
            }

            /// <summary>
            /// Pres the process wm key down_ left.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            protected virtual bool PreProcessWmKeyDown_Left(ref Message m)
            {
                return PerformPosMoveLeft();
            }

            /// <summary>
            /// Pres the process wm key down_ up.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            protected virtual bool PreProcessWmKeyDown_Up(ref Message m)
            {
                long pos = HexViewer.bytePosition;
                int cp = HexViewer.byteCharacterPosition;

                if (!(pos == 0 && cp == 0))
                {
                    pos = Math.Max(-1, pos - HexViewer.hexMaxHBytes);
                    if (pos == -1)
                        return true;

                    HexViewer.SetPosition(pos);

                    if (pos < HexViewer.startByte)
                    {
                        HexViewer.PerformScrollLineUp();
                    }

                    HexViewer.UpdateCaret();
                    HexViewer.Invalidate();
                }

                HexViewer.ScrollByteIntoView();
                HexViewer.ReleaseSelection();

                return true;
            }

            /// <summary>
            /// Pres the process wm key down_ right.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            protected virtual bool PreProcessWmKeyDown_Right(ref Message m)
            {
                return PerformPosMoveRight();
            }

            /// <summary>
            /// Pres the process wm key down_ down.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            protected virtual bool PreProcessWmKeyDown_Down(ref Message m)
            {
                long pos = HexViewer.bytePosition;
                int cp = HexViewer.byteCharacterPosition;

                if (pos == HexViewer._byteProvider.Length && cp == 0)
                    return true;

                pos = Math.Min(HexViewer._byteProvider.Length, pos + HexViewer.hexMaxHBytes);

                if (pos == HexViewer._byteProvider.Length)
                    cp = 0;

                HexViewer.SetPosition(pos, cp);

                if (pos > HexViewer.endByte - 1)
                {
                    HexViewer.PerformScrollLineDown();
                }

                HexViewer.UpdateCaret();
                HexViewer.ScrollByteIntoView();
                HexViewer.ReleaseSelection();
                HexViewer.Invalidate();

                return true;
            }

            /// <summary>
            /// Pres the process wm key down_ page up.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            protected virtual bool PreProcessWmKeyDown_PageUp(ref Message m)
            {
                long pos = HexViewer.bytePosition;
                int cp = HexViewer.byteCharacterPosition;

                if (pos == 0 && cp == 0)
                    return true;

                pos = Math.Max(0, pos - HexViewer.hexMaxBytes);
                if (pos == 0)
                    return true;

                HexViewer.SetPosition(pos);

                if (pos < HexViewer.startByte)
                {
                    HexViewer.PerformScrollPageUp();
                }

                HexViewer.ReleaseSelection();
                HexViewer.UpdateCaret();
                HexViewer.Invalidate();
                return true;
            }

            /// <summary>
            /// Pres the process wm key down_ page down.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            protected virtual bool PreProcessWmKeyDown_PageDown(ref Message m)
            {
                long pos = HexViewer.bytePosition;
                int cp = HexViewer.byteCharacterPosition;

                if (pos == HexViewer._byteProvider.Length && cp == 0)
                    return true;

                pos = Math.Min(HexViewer._byteProvider.Length, pos + HexViewer.hexMaxBytes);

                if (pos == HexViewer._byteProvider.Length)
                    cp = 0;

                HexViewer.SetPosition(pos, cp);

                if (pos > HexViewer.endByte - 1)
                {
                    HexViewer.PerformScrollPageDown();
                }

                HexViewer.ReleaseSelection();
                HexViewer.UpdateCaret();
                HexViewer.Invalidate();

                return true;
            }

            /// <summary>
            /// Pres the process wm key down_ shift left.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            protected virtual bool PreProcessWmKeyDown_ShiftLeft(ref Message m)
            {
                long pos = HexViewer.bytePosition;
                long sel = HexViewer._selectionLength;

                if (pos + sel < 1)
                    return true;

                if (pos + sel <= startPositionInfo.Index)
                {
                    if (pos == 0)
                        return true;

                    pos--;
                    sel++;
                }
                else
                {
                    sel = Math.Max(0, sel - 1);
                }

                HexViewer.ScrollByteIntoView();
                HexViewer.InternalSelect(pos, sel);

                return true;
            }

            /// <summary>
            /// Pres the process wm key down_ shift up.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            protected virtual bool PreProcessWmKeyDown_ShiftUp(ref Message m)
            {
                long pos = HexViewer.bytePosition;
                long sel = HexViewer._selectionLength;

                if (pos - HexViewer.hexMaxHBytes < 0 && pos <= startPositionInfo.Index)
                    return true;

                if (startPositionInfo.Index >= pos + sel)
                {
                    pos = pos - HexViewer.hexMaxHBytes;
                    sel += HexViewer.hexMaxHBytes;
                    HexViewer.InternalSelect(pos, sel);
                    HexViewer.ScrollByteIntoView();
                }
                else
                {
                    sel -= HexViewer.hexMaxHBytes;
                    if (sel < 0)
                    {
                        pos = startPositionInfo.Index + sel;
                        sel = -sel;
                        HexViewer.InternalSelect(pos, sel);
                        HexViewer.ScrollByteIntoView();
                    }
                    else
                    {
                        sel -= HexViewer.hexMaxHBytes;
                        HexViewer.InternalSelect(pos, sel);
                        HexViewer.ScrollByteIntoView(pos + sel);
                    }
                }

                return true;
            }

            /// <summary>
            /// Pres the process wm key down_ shift right.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            protected virtual bool PreProcessWmKeyDown_ShiftRight(ref Message m)
            {
                long pos = HexViewer.bytePosition;
                long sel = HexViewer._selectionLength;

                if (pos + sel >= HexViewer._byteProvider.Length)
                    return true;

                if (startPositionInfo.Index <= pos)
                {
                    sel++;
                    HexViewer.InternalSelect(pos, sel);
                    HexViewer.ScrollByteIntoView(pos + sel);
                }
                else
                {
                    pos++;
                    sel = Math.Max(0, sel - 1);
                    HexViewer.InternalSelect(pos, sel);
                    HexViewer.ScrollByteIntoView();
                }

                return true;
            }

            /// <summary>
            /// Pres the process wm key down_ shift down.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            protected virtual bool PreProcessWmKeyDown_ShiftDown(ref Message m)
            {
                long pos = HexViewer.bytePosition;
                long sel = HexViewer._selectionLength;

                long max = HexViewer._byteProvider.Length;

                if (pos + sel + HexViewer.hexMaxHBytes > max)
                    return true;

                if (startPositionInfo.Index <= pos)
                {
                    sel += HexViewer.hexMaxHBytes;
                    HexViewer.InternalSelect(pos, sel);
                    HexViewer.ScrollByteIntoView(pos + sel);
                }
                else
                {
                    sel -= HexViewer.hexMaxHBytes;
                    if (sel < 0)
                    {
                        pos = startPositionInfo.Index;
                        sel = -sel;
                    }
                    else
                    {
                        pos += HexViewer.hexMaxHBytes;
                        sel -= HexViewer.hexMaxHBytes;
                    }

                    HexViewer.InternalSelect(pos, sel);
                    HexViewer.ScrollByteIntoView();
                }

                return true;
            }

            /// <summary>
            /// Pres the process wm key down_ tab.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            protected virtual bool PreProcessWmKeyDown_Tab(ref Message m)
            {
                if (HexViewer._stringViewVisible && HexViewer.currentKeyInterpreter.GetType() == typeof(KeyInterpreter))
                {
                    HexViewer.ActivateStringKeyInterpreter();
                    HexViewer.ScrollByteIntoView();
                    HexViewer.ReleaseSelection();
                    HexViewer.UpdateCaret();
                    HexViewer.Invalidate();
                    return true;
                }

                if (HexViewer.Parent == null) return true;
                HexViewer.Parent.SelectNextControl(HexViewer, true, true, true, true);
                return true;
            }

            /// <summary>
            /// Pres the process wm key down_ shift tab.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            protected virtual bool PreProcessWmKeyDown_ShiftTab(ref Message m)
            {
                if (HexViewer.currentKeyInterpreter is StringKeyInterpreter)
                {
                    ShiftDown = false;
                    HexViewer.ActivateKeyInterpreter();
                    HexViewer.ScrollByteIntoView();
                    HexViewer.ReleaseSelection();
                    HexViewer.UpdateCaret();
                    HexViewer.Invalidate();
                    return true;
                }

                if (HexViewer.Parent == null) return true;
                HexViewer.Parent.SelectNextControl(HexViewer, false, true, true, true);
                return true;
            }

            /// <summary>
            /// Pres the process wm key down_ back.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            protected virtual bool PreProcessWmKeyDown_Back(ref Message m)
            {
                if (!HexViewer._byteProvider.SupportsDeleteBytes)
                    return true;

                long pos = HexViewer.bytePosition;
                long sel = HexViewer._selectionLength;
                int cp = HexViewer.byteCharacterPosition;

                long startDelete = (cp == 0 && sel == 0) ? pos - 1 : pos;
                if (startDelete < 0 && sel < 1)
                    return true;

                long bytesToDelete = (sel > 0) ? sel : 1;
                HexViewer._byteProvider.DeleteBytes(Math.Max(0, startDelete), bytesToDelete);
                HexViewer.UpdateScrollSize();

                if (sel == 0)
                    PerformPosMoveLeftByte();

                HexViewer.ReleaseSelection();
                HexViewer.Invalidate();

                return true;
            }

            /// <summary>
            /// Pres the process wm key down_ delete.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            protected virtual bool PreProcessWmKeyDown_Delete(ref Message m)
            {
                if (!HexViewer._byteProvider.SupportsDeleteBytes)
                    return true;

                long pos = HexViewer.bytePosition;
                long sel = HexViewer._selectionLength;

                if (pos >= HexViewer._byteProvider.Length)
                    return true;

                long bytesToDelete = (sel > 0) ? sel : 1;
                HexViewer._byteProvider.DeleteBytes(pos, bytesToDelete);

                HexViewer.UpdateScrollSize();
                HexViewer.ReleaseSelection();
                HexViewer.Invalidate();

                return true;
            }

            /// <summary>
            /// Pres the process wm key down_ home.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            protected virtual bool PreProcessWmKeyDown_Home(ref Message m)
            {
                long pos = HexViewer.bytePosition;
                int cp = HexViewer.byteCharacterPosition;

                if (pos < 1)
                    return true;

                pos = 0;
                cp = 0;
                HexViewer.SetPosition(pos, cp);

                HexViewer.ScrollByteIntoView();
                HexViewer.UpdateCaret();
                HexViewer.ReleaseSelection();

                return true;
            }

            /// <summary>
            /// Pres the process wm key down_ end.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            protected virtual bool PreProcessWmKeyDown_End(ref Message m)
            {
                long pos = HexViewer.bytePosition;
                int cp = HexViewer.byteCharacterPosition;

                if (pos >= HexViewer._byteProvider.Length - 1)
                    return true;

                pos = HexViewer._byteProvider.Length;
                cp = 0;
                HexViewer.SetPosition(pos, cp);

                HexViewer.ScrollByteIntoView();
                HexViewer.UpdateCaret();
                HexViewer.ReleaseSelection();

                return true;
            }

            /// <summary>
            /// Pres the process wm key down_ shift shift key.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            protected virtual bool PreProcessWmKeyDown_ShiftShiftKey(ref Message m)
            {
                if (mouseDown)
                    return true;
                if (ShiftDown)
                    return true;

                ShiftDown = true;

                if (HexViewer._selectionLength > 0)
                    return true;

                startPositionInfo = new BytePositionInfo(HexViewer.bytePosition, HexViewer.byteCharacterPosition);

                return true;
            }

            /// <summary>
            /// Pres the process wm key down_ control C.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            protected virtual bool PreProcessWmKeyDown_ControlC(ref Message m)
            {
                HexViewer.Copy();
                return true;
            }

            /// <summary>
            /// Pres the process wm key down_ control X.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            protected virtual bool PreProcessWmKeyDown_ControlX(ref Message m)
            {
                HexViewer.Cut();
                return true;
            }

            /// <summary>
            /// Pres the process wm key down_ control V.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            protected virtual bool PreProcessWmKeyDown_ControlV(ref Message m)
            {
                HexViewer.Paste();
                return true;
            }

            #endregion

            #region PreProcessWmChar methods

            /// <summary>
            /// Pres the process wm char.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            public virtual bool PreProcessWmChar(ref Message m)
            {
                if (Control.ModifierKeys == Keys.Control)
                {
                    return HexViewer.BasePreProcessMessage(ref m);
                }

                bool sw = HexViewer._byteProvider.SupportsWriteByte;
                bool si = HexViewer._byteProvider.SupportsInsertBytes;
                bool sd = HexViewer._byteProvider.SupportsDeleteBytes;

                long pos = HexViewer.bytePosition;
                long sel = HexViewer._selectionLength;
                int cp = HexViewer.byteCharacterPosition;

                if (
                    (!sw && pos != HexViewer._byteProvider.Length) ||
                    (!si && pos == HexViewer._byteProvider.Length))
                {
                    return HexViewer.BasePreProcessMessage(ref m);
                }

                char c = (char)m.WParam.ToInt32();

                if (Uri.IsHexDigit(c))
                {
                    if (RaiseKeyPress(c)) return true;

                    if (HexViewer.ReadOnly) return true;

                    bool isInsertMode = (pos == HexViewer._byteProvider.Length);

                    // do insert when insertActive = true
                    if (!isInsertMode && si && HexViewer._insertActive && cp == 0)
                        isInsertMode = true;

                    if (sd && si && sel > 0)
                    {
                        HexViewer._byteProvider.DeleteBytes(pos, sel);
                        isInsertMode = true;
                        cp = 0;
                        HexViewer.SetPosition(pos, cp);
                    }

                    HexViewer.ReleaseSelection();

                    byte currentByte = 0;
                    if (!isInsertMode) currentByte = HexViewer._byteProvider.ReadByte(pos);

                    string sCb = currentByte.ToString("X", System.Threading.Thread.CurrentThread.CurrentCulture);
                    if (sCb.Length == 1) sCb = "0" + sCb;

                    string sNewCb = c.ToString();
                    if (cp == 0) sNewCb += sCb.Substring(1, 1);
                    else sNewCb = sCb.Substring(0, 1) + sNewCb;

                    byte newcb = byte.Parse(sNewCb,
                        NumberStyles.AllowHexSpecifier, Thread.CurrentThread.CurrentCulture);

                    if (isInsertMode)
                        HexViewer._byteProvider.InsertBytes(pos, new byte[] { newcb });
                    else HexViewer._byteProvider.WriteByte(pos, newcb);

                    PerformPosMoveRight();

                    HexViewer.Invalidate();
                    return true;
                }
                else return HexViewer.BasePreProcessMessage(ref m);
            }

            /// <summary>
            /// Raises the key press.
            /// </summary>
            /// <param name="keyChar">The key char.</param>
            /// <returns></returns>
            protected bool RaiseKeyPress(char keyChar)
            {
                KeyPressEventArgs e = new KeyPressEventArgs(keyChar);
                HexViewer.OnKeyPress(e);
                return e.Handled;
            }

            #endregion

            #region PreProcessWmKeyUp methods

            /// <summary>
            /// Pres the process wm key up.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            public virtual bool PreProcessWmKeyUp(ref Message m)
            {
                System.Diagnostics.Debug.WriteLine("PreProcessWmKeyUp(ref Message m)", "KeyInterpreter");

                Keys vc = (Keys)m.WParam.ToInt32();
                Keys keyData = vc | Control.ModifierKeys;

                switch (keyData)
                {
                    case Keys.ShiftKey:
                    case Keys.Insert:
                        if (RaiseKeyUp(keyData))
                            return true;
                        break;
                }

                switch (keyData)
                {
                    case Keys.ShiftKey:
                        ShiftDown = false;
                        return true;
                    case Keys.Insert:
                        return PreProcessWmKeyUp_Insert(ref m);
                    default:
                        return HexViewer.BasePreProcessMessage(ref m);
                }
            }

            /// <summary>
            /// Pres the process wm key up_ insert.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            protected virtual bool PreProcessWmKeyUp_Insert(ref Message m)
            {
                HexViewer._insertActive = !HexViewer._insertActive;
                return true;
            }

            /// <summary>
            /// Raises the key up.
            /// </summary>
            /// <param name="keyData">The key data.</param>
            /// <returns></returns>
            protected bool RaiseKeyUp(Keys keyData)
            {
                KeyEventArgs e = new KeyEventArgs(keyData);
                HexViewer.OnKeyUp(e);
                return e.Handled;
            }

            #endregion

            #region Misc

            /// <summary>
            /// Performs the pos move left.
            /// </summary>
            /// <returns></returns>
            protected virtual bool PerformPosMoveLeft()
            {
                long pos = HexViewer.bytePosition;
                long sel = HexViewer._selectionLength;
                int cp = HexViewer.byteCharacterPosition;

                if (sel != 0)
                {
                    cp = 0;
                    HexViewer.SetPosition(pos, cp);
                    HexViewer.ReleaseSelection();
                }
                else
                {
                    if (pos == 0 && cp == 0)
                        return true;

                    if (cp > 0) cp--;
                    else
                    {
                        pos = Math.Max(0, pos - 1);
                        cp++;
                    }

                    HexViewer.SetPosition(pos, cp);

                    if (pos < HexViewer.startByte)
                        HexViewer.PerformScrollLineUp();

                    HexViewer.UpdateCaret();
                    HexViewer.Invalidate();
                }

                HexViewer.ScrollByteIntoView();
                return true;
            }

            /// <summary>
            /// Performs the pos move right.
            /// </summary>
            /// <returns></returns>
            protected virtual bool PerformPosMoveRight()
            {
                long pos = HexViewer.bytePosition;
                int cp = HexViewer.byteCharacterPosition;
                long sel = HexViewer._selectionLength;

                if (sel != 0)
                {
                    pos += sel;
                    cp = 0;
                    HexViewer.SetPosition(pos, cp);
                    HexViewer.ReleaseSelection();
                }
                else
                {
                    if (!(pos == HexViewer._byteProvider.Length && cp == 0))
                    {

                        if (cp > 0)
                        {
                            pos = Math.Min(HexViewer._byteProvider.Length, pos + 1);
                            cp = 0;
                        }
                        else cp++;

                        HexViewer.SetPosition(pos, cp);

                        if (pos > HexViewer.endByte - 1)
                            HexViewer.PerformScrollLineDown();

                        HexViewer.UpdateCaret();
                        HexViewer.Invalidate();
                    }
                }

                HexViewer.ScrollByteIntoView();
                return true;
            }

            /// <summary>
            /// Performs the pos move left byte.
            /// </summary>
            /// <returns></returns>
            protected virtual bool PerformPosMoveLeftByte()
            {
                long pos = HexViewer.bytePosition;
                int cp = HexViewer.byteCharacterPosition;

                if (pos == 0) return true;

                pos = Math.Max(0, pos - 1);
                cp = 0;

                HexViewer.SetPosition(pos, cp);

                if (pos < HexViewer.startByte)
                    HexViewer.PerformScrollLineUp();

                HexViewer.UpdateCaret();
                HexViewer.ScrollByteIntoView();
                HexViewer.Invalidate();

                return true;
            }

            /// <summary>
            /// Performs the pos move right byte.
            /// </summary>
            /// <returns></returns>
            protected virtual bool PerformPosMoveRightByte()
            {
                long pos = HexViewer.bytePosition;
                int cp = HexViewer.byteCharacterPosition;

                if (pos == HexViewer._byteProvider.Length) return true;

                pos = Math.Min(HexViewer._byteProvider.Length, pos + 1);
                cp = 0;

                HexViewer.SetPosition(pos, cp);

                if (pos > HexViewer.endByte - 1)
                    HexViewer.PerformScrollLineDown();

                HexViewer.UpdateCaret();
                HexViewer.ScrollByteIntoView();
                HexViewer.Invalidate();

                return true;
            }

            /// <summary>
            /// Gives some information about where to place the caret.
            /// </summary>
            /// <param name="byteIndex">the index of the byte</param>
            /// <returns>
            /// the position where the caret is to place.
            /// </returns>
            public virtual PointF GetCaretPointF(long byteIndex)
            {
                System.Diagnostics.Debug.WriteLine("GetCaretPointF()", "KeyInterpreter");
                return HexViewer.GetBytePointF(byteIndex);
            }

            /// <summary>
            /// Gets the byte position info.
            /// </summary>
            /// <param name="p">The position.</param>
            /// <returns></returns>
            protected virtual BytePositionInfo GetBytePositionInfo(Point p)
            {
                return HexViewer.GetHexBytePositionInfo(p);
            }

            #endregion
        }

        /// <summary>
        /// Handles user input such as mouse and keyboard input during string view edit
        /// </summary>
        private class StringKeyInterpreter : KeyInterpreter
        {
            #region Ctors

            /// <summary>
            /// Initializes a new instance of the <see cref="StringKeyInterpreter"/> class.
            /// </summary>
            /// <param name="owner">The owner.</param>
            public StringKeyInterpreter(HexViewer owner)
                : base(owner)
            {
                HexViewer.byteCharacterPosition = 0;
            }

            #endregion

            #region PreProcessWmKeyDown methods

            /// <summary>
            /// Pres the process wm key down.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            public override bool PreProcessWmKeyDown(ref Message m)
            {
                Keys vc = (Keys)m.WParam.ToInt32();
                Keys keyData = vc | Control.ModifierKeys;

                switch (keyData)
                {
                    case Keys.Tab | Keys.Shift:
                    case Keys.Tab:
                        if (RaiseKeyDown(keyData))
                            return true;
                        break;
                }

                switch (keyData)
                {
                    case Keys.Tab | Keys.Shift:
                        return PreProcessWmKeyDown_ShiftTab(ref m);
                    case Keys.Tab:
                        return PreProcessWmKeyDown_Tab(ref m);
                    default:
                        return base.PreProcessWmKeyDown(ref m);
                }
            }

            /// <summary>
            /// PreProcesses the WM_KEYDOWN (Left) event.
            /// </summary>
            /// <param name="m">The message.</param>
            /// <returns></returns>
            protected override bool PreProcessWmKeyDown_Left(ref Message m)
            {
                return PerformPosMoveLeftByte();
            }

            /// <summary>
            /// PreProcesses the WM_KEYDOWN (Right) event.
            /// </summary>
            /// <param name="m">The message.</param>
            /// <returns></returns>
            protected override bool PreProcessWmKeyDown_Right(ref Message m)
            {
                return PerformPosMoveRightByte();
            }

            #endregion

            #region PreProcessWmChar methods

            /// <summary>
            /// Pres the process wm char.
            /// </summary>
            /// <param name="m">The m.</param>
            /// <returns></returns>
            public override bool PreProcessWmChar(ref Message m)
            {
                if (Control.ModifierKeys == Keys.Control)
                    return HexViewer.BasePreProcessMessage(ref m);

                bool sw = HexViewer._byteProvider.SupportsWriteByte;
                bool si = HexViewer._byteProvider.SupportsInsertBytes;
                bool sd = HexViewer._byteProvider.SupportsDeleteBytes;

                long pos = HexViewer.bytePosition;
                long sel = HexViewer._selectionLength;
                int cp = HexViewer.byteCharacterPosition;

                if ((!sw && pos != HexViewer._byteProvider.Length) ||
                    (!si && pos == HexViewer._byteProvider.Length))
                    return HexViewer.BasePreProcessMessage(ref m);

                char c = (char)m.WParam.ToInt32();

                if (RaiseKeyPress(c)) return true;
                if (HexViewer.ReadOnly) return true;

                bool isInsertMode = (pos == HexViewer._byteProvider.Length);

                // do insert when insertActive = true
                if (!isInsertMode && si && HexViewer._insertActive)
                    isInsertMode = true;

                if (sd && si && sel > 0)
                {
                    HexViewer._byteProvider.DeleteBytes(pos, sel);
                    isInsertMode = true;
                    cp = 0;
                    HexViewer.SetPosition(pos, cp);
                }

                HexViewer.ReleaseSelection();

                if (isInsertMode)
                    HexViewer._byteProvider.InsertBytes(pos, new byte[] { (byte)c });
                else HexViewer._byteProvider.WriteByte(pos, (byte)c);

                PerformPosMoveRightByte();
                HexViewer.Invalidate();

                return true;
            }

            #endregion

            #region Misc

            /// <summary>
            /// Gives some information about where to place the caret.
            /// </summary>
            /// <param name="byteIndex">the index of the byte</param>
            /// <returns>
            /// the position where the caret is to place.
            /// </returns>
            public override PointF GetCaretPointF(long byteIndex)
            {
                System.Diagnostics.Debug.WriteLine("GetCaretPointF()", "StringKeyInterpreter");

                Point gp = HexViewer.GetGridBytePoint(byteIndex);
                return HexViewer.GetByteStringPointF(gp);
            }

            /// <summary>
            /// Gets the byte position info.
            /// </summary>
            /// <param name="p">The position.</param>
            /// <returns></returns>
            protected override BytePositionInfo GetBytePositionInfo(Point p)
            {
                return HexViewer.GetStringBytePositionInfo(p);
            }

            #endregion
        }
    }
}
