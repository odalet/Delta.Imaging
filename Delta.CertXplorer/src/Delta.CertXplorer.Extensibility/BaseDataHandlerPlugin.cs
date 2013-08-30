namespace Delta.CertXplorer.Extensibility
{
    public abstract class BaseDataHandlerPlugin : BasePlugin, IDataHandlerPlugin
    {
        #region IDataHandlerPlugin Members

        public abstract IDataHandler CreateHandler();

        #endregion
    }
}
