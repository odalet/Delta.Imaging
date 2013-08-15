using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delta.CertXplorer.ApplicationModel.Services
{
    public interface ISessionService
    {
        object CurrentSession {get;}

        long SessionId { get; }

        bool IsConnected { get; }

        void Disconnect();

        void SetCurrentSession(object session);

        string SiteCode{ get; }

        string UserLogin{ get; }
      
    }
}
