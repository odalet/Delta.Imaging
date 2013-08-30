using System;
using System.IO;
using System.Linq;
using System.Text;

using Delta.CertXplorer.Extensibility;

namespace PemPlugin
{
    internal class PemHandlerPlugin : BaseDataHandlerPlugin
    {
        private class PemHandler : IDataHandler
        {
            private byte[] data = null;
            private string FileName { get; set; }

            #region IDataHandler Members            

            public bool CanHandleFile(string filename)
            {
                if (!File.Exists(filename)) throw new FileNotFoundException(string.Format(
                    "File {0} could not be found;", filename));

                FileName = filename;
                data = File.ReadAllBytes(filename);

                var s = Encoding.ASCII.GetString(data);
                return s.StartsWith("-----BEGIN") || s.StartsWith("---- BEGIN");
            }

            public IData ProcessFile()
            {
                if (data == null) throw new InvalidOperationException("Invalid input data: null");

                var strings = Encoding.ASCII.GetString(data).Split('\r', '\n').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

                var b64 = string.Join("", strings.Where(s => !s.StartsWith("----")));
                var workload = Convert.FromBase64String(b64);
                
                return new PemData()
                {
                    MainData    = workload,
                    Header = strings[0],
                    Footer = strings[strings.Length - 1]
                };
            }

            #endregion
        }

        private static readonly IPluginInfo pluginInfo = new PemHandlerPluginInfo();

        public override IDataHandler CreateHandler()
        {
            return new PemHandler();
        }

        public override IPluginInfo PluginInfo
        {
            get { return pluginInfo; }
        }

        protected override Guid PluginId
        {
            get { return pluginInfo.Id; }
        }

        protected override string PluginName
        {
            get { return pluginInfo.Name; }
        }
    }
}
