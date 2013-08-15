using System;
using System.Threading;
using System.Security.Principal;
using System.ComponentModel;

namespace Delta.CertXplorer.ThisImplementation
{
    /// <summary>
    /// Provides access to the information about the current user.
    /// </summary>
    public class ThisUser
    {
        /// <summary>
        /// Gets or sets the current principal (for role-based security).
        /// </summary>
        /// <value>A <see cref="System.Security.Principal.IPrincipal"/> value 
        /// representing the security context.</value>
        public IPrincipal CurrentPrincipal
        {
            get { return Thread.CurrentPrincipal; }
            set { Thread.CurrentPrincipal = value; }
        }

        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the user was authenticated; otherwise, <c>false</c>.
        /// </value>
        public bool IsAuthenticated
        {
            get { return Thread.CurrentPrincipal.Identity.IsAuthenticated; }
        }

        /// <summary>
        /// Gets a value indicating whether this user is represented by a windows principal.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the user's principal is a windows principal; otherwise, <c>false</c>.
        /// </value>
        public bool IsWindowsPrincipal
        {
            get { return Thread.CurrentPrincipal is WindowsPrincipal; }
        }

        /// <summary>
        /// Gets the name of the current user. 
        /// </summary>
        /// <value>The name of the current user.</value>
        public string Name
        {
            get { return Thread.CurrentPrincipal.Identity.Name; }
        }

        /// <summary>
        /// Determines whether the current user belongs to the specified role. 
        /// </summary>
        /// <param name="role">The role for which to check membership.</param>
        /// <returns>
        /// 	<c>true</c> if the current user is a member of the specified role; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInRole(WindowsBuiltInRole role)
        {
            if (IsWindowsPrincipal)
                return ((WindowsPrincipal)Thread.CurrentPrincipal).IsInRole(role);
            return Thread.CurrentPrincipal.IsInRole(role.ToString());
        }

        /// <summary>
        /// Determines whether the current user belongs to the specified role. 
        /// </summary>
        /// <param name="role">The role for which to check membership.</param>
        /// <returns>
        /// 	<c>true</c> if the current user is a member of the specified role; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInRole(string role)
        {
            return Thread.CurrentPrincipal.IsInRole(role);
        }

        /// <summary>
        /// Sets the thread's current principal to the Windows user that started the application.
        /// </summary>
        public void InitializeWithWindowsUser()
        {
            Thread.CurrentPrincipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
        }
    }
}
