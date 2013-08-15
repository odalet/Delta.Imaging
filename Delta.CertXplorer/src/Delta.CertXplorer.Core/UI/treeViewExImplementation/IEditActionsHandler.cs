using System;

namespace Delta.CertXplorer.UI
{
    public interface IEditActionsHandler
    {
        EditActionState EditActionState { get; set; }

        bool CanCut { get; }
        bool CanCopy { get; }
        bool CanPaste { get; }
        bool CanDelete { get; }
        bool CanSelectAll { get; }

        void Cut();
        void Copy();
        void Paste();
        void Delete();
        void SelectAll();
    }
}
