using System;

namespace Delta.CertXplorer.UI
{
    public enum EditActionState : byte
    {
        None = 0,
        Cut = 1,
        Copied = 2,
        Deleted = 3
    }
}
