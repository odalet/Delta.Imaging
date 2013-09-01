using System;
using System.IO;
using Delta.CapiNet.Pem;
using Delta.CertXplorer.Extensibility;

namespace PemPlugin
{
    internal class PemHandlerPlugin : BaseDataHandlerPlugin
    {
        private class PemHandler : IDataHandler
        {
            private PemHandlerPlugin plugin = null;
            private byte[] fileContent = null;
            private string FileName { get; set; }

            public PemHandler(PemHandlerPlugin parent)
            {
                if (parent == null) throw new ArgumentNullException("parent");
                plugin = parent;
            }

            #region IDataHandler Members            

            public bool CanHandleFile(string filename)
            {
                if (!File.Exists(filename)) throw new FileNotFoundException(string.Format(
                    "File {0} could not be found;", filename));
                
                FileName = filename;
                fileContent = File.ReadAllBytes(filename);

                return PemDecoder.IsPemData(fileContent);
            }

            public IData ProcessFile()
            {
                if (fileContent == null) throw new InvalidOperationException("Invalid input data: null");

                var decoder = new PemDecoder();
                var result = decoder.ReadData(fileContent);
                if (result == null)
                {
                    foreach (var error in decoder.Errors)
                        plugin.Log.Error(error);
                    return null;
                }

                foreach (var warning in result.Warnings)
                    plugin.Log.Warning(warning);

                return new PemData(result);
            }

            #endregion
        }

        private static readonly IPluginInfo pluginInfo = new PemHandlerPluginInfo();

        public override IDataHandler CreateHandler()
        {
            return new PemHandler(this);
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
