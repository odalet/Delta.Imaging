using System;
using System.IO;
using System.Web;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Globalization;
using System.Deployment.Application;
using System.Collections.ObjectModel;

using Delta.CertXplorer.ComponentModel;

namespace Delta.CertXplorer.ThisImplementation
{
    /// <summary>
    /// Provides properties, methods, and events related to the current application.
    /// </summary>
    public partial class ThisApplication : BasePropertyNotifier
    {
        private AssemblyInfo info = null;
        private ReadOnlyCollection<string> commandLineArgs = null;
        private ThisApplicationType applicationType = ThisApplicationType.NotSet;

        private bool isSingleInstanceWasSet = false;
        private bool isSingleInstance = false;
        private bool isWcfService = false;

        public ThisApplicationType ApplicationType { get { return applicationType; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThisApplication"/> class.
        /// </summary>
        public ThisApplication()
        {
            // Initialize application type based on the contexts test.
            isWcfService = false;
            applicationType = ThisApplicationType.NotSet;

            if (IsWebContext)
                applicationType = ThisApplicationType.WebApplication;
            else if (IsWcfContext)
            {
                applicationType = ThisApplicationType.WcfService;
                isWcfService = true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current application is running in a web context.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the current application is running in a web context; otherwise, <c>false</c>.
        /// </value>
        public bool IsWebContext { get { return This.IsWebContext; } }

        /// <summary>
        /// Gets a value indicating whether the current application is running in a WCF context.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the current application is running in a WCF context; otherwise, <c>false</c>.
        /// </value>
        public bool IsWcfContext { get { return This.IsWcfContext; } }

        /// <summary>
        /// Gets a value indicating whether this application is hosting a WCF service.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this application is hosting a WCF service; otherwise, <c>false</c>.
        /// </value>
        public bool IsWcfService
        {
            get { return isWcfService; }
            internal set { isWcfService = value; }
        }

        /// <summary>
        /// Gets or sets an action to be executed just before the application exits.
        /// </summary>
        /// <remarks>
        /// This property is called only in the case of a Windows Forms application.
        /// </remarks>
        /// <value>An <see cref="System.Action"/> instance.</value>
        public Action OnApplicationExit { get; set; }

        /// <summary>
        /// Determines whether this application is a single-instance application.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance this application is a single-instance application; otherwise, <c>false</c>.
        /// </value>
        public bool IsSingleInstance
        {
            get { return isSingleInstance; }
            set 
            {
                if (isSingleInstanceWasSet) throw new ApplicationException(
                    "Property ThisApplication.IsSingleInstance can only be set once.");

                isSingleInstance = value;
            }
        }

        /// <summary>
        /// Sets both the current culture and the current UI culture.
        /// </summary>
        /// <param name="culture">The culture to affect.</param>
        public void SetBothCultures(CultureInfo culture)
        {
            Culture = culture;
            UICulture = culture;
        }

        /// <summary>
        /// Gets or sets the culture that the current thread uses for 
        /// string manipulation and string formatting.
        /// </summary>
        public CultureInfo Culture
        {
            get { return Thread.CurrentThread.CurrentCulture; }
            set 
            {
                if (value != Thread.CurrentThread.CurrentCulture)
                {
                    base.OnPropertyChanging("Culture");
                    Thread.CurrentThread.CurrentCulture = value;
                    base.OnPropertyChanged("Culture");
                }
            }
        }

        /// <summary>
        /// Gets or sets the culture that the current thread uses for 
        /// retrieving culture-specific resources.
        /// </summary>
        public CultureInfo UICulture
        {
            get { return Thread.CurrentThread.CurrentUICulture; }
            set
            {
                if (value != Thread.CurrentThread.CurrentUICulture)
                {
                    base.OnPropertyChanging("UICulture");
                    Thread.CurrentThread.CurrentUICulture = value;
                    base.OnPropertyChanged("UICulture");
                }
            }
        }

        public Assembly EntryAssembly
        {
            get
            {               
                // When using ASP.NET, Assembly.GetEntryAssembly() is null.
                Assembly entryAssembly = Assembly.GetEntryAssembly();
                if (entryAssembly == null)
                {
                    StackTrace stack = new StackTrace();

                    entryAssembly = Assembly.GetExecutingAssembly();
                    for (int current = 0; current < stack.FrameCount; current++)
                    {
                        var frame = stack.GetFrame(current);
                        var assembly = frame.GetMethod().Module.Assembly;
                        if (assembly.IsFrameworkAssembly())
                            return entryAssembly;
                        else entryAssembly = assembly;
                    }
                }

                return entryAssembly;
            }
        }

        /// <summary>
        /// Gets the root directory for this application.
        /// </summary>
        /// <value>The root directory.</value>
        public string RootDirectory
        {
            get
            {
                if ((applicationType == ThisApplicationType.WebApplication) && (HttpContext.Current != null))
                    return HttpContext.Current.Request.PhysicalApplicationPath;
                else
                {
                    string location = EntryAssembly.Location;
                    return Path.GetDirectoryName(location);
                }
            }
        }

        /// <summary>
        /// Gets the root binaries directory for this application.
        /// </summary>
        /// <remarks>
        /// This directory is equal to <see cref="RootDirectory"/> for all application types
        /// except for <see cref="ThisApplicationType.WebApplication"/> type: in this case, it is equal 
        /// to <see cref="RootDirectory"/> + <c>/bin</c>.
        /// </remarks>
        /// <value>The binaries directory.</value>
        public string BinariesDirectory
        {
            get
            {
                if (applicationType == ThisApplicationType.WebApplication)
                    return Path.Combine(RootDirectory, "bin");
                else return RootDirectory;
            }
        }

        /// <summary>
        /// Gets an object that provides properties for getting information 
        /// about the application's assembly, such as the version number, 
        /// description, and so on. 
        /// </summary>
        public AssemblyInfo Info
        {
            get
            {
                if (info == null) info = new AssemblyInfo(EntryAssembly);
                return info;
            }
        }

        /// <summary>
        /// Gets a collection containing the command-line arguments as 
        /// strings for the current application.
        /// </summary>
        /// <remarks>
        /// This colection doesn't contain the name of the application, only the arguments.
        /// </remarks>
        public ReadOnlyCollection<string> CommandLineArgs
        {
            get
            {
                if (commandLineArgs == null)
                {
                    string[] arguments = Environment.GetCommandLineArgs();
                    int len = arguments.Length;
                    if (len >= 2)
                    {
                        string[] destination = new string[len - 1];
                        arguments.CopyTo(destination, 1);
                        commandLineArgs = new ReadOnlyCollection<string>(destination);
                    }
                    else commandLineArgs = new ReadOnlyCollection<string>(new string[] { });
                }
                
                return commandLineArgs;
            }
        }

        /// <summary>
        /// Gets the current application's ClickOnce deployment object, 
        /// which provides support for updating the current deployment 
        /// programmatically and support for the on-demand download of 
        /// files.
        /// </summary>
        public ApplicationDeployment Deployment
        {
            get { return ApplicationDeployment.CurrentDeployment; }
        }

        /// <summary>
        /// Gets a value indicating whether the application was deployed from a network using ClickOnce.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the current application was deployed from a network; otherwise, <c>false</c>.
        /// </value>
        public bool IsNetworkDeployed
        {
            get { return ApplicationDeployment.IsNetworkDeployed; }
        }

        /// <summary>
        /// Returns the value of the specified environment variable.
        /// </summary>
        /// <param name="name">The name of the environment variable.</param>
        /// <returns>The value of the environment variable with the name <paramref name="name"/>.</returns>
        public string GetEnvironmentVariable(string name)
        {
            string environmentVariable = Environment.GetEnvironmentVariable(name);
            if (environmentVariable == null) throw new ArgumentException(
                "Environment Variable not found", "name");

            return environmentVariable;
        }

        /// <summary>
        /// Changes the culture used by the current thread for 
        /// string manipulation and for string formatting.
        /// </summary>
        /// <param name="cultureName">Name of the culture as a string. 
        /// For a list of possible names, see <see cref="System.Globalization.CultureInfo"/>.</param>
        public void ChangeCulture(string cultureName)
        {
            base.OnPropertyChanging("Culture");
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            base.OnPropertyChanged("Culture");
        }

        /// <summary>
        /// Changes the culture that the current thread uses for 
        /// retrieving culture-specific resources.
        /// </summary>
        /// <param name="cultureName">Name of the culture as a string. 
        /// For a list of possible names, see <see cref="System.Globalization.CultureInfo"/>.</param>
        public void ChangeUICulture(string cultureName)
        {
            base.OnPropertyChanging("UICulture");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
            base.OnPropertyChanged("UICulture");
        }

        #region Application Type specific Run methods

        /// <summary>
        /// Runs this application as a console application.
        /// </summary>
        public void Run()
        {

        }

        /// <summary>
        /// Runs this application by launching the specified form.
        /// </summary>
        /// <param name="form">The application's main form.</param>
        public void Run(System.Windows.Forms.Form form)
        {
            if (form == null) throw new ArgumentNullException("form");

            if (applicationType != ThisApplicationType.WindowsFormsApplication)
                throw new ApplicationException(string.Format("Bad application type: {0}; should be {1}", 
                    applicationType, ThisApplicationType.WindowsFormsApplication));

            if (IsSingleInstance) SingleInstanceManager.Run(form);
            else System.Windows.Forms.Application.Run(form);
        }

        #endregion

        internal void SetApplicationType(ThisApplicationType type)
        {
            applicationType = type;
        }        
    }
}
