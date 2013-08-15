using System;
using System.ComponentModel.Design;
using System.Collections.Generic;

namespace Delta.CertXplorer.ComponentModel
{
    /// <summary>
    /// Defines a specific <see cref="System.IServiceProvider"/> that
    /// is able to provide the list of all the service types it contains.
    /// </summary>
    public interface IListableServiceProvider : IServiceProvider
    {
        /// <summary>
        /// Gets the list of the service types contained in this service provider.
        /// </summary>
        /// <returns>A list of service types.</returns>
        IList<Type> GetServicesList();

        /// <summary>
        /// Gets the list of the service types contained in this service provider.
        /// </summary>
        /// <remarks>
        /// Because a service type can appear both in a service container and in its parent service provider,
        /// the returned list doesn't contain unique types.
        /// </remarks>
        /// <param name="promote">
        /// If set to <c>true</c>, then recurse the hierarchy of parent service providers
        /// and tries to get their service types list.
        /// </param>
        /// <returns>A list of service types.</returns>
        IList<Type> GetServicesList(bool promote);
    }
}
