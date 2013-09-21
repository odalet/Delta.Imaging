using System;

namespace Delta.CertXplorer.CertManager.Wrappers
{
    internal abstract class BaseWrapper
    {
        protected static T TryGet<T>(Func<T> function)
        {
            try
            {
                return function();
            }
            catch (Exception ex)
            {
                This.Logger.Error(string.Format("Could not execute function: {0}", ex.Message), ex);
            }

            return default(T);
        }
    }
}
