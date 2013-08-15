using System;

namespace Delta.CertXplorer
{
    /// <summary>
    /// Defines what kind of application <see cref="This.Application"/> represents.
    /// </summary>
    [Flags]
    public enum ThisApplicationType : int
    {
        /// <summary><see cref="This.Application"/> type is not set.</summary>
        NotSet = 0x00,

        /// <summary><see cref="This.Application"/> is a console application.</summary>
        ConsoleApplication = 0x01,

        /// <summary><see cref="This.Application"/> is a Windows Forms application.</summary>
        WindowsFormsApplication = 0x02,

        /// <summary><see cref="This.Application"/> is a Windows service.</summary>
        WindowsServiceApplication = 0x04,

        /// <summary><see cref="This.Application"/> is an ASP.NET application.</summary>
        WebApplication = 0x08,

        /// <summary><see cref="This.Application"/> is a WCF service.</summary>
        WcfService = 0x10
    }
}
