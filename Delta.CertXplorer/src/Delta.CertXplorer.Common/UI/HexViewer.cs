using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.ComponentModel;
using System.Security.Permissions;

using Delta.CertXplorer.Internals;

// I did not write the HexViewer control, though I can't remember where it's from...


namespace Delta.CertXplorer.UI
{
    /// <summary>
    /// Represents a hex box control.
    /// </summary>
    public partial class HexViewer : Control
    {
        /// <summary>
        /// Represents a position in the HexViewer control
        /// </summary>
        private struct BytePositionInfo
        {
            private int location;
            private long index;

            /// <summary>
            /// Initializes a new instance of the <see cref="BytePositionInfo"/> struct.
            /// </summary>
            /// <param name="characterIndex">Index of the character.</param>
            /// <param name="characterLocation">The character location.</param>
            public BytePositionInfo(long characterIndex, int characterLocation)
            {
                index = characterIndex;
                location = characterLocation;
            }

            /// <summary>
            /// Gets the character position.
            /// </summary>
            /// <value>The character position.</value>
            public int Location
            {
                get { return location; }
            }

            /// <summary>
            /// Gets the index.
            /// </summary>
            /// <value>The index.</value>
            public long Index
            {
                get { return index; }
            }
        }

        #region Fields

        /// <summary>
        /// Contains the hole content bounds of all text
        /// </summary>
        private Rectangle _recContent;

        /// <summary>
        /// Contains the line info bounds
        /// </summary>
        private Rectangle _recLineInfo;

        /// <summary>
        /// Contains the hex data bounds
        /// </summary>
        private Rectangle _recHex;

        /// <summary>
        /// Contains the string view bounds
        /// </summary>
        private Rectangle _recStringView;

        /// <summary>
        /// Contains string format information for text drawing
        /// </summary>
        private StringFormat _stringFormat;

        /// <summary>
        /// Contains the width and height of a single char
        /// </summary>
        private SizeF _charSize;

        /// <summary>
        /// Contains the maximum of visible horizontal bytes
        /// </summary>
        private int hexMaxHBytes;

        /// <summary>
        /// Contains the maximum of visible vertical bytes
        /// </summary>
        private int hexMaxVBytes;

        /// <summary>
        /// Contains the maximum of visible bytes.
        /// </summary>
        private int hexMaxBytes;

        /// <summary>
        /// Contains the scroll bars minimum value
        /// </summary>
        private long scrollVmin;

        /// <summary>
        /// Contains the scroll bars maximum value
        /// </summary>
        private long scrollVmax;

        /// <summary>
        /// Contains the scroll bars current position
        /// </summary>
        private long scrollVpos;

        /// <summary>
        /// Contains a vertical scroll
        /// </summary>
        private VScrollBar vScrollBar;

        /// <summary>
        /// Contains the border´s left shift
        /// </summary>
        private int recBorderLeft = SystemInformation.Border3DSize.Width;

        /// <summary>
        /// Contains the border´s right shift
        /// </summary>
        private int recBorderRight = SystemInformation.Border3DSize.Width;

        /// <summary>
        /// Contains the border´s top shift
        /// </summary>
        private int recBorderTop = SystemInformation.Border3DSize.Height;

        /// <summary>
        /// Contains the border bottom shift
        /// </summary>
        private int recBorderBottom = SystemInformation.Border3DSize.Height;

        /// <summary>
        /// Contains the index of the first visible byte
        /// </summary>
        private long startByte;

        /// <summary>
        /// Contains the index of the last visible byte
        /// </summary>
        private long endByte;

        /// <summary>
        /// Contains the current byte position
        /// </summary>
        private long bytePosition = -1;

        /// <summary>
        /// Contains the current char position in one byte
        /// </summary>
        /// <example>
        /// "1A"
        /// "1" = char position of 0
        /// "A" = char position of 1
        /// </example>
        private int byteCharacterPosition;

        /// <summary>
        /// Contains string format information for hex values
        /// </summary>
        private string hexStringFormat = "X";

        /// <summary>
        /// Contains the current key interpreter
        /// </summary>
        private IKeyInterpreter currentKeyInterpreter;

        /// <summary>
        /// Contains an empty key interpreter without functionality
        /// </summary>
        private EmptyKeyInterpreter emptyKeyInterpreter;

        /// <summary>
        /// Contains the default key interpreter
        /// </summary>
        private KeyInterpreter keyInterpreter;

        /// <summary>
        /// Contains the string key interpreter
        /// </summary>
        private StringKeyInterpreter stringKeyInterpreter;

        /// <summary>
        /// Contains True if caret is visible
        /// </summary>
        private bool caretVisible;

        /// <summary>
        /// Contains true, if the find (Find method) should be aborted.
        /// </summary>
        private bool abortFind;

        /// <summary>
        /// Contains a value of the current finding position.
        /// </summary>
        private long findingPos;

        /// <summary>
        /// Contains a state value about Insert or Write mode. When this value is true and the ByteProvider SupportsInsert is true bytes are inserted instead of overridden.
        /// </summary>
        private bool _insertActive;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="HexViewer"/> class.
        /// </summary>
        public HexViewer()
        {
            InitializeComponent();

            vScrollBar = new VScrollBar();
            vScrollBar.Scroll += (s, e) =>
            {
                switch (e.Type)
                {
                    case ScrollEventType.Last:
                        break;
                    case ScrollEventType.EndScroll:
                        break;
                    case ScrollEventType.SmallIncrement:
                        PerformScrollLineDown();
                        break;
                    case ScrollEventType.SmallDecrement:
                        PerformScrollLineUp();
                        break;
                    case ScrollEventType.LargeIncrement:
                        PerformScrollPageDown();
                        break;
                    case ScrollEventType.LargeDecrement:
                        PerformScrollPageUp();
                        break;
                    case ScrollEventType.ThumbPosition:
                        long lPos = FromScrollPos(e.NewValue);
                        PerformScrollThumpPosition(lPos);
                        break;
                    case ScrollEventType.ThumbTrack:
                        break;
                    case ScrollEventType.First:
                        break;
                    default:
                        break;
                }

                e.NewValue = ToScrollPos(scrollVpos);
            };

            BackColor = Color.White;
            Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            _stringFormat = new StringFormat(StringFormat.GenericTypographic);
            _stringFormat.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;

            ActivateEmptyKeyInterpreter();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        #region Events

        /// <summary>
        /// Occurs, when the value of ReadOnly property has changed.
        /// </summary>
        [Description("Occurs, when the value of ReadOnly property has changed.")]
        public event EventHandler ReadOnlyChanged;

        /// <summary>
        /// Occurs, when the value of ByteProvider property has changed.
        /// </summary>
        [Description("Occurs, when the value of ByteProvider property has changed.")]
        public event EventHandler ByteProviderChanged;

        /// <summary>
        /// Occurs, when the value of SelectionStart property has changed.
        /// </summary>
        [Description("Occurs, when the value of SelectionStart property has changed.")]
        public event EventHandler SelectionStartChanged;

        /// <summary>
        /// Occurs, when the value of SelectionLength property has changed.
        /// </summary>
        [Description("Occurs, when the value of SelectionLength property has changed.")]
        public event EventHandler SelectionLengthChanged;

        /// <summary>
        /// Occurs, when the value of LineInfoVisible property has changed.
        /// </summary>
        [Description("Occurs, when the value of LineInfoVisible property has changed.")]
        public event EventHandler LineInfoVisibleChanged;

        /// <summary>
        /// Occurs, when the value of StringViewVisible property has changed.
        /// </summary>
        [Description("Occurs, when the value of StringViewVisible property has changed.")]
        public event EventHandler StringViewVisibleChanged;

        /// <summary>
        /// Occurs, when the value of BorderStyle property has changed.
        /// </summary>
        [Description("Occurs, when the value of BorderStyle property has changed.")]
        public event EventHandler BorderStyleChanged;

        /// <summary>
        /// Occurs, when the value of BytesPerLine property has changed.
        /// </summary>
        [Description("Occurs, when the value of BytesPerLine property has changed.")]
        public event EventHandler BytesPerLineChanged;

        /// <summary>
        /// Occurs, when the value of UseFixedBytesPerLine property has changed.
        /// </summary>
        [Description("Occurs, when the value of UseFixedBytesPerLine property has changed.")]
        public event EventHandler UseFixedBytesPerLineChanged;

        /// <summary>
        /// Occurs, when the value of VScrollBarVisible property has changed.
        /// </summary>
        [Description("Occurs, when the value of VScrollBarVisible property has changed.")]
        public event EventHandler VScrollBarVisibleChanged;

        /// <summary>
        /// Occurs, when the value of Casing property has changed.
        /// </summary>
        [Description("Occurs, when the value of Casing property has changed.")]
        public event EventHandler CasingChanged;

        /// <summary>
        /// Occurs, when the value of HorizontalByteCount property has changed.
        /// </summary>
        [Description("Occurs, when the value of HorizontalByteCount property has changed.")]
        public event EventHandler HorizontalByteCountChanged;

        /// <summary>
        /// Occurs, when the value of VerticalByteCount property has changed.
        /// </summary>
        [Description("Occurs, when the value of VerticalByteCount property has changed.")]
        public event EventHandler VerticalByteCountChanged;

        /// <summary>
        /// Occurs, when the value of CurrentLine property has changed.
        /// </summary>
        [Description("Occurs, when the value of CurrentLine property has changed.")]
        public event EventHandler CurrentLineChanged;

        /// <summary>
        /// Occurs, when the value of CurrentPositionInLine property has changed.
        /// </summary>
        [Description("Occurs, when the value of CurrentPositionInLine property has changed.")]
        public event EventHandler CurrentPositionInLineChanged;

        #endregion

        #region Properties

        private IByteProvider _byteProvider;

        /// <summary>
        /// Gets or sets the ByteProvider.
        /// </summary>
        [Browsable(false), DefaultValue(null)]
        private IByteProvider ByteProvider
        {
            get { return _byteProvider; }
            set
            {
                if (_byteProvider == value)
                    return;

                if (value == null)
                    ActivateEmptyKeyInterpreter();
                else
                    ActivateKeyInterpreter();

                if (_byteProvider != null)
                    _byteProvider.LengthChanged -= new EventHandler(OnByteProviderLengthChanged);

                _byteProvider = value;
                if (_byteProvider != null)
                    _byteProvider.LengthChanged += new EventHandler(OnByteProviderLengthChanged);

                OnByteProviderChanged(EventArgs.Empty);

                if (value == null) // do not raise events if value is null
                {
                    bytePosition = -1;
                    byteCharacterPosition = 0;
                    _selectionLength = 0;

                    DestroyCaret();
                }
                else
                {
                    SetPosition(0, 0);
                    SetSelectionLength(0);

                    if (caretVisible && Focused) UpdateCaret();
                    else CreateCaret();
                }

                CheckCurrentLineChanged();
                CheckCurrentPositionInLineChanged();

                scrollVpos = 0;

                UpdateVisibilityBytes();
                UpdateRectanglePositioning();

                Invalidate();
            }
        }

        private byte[] data = null;

        [Browsable(false), DefaultValue(null), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public byte[] Data
        {
            get { return data; }
            set
            {
                data = value;
                ByteProvider = new ByteArrayProvider(data);
            }
        }

        /// <summary>
        /// Gets a value that indicates the current position during Find method execution.
        /// </summary>
        [DefaultValue(0), Browsable(false)]
        public long CurrentFindingPosition
        {
            get { return findingPos; }
        }

        /// <summary>
        /// Gets or sets if the count of bytes in one line is fix.
        /// </summary>
        /// <remarks>
        /// When set to True, BytesPerLine property determine the maximum count of bytes in one line.
        /// </remarks>
        [DefaultValue(false), Category("Hex"), Description("Gets or sets if the count of bytes in one line is fix.")]
        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                if (_readOnly == value)
                    return;

                _readOnly = value;
                OnReadOnlyChanged(EventArgs.Empty);
                Invalidate();
            }
        }

        private bool _readOnly;

        /// <summary>
        /// Gets or sets the maximum count of bytes in one line.
        /// </summary>
        /// <remarks>
        /// UsedFixedBytesPerLine property must set to true
        /// </remarks>
        [DefaultValue(16), Category("Hex"), Description("Gets or sets the maximum count of bytes in one line.")]
        public int BytesPerLine
        {
            get { return _bytesPerLine; }
            set
            {
                if (_bytesPerLine == value)
                    return;

                _bytesPerLine = value;
                OnByteProviderChanged(EventArgs.Empty);

                UpdateRectanglePositioning();
                Invalidate();
            }
        } int _bytesPerLine = 16;

        /// <summary>
        /// Gets or sets if the count of bytes in one line is fix.
        /// </summary>
        /// <remarks>
        /// When set to True, BytesPerLine property determine the maximum count of bytes in one line.
        /// </remarks>
        [DefaultValue(false), Category("Hex"), Description("Gets or sets if the count of bytes in one line is fix.")]
        public bool UseFixedBytesPerLine
        {
            get { return _useFixedBytesPerLine; }
            set
            {
                if (_useFixedBytesPerLine == value)
                    return;

                _useFixedBytesPerLine = value;
                OnUseFixedBytesPerLineChanged(EventArgs.Empty);

                UpdateRectanglePositioning();
                Invalidate();
            }
        } bool _useFixedBytesPerLine;

        /// <summary>
        /// Gets or sets the visibility of a vertical scroll bar.
        /// </summary>
        [DefaultValue(false), Category("Hex"), Description("Gets or sets the visibility of a vertical scroll bar.")]
        public bool VScrollBarVisible
        {
            get { return this._vScrollBarVisible; }
            set
            {
                if (_vScrollBarVisible == value)
                    return;

                _vScrollBarVisible = value;

                if (_vScrollBarVisible)
                    Controls.Add(vScrollBar);
                else
                    Controls.Remove(vScrollBar);

                UpdateRectanglePositioning();
                UpdateScrollSize();

                OnVScrollBarVisibleChanged(EventArgs.Empty);
            }
        } bool _vScrollBarVisible;

        /// <summary>
        /// Gets or sets the visibility of a line info.
        /// </summary>
        [DefaultValue(false), Category("Hex"), Description("Gets or sets the visibility of a line info.")]
        public bool LineInfoVisible
        {
            get { return _lineInfoVisible; }
            set
            {
                if (_lineInfoVisible == value)
                    return;

                _lineInfoVisible = value;
                OnLineInfoVisibleChanged(EventArgs.Empty);

                UpdateRectanglePositioning();
                Invalidate();
            }
        } bool _lineInfoVisible;

        /// <summary>
        /// Gets or sets the hex box´s border style.
        /// </summary>
        [DefaultValue(typeof(BorderStyle), "Fixed3D"), Category("Hex"), Description("Gets or sets the hex box´s border style.")]
        public BorderStyle BorderStyle
        {
            get { return _borderStyle; }
            set
            {
                if (_borderStyle == value)
                    return;

                _borderStyle = value;
                switch (_borderStyle)
                {
                    case BorderStyle.None:
                        recBorderLeft = recBorderTop = recBorderRight = recBorderBottom = 0;
                        break;
                    case BorderStyle.Fixed3D:
                        recBorderLeft = recBorderRight = SystemInformation.Border3DSize.Width;
                        recBorderTop = recBorderBottom = SystemInformation.Border3DSize.Height;
                        break;
                    case BorderStyle.FixedSingle:
                        recBorderLeft = recBorderTop = recBorderRight = recBorderBottom = 1;
                        break;
                }

                UpdateRectanglePositioning();

                OnBorderStyleChanged(EventArgs.Empty);

            }
        } BorderStyle _borderStyle = BorderStyle.Fixed3D;

        /// <summary>
        /// Gets or sets the visibility of the string view.
        /// </summary>
        [DefaultValue(false), Category("Hex"), Description("Gets or sets the visibility of the string view.")]
        public bool StringViewVisible
        {
            get { return _stringViewVisible; }
            set
            {
                if (_stringViewVisible == value)
                    return;

                _stringViewVisible = value;
                OnStringViewVisibleChanged(EventArgs.Empty);

                UpdateRectanglePositioning();
                Invalidate();
            }
        } bool _stringViewVisible;

        /// <summary>
        /// Gets or sets whether the HexBox control displays the hex characters in upper or lower case.
        /// </summary>
        [DefaultValue(typeof(CharacterCasing), "Upper"), Category("Hex"), Description("Gets or sets whether the HexBox control displays the hex characters in upper or lower case.")]
        public CharacterCasing Casing
        {
            get
            {
                return (hexStringFormat == "X" ? CharacterCasing.Upper : CharacterCasing.Lower);
            }
            set
            {
                string format = (value == CharacterCasing.Upper ? "X" : "x");

                if (hexStringFormat == format)
                    return;

                hexStringFormat = format;
                OnCasingChanged(EventArgs.Empty);

                Invalidate();
            }
        }

        /// <summary>
        /// Gets and sets the starting point of the bytes selected in the hex box.
        /// </summary>
        [Browsable(false), DefaultValue(0)]
        public long SelectionStart
        {
            get { return bytePosition; }
            set
            {
                SetPosition(value, 0);
                ScrollByteIntoView();
                Invalidate();
            }
        }

        /// <summary>
        /// Gets and sets the number of bytes selected in the hex box.
        /// </summary>
        [DefaultValue(0)]
        public long SelectionLength
        {
            get { return _selectionLength; }
            set
            {
                SetSelectionLength(value);
                ScrollByteIntoView();
                Invalidate();
            }
        } long _selectionLength;


        /// <summary>
        /// Gets or sets the background color for the selected bytes.
        /// </summary>
        [DefaultValue(typeof(Color), "Blue"), Category("Hex"), Description("Gets or sets the background color for the selected bytes.")]
        public Color SelectionBackColor
        {
            get { return _selectionBackColor; }
            set { _selectionBackColor = value; Invalidate(); }
        } Color _selectionBackColor = Color.Blue;

        /// <summary>
        /// Gets or sets the foreground color for the selected bytes.
        /// </summary>
        [DefaultValue(typeof(Color), "White"), Category("Hex"), Description("Gets or sets the foreground color for the selected bytes.")]
        public Color SelectionForeColor
        {
            get { return _selectionForeColor; }
            set { _selectionForeColor = value; Invalidate(); }
        } Color _selectionForeColor = Color.White;

        /// <summary>
        /// Gets or sets the visibility of a shadow selection.
        /// </summary>
        [DefaultValue(true), Category("Hex"), Description("Gets or sets the visibility of a shadow selection.")]
        public bool ShadowSelectionVisible
        {
            get { return _shadowSelectionVisible; }
            set
            {
                if (_shadowSelectionVisible == value)
                    return;
                _shadowSelectionVisible = value;
                Invalidate();
            }
        } bool _shadowSelectionVisible = true;

        /// <summary>
        /// Gets or sets the color of the shadow selection. 
        /// </summary>
        /// <remarks>
        /// A alpha component must be given! 
        /// Default alpha = 100
        /// </remarks>
        [Category("Hex"), Description("Gets or sets the color of the shadow selection.")]
        public Color ShadowSelectionColor
        {
            get { return _shadowSelectionColor; }
            set { _shadowSelectionColor = value; Invalidate(); }
        } Color _shadowSelectionColor = Color.FromArgb(100, 60, 188, 255);

        /// <summary>
        /// Gets the number bytes drawn horizontally.
        /// </summary>
        [DefaultValue(true), Browsable(false)]
        public int HorizontalByteCount
        {
            get { return hexMaxHBytes; }
        }

        /// <summary>
        /// Gets the number bytes drawn vertically.
        /// </summary>
        [DefaultValue(true), Browsable(false)]
        public int VerticalByteCount
        {
            get { return hexMaxVBytes; }
        }

        /// <summary>
        /// Gets the current line
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public long CurrentLine
        {
            get { return _currentLine; }
        } long _currentLine;

        /// <summary>
        /// Gets the current position in the current line
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public long CurrentPositionInLine
        {
            get { return _currentPositionInLine; }
        } int _currentPositionInLine;

        /// <summary>
        /// Gets the a value if insertion mode is active or not.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool InsertActive
        {
            get { return _insertActive; }
        }

        #endregion

        #region Overridden properties

        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        [DefaultValue(typeof(Color), "White")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        /// <summary>
        /// The font used to display text in the hexbox.
        /// </summary>
        [Editor(typeof(FixedSizeFontEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public override Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        /// <summary>
        /// Not used.
        /// </summary>
        [DefaultValue(""),
        Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        EditorBrowsable(EditorBrowsableState.Never),
        Bindable(false)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        /// <summary>
        /// Not used.
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        EditorBrowsable(EditorBrowsableState.Never),
        Bindable(false)]
        public override RightToLeft RightToLeft
        {
            get { return base.RightToLeft; }
            set { base.RightToLeft = value; }
        }

        #endregion

        #region Paint methods

        /// <summary>
        /// Paints the background.
        /// </summary>
        /// <param name="e">A PaintEventArgs that contains the event data.</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(BackColor), ClientRectangle);

            switch (_borderStyle)
            {
                case BorderStyle.Fixed3D:
                    if (VisualStylesEnabled())
                    {
                        //// draw xp themed border
                        //int partId = Interop.EP_EDITTEXT;

                        //int stateId;
                        //if (Enabled) stateId = Interop.ETS_NORMAL;
                        //else stateId = Interop.ETS_DISABLED;

                        //var rect = Rect.FromRectangle(this.ClientRectangle);

                        //IntPtr hTheme = Interop.OpenThemeData(this.Handle, "EDIT");

                        //IntPtr hDC = e.Graphics.GetHdc();

                        //Interop.DrawThemeBackground(hTheme, hDC, partId, stateId, ref rect, IntPtr.Zero);

                        //e.Graphics.ReleaseHdc(hDC);

                        //Interop.CloseThemeData(hTheme);

                        var element = Enabled ? VisualStyleElement.TextBox.TextEdit.Normal :
                            VisualStyleElement.TextBox.TextEdit.Disabled;
                        var renderer = new VisualStyleRenderer(element);
                        renderer.DrawBackground(e.Graphics, ClientRectangle);


                    } // draw default border
                    else ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle, Border3DStyle.Sunken);
                    break;
                case BorderStyle.FixedSingle:
                    // draw fixed single border
                    ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
                    break;
            }
        }

        /// <summary>
        /// Paints the hex box.
        /// </summary>
        /// <param name="e">A PaintEventArgs that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_byteProvider == null) return;

            // draw only in the content rectangle, so exclude the border and the scrollbar.
            Region r = new Region(ClientRectangle);
            r.Exclude(_recContent);
            e.Graphics.ExcludeClip(r);

            UpdateVisibilityBytes();

            if (_lineInfoVisible)
                PaintLineInfo(e.Graphics, startByte, endByte);

            if (!_stringViewVisible)
                PaintHex(e.Graphics, startByte, endByte);
            else
            {
                PaintHexAndStringView(e.Graphics, startByte, endByte);
                if (_shadowSelectionVisible)
                    PaintCurrentBytesSign(e.Graphics);
            }
        }

        private void PaintLineInfo(Graphics g, long startByte, long endByte)
        {
            Brush brush = new SolidBrush(GetDefaultForeColor());
            int maxLine = GetGridBytePoint(endByte - startByte).Y + 1;

            for (int i = 0; i < maxLine; i++)
            {
                long lastLineByte = startByte + (hexMaxHBytes) * i + hexMaxHBytes;

                PointF bytePointF = GetBytePointF(new Point(0, 0 + i));
                string info = lastLineByte.ToString(hexStringFormat, System.Threading.Thread.CurrentThread.CurrentCulture);
                int nulls = 8 - info.Length;

                string formattedInfo;
                if (nulls > -1) formattedInfo = new string('0', 8 - info.Length) + info;
                else formattedInfo = new string('~', 8);

                g.DrawString(formattedInfo, Font, brush, new PointF(_recLineInfo.X, bytePointF.Y), _stringFormat);
            }
        }

        private void PaintHex(Graphics g, long startByte, long endByte)
        {
            Brush brush = new SolidBrush(GetDefaultForeColor());
            Brush selBrush = new SolidBrush(_selectionForeColor);
            Brush selBrushBack = new SolidBrush(_selectionBackColor);

            int counter = -1;
            long intern_endByte = Math.Min(_byteProvider.Length - 1, endByte + hexMaxHBytes);

            bool isKeyInterpreterActive = currentKeyInterpreter == null || currentKeyInterpreter.GetType() == typeof(KeyInterpreter);

            for (long i = startByte; i < intern_endByte + 1; i++)
            {
                counter++;
                Point gridPoint = GetGridBytePoint(counter);
                byte b = _byteProvider.ReadByte(i);

                bool isSelectedByte = i >= bytePosition && i <= (bytePosition + _selectionLength - 1) && _selectionLength != 0;

                if (isSelectedByte && isKeyInterpreterActive)
                    PaintHexStringSelected(g, b, selBrush, selBrushBack, gridPoint);
                else PaintHexString(g, b, brush, gridPoint);
            }
        }

        private void PaintHexString(Graphics g, byte b, Brush brush, Point gridPoint)
        {
            PointF bytePointF = GetBytePointF(gridPoint);

            string sB = b.ToString(hexStringFormat, System.Threading.Thread.CurrentThread.CurrentCulture);
            if (sB.Length == 1) sB = "0" + sB;

            g.DrawString(sB.Substring(0, 1), Font, brush, bytePointF, _stringFormat);
            bytePointF.X += _charSize.Width;
            g.DrawString(sB.Substring(1, 1), Font, brush, bytePointF, _stringFormat);
        }

        private void PaintHexStringSelected(Graphics g, byte b, Brush brush, Brush brushBack, Point gridPoint)
        {
            string sB = b.ToString(hexStringFormat, System.Threading.Thread.CurrentThread.CurrentCulture);
            if (sB.Length == 1) sB = "0" + sB;

            PointF bytePointF = GetBytePointF(gridPoint);

            bool isLastLineChar = (gridPoint.X + 1 == hexMaxHBytes);
            float bcWidth = (isLastLineChar) ? _charSize.Width * 2 : _charSize.Width * 3;

            g.FillRectangle(brushBack, bytePointF.X, bytePointF.Y, bcWidth, _charSize.Height);
            g.DrawString(sB.Substring(0, 1), Font, brush, bytePointF, _stringFormat);
            bytePointF.X += _charSize.Width;
            g.DrawString(sB.Substring(1, 1), Font, brush, bytePointF, _stringFormat);
        }

        private void PaintHexAndStringView(Graphics g, long startByte, long endByte)
        {
            Brush brush = new SolidBrush(GetDefaultForeColor());
            Brush selBrush = new SolidBrush(_selectionForeColor);
            Brush selBrushBack = new SolidBrush(_selectionBackColor);

            int counter = -1;
            long intern_endByte = Math.Min(_byteProvider.Length - 1, endByte + hexMaxHBytes);

            bool isKeyInterpreterActive = currentKeyInterpreter == null || currentKeyInterpreter.GetType() == typeof(KeyInterpreter);
            bool isStringKeyInterpreterActive = currentKeyInterpreter != null && currentKeyInterpreter.GetType() == typeof(StringKeyInterpreter);

            for (long i = startByte; i < intern_endByte + 1; i++)
            {
                counter++;
                Point gridPoint = GetGridBytePoint(counter);
                PointF byteStringPointF = GetByteStringPointF(gridPoint);
                byte b = _byteProvider.ReadByte(i);

                bool isSelectedByte = i >= bytePosition && i <= (bytePosition + _selectionLength - 1) && _selectionLength != 0;

                if (isSelectedByte && isKeyInterpreterActive)
                    PaintHexStringSelected(g, b, selBrush, selBrushBack, gridPoint);
                else PaintHexString(g, b, brush, gridPoint);

                string s;
                if (b > 0x1F && !(b > 0x7E && b < 0xA0))
                    s = ((char)b).ToString();
                else s = ".";

                if (isSelectedByte && isStringKeyInterpreterActive)
                {
                    g.FillRectangle(selBrushBack, byteStringPointF.X, byteStringPointF.Y, _charSize.Width, _charSize.Height);
                    g.DrawString(s, Font, selBrush, byteStringPointF, _stringFormat);
                }
                else g.DrawString(s, Font, brush, byteStringPointF, _stringFormat);
            }
        }

        private void PaintCurrentBytesSign(Graphics g)
        {
            if (currentKeyInterpreter != null && Focused && bytePosition != -1 && Enabled)
            {
                if (currentKeyInterpreter.GetType() == typeof(KeyInterpreter))
                {
                    if (_selectionLength == 0)
                    {
                        Point gp = GetGridBytePoint(bytePosition - startByte);
                        PointF pf = GetByteStringPointF(gp);
                        Size s = new Size((int)_charSize.Width, (int)_charSize.Height);
                        Rectangle r = new Rectangle((int)pf.X, (int)pf.Y, s.Width, s.Height);
                        if (r.IntersectsWith(_recStringView))
                        {
                            r.Intersect(_recStringView);
                            PaintCurrentByteSign(g, r);
                        }
                    }
                    else
                    {
                        int lineWidth = (int)(_recStringView.Width - _charSize.Width);

                        Point startSelGridPoint = GetGridBytePoint(bytePosition - startByte);
                        PointF startSelPointF = GetByteStringPointF(startSelGridPoint);

                        Point endSelGridPoint = GetGridBytePoint(bytePosition - startByte + _selectionLength - 1);
                        PointF endSelPointF = GetByteStringPointF(endSelGridPoint);

                        int multiLine = endSelGridPoint.Y - startSelGridPoint.Y;
                        if (multiLine == 0)
                        {
                            Rectangle singleLine = new Rectangle(
                                (int)startSelPointF.X,
                                (int)startSelPointF.Y,
                                (int)(endSelPointF.X - startSelPointF.X + _charSize.Width),
                                (int)_charSize.Height);
                            if (singleLine.IntersectsWith(_recStringView))
                            {
                                singleLine.Intersect(_recStringView);
                                PaintCurrentByteSign(g, singleLine);
                            }
                        }
                        else
                        {
                            Rectangle firstLine = new Rectangle(
                                (int)startSelPointF.X,
                                (int)startSelPointF.Y,
                                (int)(_recStringView.X + lineWidth - startSelPointF.X + _charSize.Width),
                                (int)_charSize.Height);
                            if (firstLine.IntersectsWith(_recStringView))
                            {
                                firstLine.Intersect(_recStringView);
                                PaintCurrentByteSign(g, firstLine);
                            }

                            if (multiLine > 1)
                            {
                                Rectangle betweenLines = new Rectangle(
                                    _recStringView.X,
                                    (int)(startSelPointF.Y + _charSize.Height),
                                    (int)(_recStringView.Width),
                                    (int)(_charSize.Height * (multiLine - 1)));
                                if (betweenLines.IntersectsWith(_recStringView))
                                {
                                    betweenLines.Intersect(_recStringView);
                                    PaintCurrentByteSign(g, betweenLines);
                                }

                            }

                            Rectangle lastLine = new Rectangle(
                                _recStringView.X,
                                (int)endSelPointF.Y,
                                (int)(endSelPointF.X - _recStringView.X + _charSize.Width),
                                (int)_charSize.Height);
                            if (lastLine.IntersectsWith(_recStringView))
                            {
                                lastLine.Intersect(_recStringView);
                                PaintCurrentByteSign(g, lastLine);
                            }
                        }
                    }
                }
                else
                {
                    if (_selectionLength == 0)
                    {
                        Point gp = GetGridBytePoint(bytePosition - startByte);
                        PointF pf = GetBytePointF(gp);
                        Size s = new Size((int)_charSize.Width * 2, (int)_charSize.Height);
                        Rectangle r = new Rectangle((int)pf.X, (int)pf.Y, s.Width, s.Height);
                        PaintCurrentByteSign(g, r);
                    }
                    else
                    {
                        int lineWidth = (int)(_recHex.Width - _charSize.Width * 5);

                        Point startSelGridPoint = GetGridBytePoint(bytePosition - startByte);
                        PointF startSelPointF = GetBytePointF(startSelGridPoint);

                        Point endSelGridPoint = GetGridBytePoint(bytePosition - startByte + _selectionLength - 1);
                        PointF endSelPointF = GetBytePointF(endSelGridPoint);

                        int multiLine = endSelGridPoint.Y - startSelGridPoint.Y;
                        if (multiLine == 0)
                        {
                            Rectangle singleLine = new Rectangle(
                                (int)startSelPointF.X,
                                (int)startSelPointF.Y,
                                (int)(endSelPointF.X - startSelPointF.X + _charSize.Width * 2),
                                (int)_charSize.Height);
                            if (singleLine.IntersectsWith(_recHex))
                            {
                                singleLine.Intersect(_recHex);
                                PaintCurrentByteSign(g, singleLine);
                            }
                        }
                        else
                        {
                            Rectangle firstLine = new Rectangle(
                                (int)startSelPointF.X,
                                (int)startSelPointF.Y,
                                (int)(_recHex.X + lineWidth - startSelPointF.X + _charSize.Width * 2),
                                (int)_charSize.Height);
                            if (firstLine.IntersectsWith(_recHex))
                            {
                                firstLine.Intersect(_recHex);
                                PaintCurrentByteSign(g, firstLine);
                            }

                            if (multiLine > 1)
                            {
                                Rectangle betweenLines = new Rectangle(
                                    _recHex.X,
                                    (int)(startSelPointF.Y + _charSize.Height),
                                    (int)(lineWidth + _charSize.Width * 2),
                                    (int)(_charSize.Height * (multiLine - 1)));
                                if (betweenLines.IntersectsWith(_recHex))
                                {
                                    betweenLines.Intersect(_recHex);
                                    PaintCurrentByteSign(g, betweenLines);
                                }

                            }

                            Rectangle lastLine = new Rectangle(
                                _recHex.X,
                                (int)endSelPointF.Y,
                                (int)(endSelPointF.X - _recHex.X + _charSize.Width * 2),
                                (int)_charSize.Height);
                            if (lastLine.IntersectsWith(_recHex))
                            {
                                lastLine.Intersect(_recHex);
                                PaintCurrentByteSign(g, lastLine);
                            }
                        }
                    }
                }
            }
        }

        private void PaintCurrentByteSign(Graphics g, Rectangle rec)
        {
            Bitmap myBitmap = new Bitmap(rec.Width, rec.Height);
            Graphics bitmapGraphics = Graphics.FromImage(myBitmap);

            SolidBrush greenBrush = new SolidBrush(_shadowSelectionColor);

            bitmapGraphics.FillRectangle(greenBrush, 0,
                0, rec.Width, rec.Height);

            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.GammaCorrected;

            g.DrawImage(myBitmap, rec.Left, rec.Top);
        }

        private Color GetDefaultForeColor()
        {
            if (Enabled) return ForeColor;
            else return Color.Gray;
        }

        private void UpdateVisibilityBytes()
        {
            if (_byteProvider == null || _byteProvider.Length == 0)
                return;

            startByte = (scrollVpos + 1) * hexMaxHBytes - hexMaxHBytes;
            endByte = (long)Math.Min(_byteProvider.Length - 1, startByte + hexMaxBytes);
        }

        #endregion

        #region Scroll methods

        private void UpdateScrollSize()
        {
            System.Diagnostics.Debug.WriteLine("UpdateScrollSize()", "HexBox");

            // calc scroll bar info
            if (VScrollBarVisible && _byteProvider != null && _byteProvider.Length > 0 && hexMaxHBytes != 0)
            {
                long scrollmax = (long)Math.Ceiling((double)_byteProvider.Length / (double)hexMaxHBytes - (double)hexMaxVBytes);
                scrollmax = Math.Max(0, scrollmax);

                long scrollpos = startByte / hexMaxHBytes;

                if (scrollmax == scrollVmax && scrollpos == scrollVpos)
                    return;

                scrollVmin = 0;
                scrollVmax = scrollmax;
                scrollVpos = Math.Min(scrollpos, scrollmax);
                UpdateVScroll();
            }
            else if (VScrollBarVisible)
            {
                // disable scroll bar
                scrollVmin = 0;
                scrollVmax = 0;
                scrollVpos = 0;
                UpdateVScroll();
            }
        }

        private void UpdateVScroll()
        {
            System.Diagnostics.Debug.WriteLine("UpdateVScroll()", "HexBox");

            int max = ToScrollMax(scrollVmax);

            if (max > 0)
            {
                vScrollBar.Minimum = 0;
                vScrollBar.Maximum = max;
                vScrollBar.Value = ToScrollPos(scrollVpos);
                vScrollBar.Enabled = true;
            }
            else vScrollBar.Enabled = false;
        }

        private int ToScrollPos(long value)
        {
            int max = 65535;

            if (scrollVmax < max) return (int)value;

            double valperc = (double)value / (double)scrollVmax * (double)100;
            int res = (int)Math.Floor((double)max / (double)100 * valperc);
            res = (int)Math.Max(scrollVmin, res);
            res = (int)Math.Min(scrollVmax, res);
            return res;
        }

        private long FromScrollPos(int value)
        {
            int max = 65535;
            if (scrollVmax < max) return (long)value;

            double valperc = (double)value / (double)max * (double)100;
            long res = (int)Math.Floor((double)scrollVmax / (double)100 * valperc);
            return res;
        }

        private int ToScrollMax(long value)
        {
            long max = 65535;
            if (value > max) return (int)max;
            else return (int)value;
        }

        private void PerformScrollToLine(long pos)
        {
            if (pos < scrollVmin || pos > scrollVmax || pos == scrollVpos)
                return;

            scrollVpos = pos;

            UpdateVScroll();
            UpdateVisibilityBytes();
            UpdateCaret();
            Invalidate();
        }

        private void PerformScrollLines(int lines)
        {
            long pos = 0L;
            if (lines > 0) pos = Math.Min(scrollVmax, scrollVpos + lines);
            else if (lines < 0) pos = Math.Max(scrollVmin, scrollVpos + lines);
            else return;

            PerformScrollToLine(pos);
        }

        private void PerformScrollLineDown()
        {
            PerformScrollLines(1);
        }

        private void PerformScrollLineUp()
        {
            PerformScrollLines(-1);
        }

        private void PerformScrollPageDown()
        {
            PerformScrollLines(hexMaxVBytes);
        }

        private void PerformScrollPageUp()
        {
            PerformScrollLines(-hexMaxVBytes);
        }

        private void PerformScrollThumpPosition(long pos)
        {
            PerformScrollToLine(pos);
        }

        /// <summary>
        /// Scrolls the selection start byte into view
        /// </summary>
        public void ScrollByteIntoView()
        {
            System.Diagnostics.Debug.WriteLine("ScrollByteIntoView()", "HexBox");

            ScrollByteIntoView(bytePosition);
        }

        /// <summary>
        /// Scrolls the specific byte into view
        /// </summary>
        /// <param name="index">the index of the byte</param>
        public void ScrollByteIntoView(long index)
        {
            System.Diagnostics.Debug.WriteLine("ScrollByteIntoView(long index)", "HexBox");

            if (_byteProvider == null || currentKeyInterpreter == null)
                return;

            if (index < startByte)
            {
                long line = (long)Math.Floor((double)index / (double)hexMaxHBytes);
                PerformScrollThumpPosition(line);
            }
            else if (index > endByte)
            {
                long line = (long)Math.Floor((double)index / (double)hexMaxHBytes);
                line -= hexMaxVBytes - 1;
                PerformScrollThumpPosition(line);
            }
        }

        #endregion

        #region Selection methods

        /// <summary>
        /// Selects the hex box.
        /// </summary>
        /// <param name="start">the start index of the selection</param>
        /// <param name="length">the length of the selection</param>
        public void Select(long start, long length)
        {
            InternalSelect(start, length);
            ScrollByteIntoView();
        }

        private void ReleaseSelection()
        {
            System.Diagnostics.Debug.WriteLine("ReleaseSelection()", "HexBox");

            if (_selectionLength == 0)
                return;
            _selectionLength = 0;
            OnSelectionLengthChanged(EventArgs.Empty);

            if (!caretVisible)
                CreateCaret();
            else
                UpdateCaret();

            Invalidate();
        }

        private void InternalSelect(long start, long length)
        {
            long pos = start;
            long sel = length;
            int cp = 0;

            if (sel > 0 && caretVisible) DestroyCaret();
            else if (sel == 0 && !caretVisible) CreateCaret();

            SetPosition(pos, cp);
            SetSelectionLength(sel);

            UpdateCaret();
            Invalidate();
        }

        #endregion

        #region Positioning methods

        private void UpdateRectanglePositioning()
        {
            // calc char size
            SizeF charSize = this.CreateGraphics().MeasureString("A", Font, 100, _stringFormat);
            _charSize = new SizeF((float)Math.Ceiling(charSize.Width), (float)Math.Ceiling(charSize.Height));

            // calc content bounds
            _recContent = ClientRectangle;
            _recContent.X += recBorderLeft;
            _recContent.Y += recBorderTop;
            _recContent.Width -= recBorderRight + recBorderLeft;
            _recContent.Height -= recBorderBottom + recBorderTop;

            if (_vScrollBarVisible)
            {
                _recContent.Width -= vScrollBar.Width;
                vScrollBar.Left = _recContent.X + _recContent.Width;
                vScrollBar.Top = _recContent.Y;
                vScrollBar.Height = _recContent.Height;
            }

            int marginLeft = 4;

            // calc line info bounds
            if (_lineInfoVisible)
            {
                _recLineInfo = new Rectangle(_recContent.X + marginLeft,
                    _recContent.Y,
                    (int)(_charSize.Width * 10),
                    _recContent.Height);
            }
            else
            {
                _recLineInfo = Rectangle.Empty;
                _recLineInfo.X = marginLeft;
            }

            // calc hex bounds and grid
            _recHex = new Rectangle(_recLineInfo.X + _recLineInfo.Width,
                _recLineInfo.Y,
                _recContent.Width - _recLineInfo.Width,
                _recContent.Height);

            if (UseFixedBytesPerLine)
            {
                SetHorizontalByteCount(_bytesPerLine);
                _recHex.Width = (int)Math.Floor(((double)hexMaxHBytes) * _charSize.Width * 3 + (2 * _charSize.Width));
            }
            else
            {
                int hmax = (int)Math.Floor((double)_recHex.Width / (double)_charSize.Width);
                if (hmax > 1)
                    SetHorizontalByteCount((int)Math.Floor((double)hmax / 3));
                else SetHorizontalByteCount(hmax);
            }

            if (_stringViewVisible)
            {
                _recStringView = new Rectangle(_recHex.X + _recHex.Width,
                    _recHex.Y,
                    (int)(_charSize.Width * hexMaxHBytes),
                    _recHex.Height);
            }
            else _recStringView = Rectangle.Empty;

            int vmax = (int)Math.Floor((double)_recHex.Height / (double)_charSize.Height);
            SetVerticalByteCount(vmax);

            hexMaxBytes = hexMaxHBytes * hexMaxVBytes;

            UpdateScrollSize();
        }

        private PointF GetBytePointF(long byteIndex)
        {
            Point gp = GetGridBytePoint(byteIndex);
            return GetBytePointF(gp);
        }

        private PointF GetBytePointF(Point gp)
        {
            float x = (3 * _charSize.Width) * gp.X + _recHex.X;
            float y = (gp.Y + 1) * _charSize.Height - _charSize.Height + _recHex.Y;

            return new PointF(x, y);
        }

        private PointF GetByteStringPointF(Point gp)
        {
            float x = (_charSize.Width) * gp.X + _recStringView.X;
            float y = (gp.Y + 1) * _charSize.Height - _charSize.Height + _recStringView.Y;

            return new PointF(x, y);
        }

        private Point GetGridBytePoint(long byteIndex)
        {
            int row = (int)Math.Floor((double)byteIndex / (double)hexMaxHBytes);
            int column = (int)(byteIndex + hexMaxHBytes - hexMaxHBytes * (row + 1));

            Point res = new Point(column, row);
            return res;
        }

        #endregion

        #region Key interpreter methods

        private void ActivateEmptyKeyInterpreter()
        {
            if (emptyKeyInterpreter == null)
                emptyKeyInterpreter = new EmptyKeyInterpreter(this);

            if (emptyKeyInterpreter == currentKeyInterpreter)
                return;

            if (currentKeyInterpreter != null)
                currentKeyInterpreter.Deactivate();

            currentKeyInterpreter = emptyKeyInterpreter;
            currentKeyInterpreter.Activate();
        }

        private void ActivateKeyInterpreter()
        {
            if (keyInterpreter == null)
                keyInterpreter = new KeyInterpreter(this);

            if (keyInterpreter == currentKeyInterpreter)
                return;

            if (currentKeyInterpreter != null)
                currentKeyInterpreter.Deactivate();

            currentKeyInterpreter = keyInterpreter;
            currentKeyInterpreter.Activate();
        }

        private void ActivateStringKeyInterpreter()
        {
            if (stringKeyInterpreter == null)
                stringKeyInterpreter = new StringKeyInterpreter(this);

            if (stringKeyInterpreter == currentKeyInterpreter)
                return;

            if (currentKeyInterpreter != null)
                currentKeyInterpreter.Deactivate();

            currentKeyInterpreter = stringKeyInterpreter;
            currentKeyInterpreter.Activate();
        }

        #endregion

        #region Caret methods

        private void CreateCaret()
        {
            if (_byteProvider == null || currentKeyInterpreter == null || caretVisible || !this.Focused)
                return;

            System.Diagnostics.Debug.WriteLine("CreateCaret()", "HexBox");

            NativeMethods.CreateCaret(Handle, IntPtr.Zero, 1, (int)_charSize.Height);

            UpdateCaret();

            NativeMethods.ShowCaret(Handle);

            caretVisible = true;
        }

        private void UpdateCaret()
        {
            if (_byteProvider == null || currentKeyInterpreter == null)
                return;

            System.Diagnostics.Debug.WriteLine("UpdateCaret()", "HexBox");

            long byteIndex = bytePosition - startByte;
            PointF p = currentKeyInterpreter.GetCaretPointF(byteIndex);
            p.X += byteCharacterPosition * _charSize.Width;
            NativeMethods.SetCaretPos((int)p.X, (int)p.Y);
        }

        private void DestroyCaret()
        {
            if (!caretVisible)
                return;

            System.Diagnostics.Debug.WriteLine("DestroyCaret()", "HexBox");

            NativeMethods.DestroyCaret();
            caretVisible = false;
        }

        private void SetCaretPosition(Point p)
        {
            System.Diagnostics.Debug.WriteLine("SetCaretPosition()", "HexBox");

            if (_byteProvider == null || currentKeyInterpreter == null)
                return;

            long pos = bytePosition;
            int cp = byteCharacterPosition;

            if (_recHex.Contains(p))
            {
                BytePositionInfo bpi = GetHexBytePositionInfo(p);
                pos = bpi.Index;
                cp = bpi.Location;

                SetPosition(pos, cp);

                ActivateKeyInterpreter();
                UpdateCaret();
                Invalidate();
            }
            else if (_recStringView.Contains(p))
            {
                BytePositionInfo bpi = GetStringBytePositionInfo(p);
                pos = bpi.Index;
                cp = bpi.Location;

                SetPosition(pos, cp);

                ActivateStringKeyInterpreter();
                UpdateCaret();
                Invalidate();
            }
        }

        private BytePositionInfo GetHexBytePositionInfo(Point p)
        {
            System.Diagnostics.Debug.WriteLine("GetHexBytePositionInfo()", "HexBox");

            long bytePos;
            int byteCharaterPos;

            float x = ((float)(p.X - _recHex.X) / _charSize.Width);
            float y = ((float)(p.Y - _recHex.Y) / _charSize.Height);
            int iX = (int)x;
            int iY = (int)y;

            int hPos = (iX / 3 + 1);

            bytePos = Math.Min(_byteProvider.Length,
                startByte + (hexMaxHBytes * (iY + 1) - hexMaxHBytes) + hPos - 1);
            byteCharaterPos = (iX % 3);
            if (byteCharaterPos > 1)
                byteCharaterPos = 1;

            if (bytePos == _byteProvider.Length)
                byteCharaterPos = 0;

            if (bytePos < 0)
                return new BytePositionInfo(0, 0);
            return new BytePositionInfo(bytePos, byteCharaterPos);
        }

        private BytePositionInfo GetStringBytePositionInfo(Point p)
        {
            System.Diagnostics.Debug.WriteLine("GetStringBytePositionInfo()", "HexBox");

            long bytePos;
            int byteCharacterPos;

            float x = ((float)(p.X - _recStringView.X) / _charSize.Width);
            float y = ((float)(p.Y - _recStringView.Y) / _charSize.Height);
            int iX = (int)x;
            int iY = (int)y;

            int hPos = iX + 1;

            bytePos = Math.Min(_byteProvider.Length,
                startByte + (hexMaxHBytes * (iY + 1) - hexMaxHBytes) + hPos - 1);
            byteCharacterPos = 0;

            if (bytePos < 0)
                return new BytePositionInfo(0, 0);
            return new BytePositionInfo(bytePos, byteCharacterPos);
        }

        #endregion

        #region PreProcessMessage methods

        /// <summary>
        /// Preprocesses windows messages.
        /// </summary>
        /// <param name="m">the message to process.</param>
        /// <returns>true, if the message was processed</returns>
        [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true),
        SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
        public override bool PreProcessMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case NativeMethods.WM_KEYDOWN:
                    return currentKeyInterpreter.PreProcessWmKeyDown(ref m);
                case NativeMethods.WM_CHAR:
                    return currentKeyInterpreter.PreProcessWmChar(ref m);
                case NativeMethods.WM_KEYUP:
                    return currentKeyInterpreter.PreProcessWmKeyUp(ref m);
                default: return base.PreProcessMessage(ref m);
            }
        }

        /// <summary>
        /// Bases the pre process message.
        /// </summary>
        /// <param name="m">The m.</param>
        /// <returns></returns>
        private bool BasePreProcessMessage(ref Message m)
        {
            return base.PreProcessMessage(ref m);
        }

        #endregion

        #region Find methods

        /// <summary>
        /// Searches the current ByteProvider
        /// </summary>
        /// <param name="bytes">the array of bytes to find</param>
        /// <param name="startIndex">the start index</param>
        /// <returns>the SelectionStart property value if find was successfull or
        /// -1 if there is no match
        /// -2 if Find was aborted.</returns>
        public long Find(byte[] bytes, long startIndex)
        {
            int match = 0;
            int bytesLength = bytes.Length;

            abortFind = false;

            for (long pos = startIndex; pos < _byteProvider.Length; pos++)
            {
                if (abortFind)
                    return -2;

                if (pos % 1000 == 0) // for performance reasons: DoEvents only 1 times per 1000 loops
                    Application.DoEvents();

                if (_byteProvider.ReadByte(pos) != bytes[match])
                {
                    pos -= match;
                    match = 0;
                    findingPos = pos;
                    continue;
                }

                match++;

                if (match == bytesLength)
                {
                    long bytePos = pos - bytesLength + 1;
                    Select(bytePos, bytesLength);
                    ScrollByteIntoView(bytePosition + _selectionLength);
                    ScrollByteIntoView(bytePosition);

                    return bytePos;
                }
            }

            return -1;
        }

        /// <summary>
        /// Aborts a working Find method.
        /// </summary>
        public void AbortFind() { abortFind = true; }

        #endregion

        #region Copy, Cut and Paste methods

        /// <summary>
        /// Copies the current selection in the hex box to the Clipboard.
        /// </summary>
        public void Copy()
        {
            if (!CanCopy()) return;

            // put bytes into buffer
            byte[] buffer = new byte[_selectionLength];
            int id = -1;

            for (long i = bytePosition; i < bytePosition + _selectionLength; i++)
            {
                id++;
                buffer[id] = _byteProvider.ReadByte(i);
            }

            DataObject da = new DataObject();

            // set string buffer clipbard data
            string sBuffer = System.Text.Encoding.ASCII.GetString(buffer, 0, buffer.Length);
            da.SetData(typeof(string), sBuffer);

            //set memorystream (BinaryData) clipboard data
            System.IO.MemoryStream ms = new System.IO.MemoryStream(buffer, 0, buffer.Length, false, true);
            da.SetData("BinaryData", ms);

            Clipboard.SetDataObject(da, true);
            UpdateCaret();
            ScrollByteIntoView();
            Invalidate();
        }

        /// <summary>
        /// Return true if Copy method could be invoked.
        /// </summary>
        public bool CanCopy()
        {
            if (_selectionLength < 1 || _byteProvider == null) return false;
            return true;
        }

        /// <summary>
        /// Moves the current selection in the hex box to the Clipboard.
        /// </summary>
        public void Cut()
        {
            if (!CanCut()) return;

            Copy();

            _byteProvider.DeleteBytes(bytePosition, _selectionLength);
            byteCharacterPosition = 0;
            UpdateCaret();
            ScrollByteIntoView();
            ReleaseSelection();
            Invalidate();
            Refresh();
        }

        /// <summary>
        /// Return true if Cut method could be invoked.
        /// </summary>
        public bool CanCut()
        {
            if (_byteProvider == null) return false;
            if (_selectionLength < 1 || !_byteProvider.SupportsDeleteBytes) return false;
            return true;
        }

        /// <summary>
        /// Replaces the current selection in the hex box with the contents of the Clipboard.
        /// </summary>
        public void Paste()
        {
            if (!CanPaste()) return;

            if (_selectionLength > 0)
                _byteProvider.DeleteBytes(bytePosition, _selectionLength);

            byte[] buffer = null;
            IDataObject da = Clipboard.GetDataObject();

            if (da.GetDataPresent("BinaryData"))
            {
                System.IO.MemoryStream ms = (System.IO.MemoryStream)da.GetData("BinaryData");
                buffer = new byte[ms.Length];
                ms.Read(buffer, 0, buffer.Length);
            }
            else if (da.GetDataPresent(typeof(string)))
            {
                string sBuffer = (string)da.GetData(typeof(string));
                buffer = System.Text.Encoding.ASCII.GetBytes(sBuffer);
            }
            else return;

            _byteProvider.InsertBytes(bytePosition, buffer);

            SetPosition(bytePosition + buffer.Length, 0);

            ReleaseSelection();
            ScrollByteIntoView();
            UpdateCaret();
            Invalidate();
        }

        /// <summary>
        /// Return true if Paste method could be invoked.
        /// </summary>
        public bool CanPaste()
        {
            if (_byteProvider == null || !_byteProvider.SupportsInsertBytes) return false;
            if (!_byteProvider.SupportsDeleteBytes && _selectionLength > 0) return false;

            IDataObject da = Clipboard.GetDataObject();
            if (da.GetDataPresent("BinaryData")) return true;
            else if (da.GetDataPresent(typeof(string))) return true;
            else return false;
        }

        #endregion

        #region Misc

        void SetPosition(long bytePos)
        {
            SetPosition(bytePos, byteCharacterPosition);
        }

        void SetPosition(long bytePos, int characterPos)
        {
            if (byteCharacterPosition != characterPos)
            {
                byteCharacterPosition = characterPos;
            }

            if (bytePos != bytePosition)
            {
                bytePosition = bytePos;
                CheckCurrentLineChanged();
                CheckCurrentPositionInLineChanged();

                OnSelectionStartChanged(EventArgs.Empty);
            }
        }

        void SetSelectionLength(long selectionLength)
        {
            if (selectionLength != _selectionLength)
            {
                _selectionLength = selectionLength;
                OnSelectionLengthChanged(EventArgs.Empty);
            }
        }

        void SetHorizontalByteCount(int value)
        {
            if (hexMaxHBytes == value)
                return;

            hexMaxHBytes = value;
            OnHorizontalByteCountChanged(EventArgs.Empty);
        }

        void SetVerticalByteCount(int value)
        {
            if (hexMaxVBytes == value)
                return;

            hexMaxVBytes = value;
            OnVerticalByteCountChanged(EventArgs.Empty);
        }

        void CheckCurrentLineChanged()
        {
            long currentLine = (long)Math.Floor((double)bytePosition / (double)hexMaxHBytes) + 1;

            if (_byteProvider == null && _currentLine != 0)
            {
                _currentLine = 0;
                OnCurrentLineChanged(EventArgs.Empty);
            }
            else if (currentLine != _currentLine)
            {
                _currentLine = currentLine;
                OnCurrentLineChanged(EventArgs.Empty);
            }
        }

        void CheckCurrentPositionInLineChanged()
        {
            Point gb = GetGridBytePoint(bytePosition);
            int currentPositionInLine = gb.X + 1;

            if (_byteProvider == null && _currentPositionInLine != 0)
            {
                _currentPositionInLine = 0;
                OnCurrentPositionInLineChanged(EventArgs.Empty);
            }
            else if (currentPositionInLine != _currentPositionInLine)
            {
                _currentPositionInLine = currentPositionInLine;
                OnCurrentPositionInLineChanged(EventArgs.Empty);
            }
        }

        private bool VisualStylesEnabled()
        {
            return VisualStyleInformation.IsSupportedByOS &&
                VisualStyleInformation.IsEnabledByUser &&
                (Application.VisualStyleState == VisualStyleState.ClientAreaEnabled ||
                Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled);

            //OperatingSystem os = Environment.OSVersion;
            //bool isAppropriateOS = os.Platform == PlatformID.Win32NT && ((os.Version.Major == 5 && os.Version.Minor >= 1) || os.Version.Major > 5);
            //bool osFeatureThemesPresent = false;
            //bool osThemeDLLAvailable = false;

            //if (isAppropriateOS)
            //{
            //    Version osThemeVersion = OSFeature.Feature.GetVersionPresent(OSFeature.Themes);
            //    osFeatureThemesPresent = osThemeVersion != null;

            //    DLLVersionInfo dllVersion = new DLLVersionInfo();
            //    dllVersion.cbSize = Marshal.SizeOf(typeof(DLLVersionInfo));
            //    int temp = Interop.DllGetVersion(ref dllVersion);
            //    osThemeDLLAvailable = dllVersion.dwMajorVersion >= 6;
            //}

            //return isAppropriateOS && osFeatureThemesPresent && osThemeDLLAvailable && Interop.IsAppThemed() && Interop.IsThemeActive();
        }

        /// <summary>
        /// Raises the ReadOnlyChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnReadOnlyChanged(EventArgs e)
        {
            if (ReadOnlyChanged != null)
                ReadOnlyChanged(this, e);
        }

        /// <summary>
        /// Raises the ByteProviderChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnByteProviderChanged(EventArgs e)
        {
            if (ByteProviderChanged != null)
                ByteProviderChanged(this, e);
        }

        /// <summary>
        /// Raises the SelectionStartChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnSelectionStartChanged(EventArgs e)
        {
            if (SelectionStartChanged != null)
                SelectionStartChanged(this, e);
        }

        /// <summary>
        /// Raises the SelectionLengthChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnSelectionLengthChanged(EventArgs e)
        {
            if (SelectionLengthChanged != null)
                SelectionLengthChanged(this, e);
        }

        /// <summary>
        /// Raises the LineInfoVisibleChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnLineInfoVisibleChanged(EventArgs e)
        {
            if (LineInfoVisibleChanged != null)
                LineInfoVisibleChanged(this, e);
        }

        /// <summary>
        /// Raises the StringViewVisibleChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnStringViewVisibleChanged(EventArgs e)
        {
            if (StringViewVisibleChanged != null)
                StringViewVisibleChanged(this, e);
        }

        /// <summary>
        /// Raises the BorderStyleChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnBorderStyleChanged(EventArgs e)
        {
            if (BorderStyleChanged != null)
                BorderStyleChanged(this, e);
        }

        /// <summary>
        /// Raises the UseFixedBytesPerLineChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnUseFixedBytesPerLineChanged(EventArgs e)
        {
            if (UseFixedBytesPerLineChanged != null)
                UseFixedBytesPerLineChanged(this, e);
        }

        /// <summary>
        /// Raises the BytesPerLineChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnBytesPerLineChanged(EventArgs e)
        {
            if (BytesPerLineChanged != null)
                BytesPerLineChanged(this, e);
        }

        /// <summary>
        /// Raises the VScrollBarVisibleChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnVScrollBarVisibleChanged(EventArgs e)
        {
            if (VScrollBarVisibleChanged != null)
                VScrollBarVisibleChanged(this, e);
        }

        /// <summary>
        /// Raises the HexCasingChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnCasingChanged(EventArgs e)
        {
            if (CasingChanged != null)
                CasingChanged(this, e);
        }

        /// <summary>
        /// Raises the HorizontalByteCountChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnHorizontalByteCountChanged(EventArgs e)
        {
            if (HorizontalByteCountChanged != null)
                HorizontalByteCountChanged(this, e);
        }

        /// <summary>
        /// Raises the VerticalByteCountChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnVerticalByteCountChanged(EventArgs e)
        {
            if (VerticalByteCountChanged != null)
                VerticalByteCountChanged(this, e);
        }

        /// <summary>
        /// Raises the CurrentLineChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnCurrentLineChanged(EventArgs e)
        {
            if (CurrentLineChanged != null)
                CurrentLineChanged(this, e);
        }

        /// <summary>
        /// Raises the CurrentPositionInLineChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnCurrentPositionInLineChanged(EventArgs e)
        {
            if (CurrentPositionInLineChanged != null)
                CurrentPositionInLineChanged(this, e);
        }

        /// <summary>
        /// Raises the MouseDown event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnMouseDown()", "HexBox");

            if (!Focused) Focus();
            SetCaretPosition(new Point(e.X, e.Y));
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Raises the MouseWhell event
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            int linesToScroll = -(e.Delta * SystemInformation.MouseWheelScrollLines / 120);
            PerformScrollLines(linesToScroll);

            base.OnMouseWheel(e);
        }

        /// <summary>
        /// Raises the Resize event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateRectanglePositioning();
        }

        /// <summary>
        /// Raises the GotFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnGotFocus(EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnGotFocus()", "HexBox");

            base.OnGotFocus(e);
            CreateCaret();
        }

        /// <summary>
        /// Raises the LostFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnLostFocus(EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnLostFocus()", "HexBox");

            base.OnLostFocus(e);
            DestroyCaret();
        }

        private void OnByteProviderLengthChanged(object sender, EventArgs e)
        {
            UpdateScrollSize();
        }

        #endregion
    }
}
