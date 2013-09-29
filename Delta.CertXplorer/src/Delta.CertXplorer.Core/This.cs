using System;
using System.Threading;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel.Design;

using Delta.CertXplorer.UI;
using Delta.CertXplorer.Logging;
using Delta.CertXplorer.Diagnostics;
using Delta.CertXplorer.ComponentModel;
using Delta.CertXplorer.ThisImplementation;

namespace Delta.CertXplorer
{
    /// <summary>
    /// This class mimcs the <b>My</b> functionality available in the VB.Net language.
    /// </summary>
    public static class This
    {
        /// <summary>
        /// Allows for the delayed creation of a static property
        /// </summary>
        /// <typeparam name="T">Type of the object to host.</typeparam>
        private sealed class ThreadSafeObjectProvider<T> where T : class, new()
        {
            [ThreadStatic]
            private static T threadStaticValue = null;

            public T GetInstance()
            {
                if (threadStaticValue == null)
                    threadStaticValue = Activator.CreateInstance<T>();
                return threadStaticValue;
            }
        }

        private static bool wcfServiceWasSet = false;
        private static bool disposed = false;
        private static ObservableServiceContainer parentContainer = null;
        private static ObservableServiceContainer childContainer = null;

        private static readonly ThreadSafeObjectProvider<ThisUser> userObjectProvider =
            new ThreadSafeObjectProvider<ThisUser>();
        private static readonly ThreadSafeObjectProvider<ThisComputer> computerObjectProvider =
            new ThreadSafeObjectProvider<ThisComputer>();
        private static readonly ThreadSafeObjectProvider<ThisApplication> applicationObjectProvider =
            new ThreadSafeObjectProvider<ThisApplication>();

        private static ServiceContainer services = new ServiceContainer();

        /// <summary>
        /// Initializes the <see cref="This"/> class.
        /// </summary>
        static This()
        {
            parentContainer = new ObservableServiceContainer();
            childContainer = new ObservableServiceContainer(parentContainer);

            AddDefaultServices();

            WireEvents(); // Child service container events.

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(OnCurrentDomainUnhandledException);
            AppDomain.CurrentDomain.ProcessExit += delegate { DisposeClass(); };
        }

        #region InitializeConsoleApplication

        /// <summary>
        /// Initializes the current application as a Console application.
        /// </summary>
        public static void InitializeConsoleApplication()
        {
            InitializeConsoleApplication(string.Empty);
        }

        /// <summary>
        /// Initializes the current application as a Console application.
        /// </summary>
        /// <param name="culture">The application's culture.</param>
        /// <remarks>
        /// The culture passed to this method is affected to both the current culture and the current UI culture.
        /// </remarks>
        public static void InitializeConsoleApplication(string culture)
        {
            // Set the application type.
            Application.SetApplicationType(ThisApplicationType.ConsoleApplication);

            if (!string.IsNullOrEmpty(culture))
            {
                try
                {
                    var ci = new CultureInfo(culture);
                    Application.Culture = ci;
                    Application.UICulture = ci;
                }
                catch (Exception ex)
                {
                    This.Logger.Error(string.Format("Unable to set application's culture to {0}:\r\n {1}",
                        culture, ex.ToFormattedString()));
                }
            }
        }

        #endregion

        #region InitializeWebApplication

        /// <summary>
        /// Initializes the current application as a Web application.
        /// </summary>
        public static void InitializeWebApplication()
        {
            InitializeWebApplication(string.Empty);
        }

        /// <summary>
        /// Initializes the current application as a Web application.
        /// </summary>
        /// <param name="culture">The application's culture.</param>
        /// <remarks>
        /// The culture passed to this method is affected to both the current culture and the current UI culture.
        /// </remarks>
        public static void InitializeWebApplication(string culture)
        {
            // Set the application type.
            Application.SetApplicationType(ThisApplicationType.WebApplication);

            if (!string.IsNullOrEmpty(culture))
            {
                try
                {
                    var ci = new CultureInfo(culture);
                    Application.Culture = ci;
                    Application.UICulture = ci;
                }
                catch (Exception ex)
                {
                    This.Logger.Error(string.Format("Unable to set application's culture to {0}:\r\n {1}",
                        culture, ex.ToFormattedString()));
                }
            }
        }

        #endregion

        #region InitializeWindowsServiceApplication

        /// <summary>
        /// Initializes the current application as a Windows service.
        /// </summary>
        public static void InitializeWindowsServiceApplication()
        {
            InitializeWindowsServiceApplication(string.Empty);
        }

        /// <summary>
        /// Initializes the current application as a Windows service.
        /// </summary>
        /// <param name="culture">The application's culture.</param>
        /// <remarks>
        /// The culture passed to this method is affected to both the current culture and the current UI culture.
        /// </remarks>
        public static void InitializeWindowsServiceApplication(string culture)
        {
            // Set the application type.
            Application.SetApplicationType(ThisApplicationType.WindowsServiceApplication);

            if (!string.IsNullOrEmpty(culture))
            {
                try
                {
                    var ci = new CultureInfo(culture);
                    Application.Culture = ci;
                    Application.UICulture = ci;
                }
                catch (Exception ex)
                {
                    This.Logger.Error(string.Format("Unable to set application's culture to {0}:\r\n {1}",
                        culture, ex.ToFormattedString()));
                }
            }
        }

        #endregion

        #region InitializeWindowsFormsApplication

        /// <summary>
        /// Initializes the current application as a Windows Forms application.
        /// </summary>
        public static void InitializeWindowsFormsApplication()
        {
            InitializeWindowsFormsApplication(string.Empty, false);
        }

        /// <summary>
        /// Initializes the current application as a Windows Forms application.
        /// </summary>
        /// <param name="singleInstance">if set to <c>true</c> then the application will be a single-instance application.</param>
        public static void InitializeWindowsFormsApplication(bool singleInstance)
        {
            InitializeWindowsFormsApplication(string.Empty, singleInstance);
        }

        /// <summary>
        /// Initializes the current application as a Windows Forms application.
        /// </summary>
        /// <param name="culture">The application's culture.</param>
        /// <remarks>
        /// The culture passed to this method is affected to both the current culture and the current UI culture.
        /// </remarks>
        public static void InitializeWindowsFormsApplication(string culture)
        {
            InitializeWindowsFormsApplication(culture, false);
        }

        /// <summary>
        /// Initializes the current application as a Windows Forms application.
        /// </summary>
        /// <param name="culture">The application's culture.</param>
        /// <param name="singleInstance">
        /// if set to <c>true</c> then the application will be a single-instance application.
        /// </param>
        /// <remarks>
        /// The culture passed to this method is affected to both the current culture and the current UI culture.
        /// </remarks>
        public static void InitializeWindowsFormsApplication(string culture, bool singleInstance)
        {
            AttachThreadException();

            System.Windows.Forms.Application.ApplicationExit += new EventHandler(OnWindowsFormsApplicationExit);

            // Set the application type.
            Application.SetApplicationType(ThisApplicationType.WindowsFormsApplication);

            // Single instance?
            Application.IsSingleInstance = singleInstance;

            SetApplicationCulture(culture);

            // We also add a UI service (to the parent container, so that it can be replaced).
            parentContainer.AddService<ISimpleUIService>(new SimpleUIService());
        }

        public static void SetApplicationCulture(string culture)
        {
            if (string.IsNullOrEmpty(culture)) return;
            try
            {
                var ci = new CultureInfo(culture);
                Application.Culture = ci;
                Application.UICulture = ci;
            }
            catch (Exception ex)
            {
                This.Logger.Error(string.Format("Unable to set application's culture to {0}:\r\n {1}",
                    culture, ex.ToFormattedString()));
            }
        }

        /// <summary>
        /// Called when the windows forms application exits.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private static void OnWindowsFormsApplicationExit(object sender, EventArgs e)
        {
            if (Application.OnApplicationExit != null)
            {
                try
                {
                    // We want no exception to exit from here!
                    Application.OnApplicationExit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        This.Logger.Error(string.Format(
                            "An error occured while processing pre-exit action: {0}", ex.Message), ex);
                    }
                    catch (Exception ex2)
                    {
                        // Eaten exception!
                        var debugException = ex2;
                    }
                }
            }
        }

        internal static void AttachThreadException()
        {
            AttachThreadException(new ThreadExceptionEventHandler(OnApplicationThreadException));
        }

        internal static void AttachThreadException(ThreadExceptionEventHandler handler)
        {
            System.Windows.Forms.Application.ThreadException += handler;
        }

        #endregion

        #region static service methods

        /// <summary>Occurs when a service is requested.</summary>
        public static event ServiceNotificationEventHandler ServiceRequested;

        /// <summary>Occurs when a service is added.</summary>
        public static event ServiceNotificationEventHandler ServiceAdded;

        /// <summary>Occurs when a service is removed.</summary>
        public static event ServiceNotificationEventHandler ServiceRemoved;

        /// <summary>
        /// Gets a service of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of the requested service.</typeparam>
        /// <returns>An instance of the service or <c>null</c>.</returns>
        public static T GetService<T>() where T : class { return Services.GetService<T>(); }

        /// <summary>
        /// Gets a service of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of the requested service.</typeparam>
        /// <param name="mandatory">
        /// if set to <c>true</c> then, the requested service is mandatory 
        /// and if not found, a <see cref="Delta.CertXplorer.ComponentModel.ServiceNotFoundException"/> is thrown.
        /// </param>
        /// <returns>
        /// An instance of the service or <c>null</c>.
        /// </returns>
        public static T GetService<T>(bool mandatory) where T : class
        {
            return Services.GetService<T>(mandatory);
        }

        /// <summary>
        /// Adds a service.
        /// </summary>
        /// <typeparam name="T">Type of the added service.</typeparam>
        /// <param name="instance">The service instance to add.</param>
        public static void AddService<T>(T instance) where T : class { Services.AddService(instance); }

        /// <summary>
        /// Gets the list of service types made available by <see cref="This"/>.
        /// </summary>
        /// <returns>A list of service types.</returns>
        public static IList<Type> GetServicesList()
        {
            return childContainer.GetServicesList(true);
        }

        #endregion

        #region Static properties

        /// <summary>
        /// Gets a value indicating whether the current application is running in a web context.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the current application is running in a web context; otherwise, <c>false</c>.
        /// </value>
        public static bool IsWebContext
        {
            get { return System.Web.HttpContext.Current != null; }
        }

        /// <summary>
        /// Gets a value indicating whether the current application is running in a WCF context.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the current application is running in a WCF context; otherwise, <c>false</c>.
        /// </value>
        public static bool IsWcfContext
        {
            get { return System.ServiceModel.OperationContext.Current != null; }
        }

        /// <summary>
        /// Sets the current application as a WCF service host.
        /// </summary>
        public static bool IsWcfService
        {
            get { return Application.IsWcfService; }
            set
            {
                if (wcfServiceWasSet) throw new ApplicationException(
                    "Property This.IsWcfService can only be set once.");
                else
                {
                    Application.IsWcfService = value;
                    wcfServiceWasSet = true;
                }
            }
        }

        /// <summary>
        /// Gets the current user object.
        /// </summary>
        public static ThisUser User
        {
            get { CheckClass(); return userObjectProvider.GetInstance(); }
        }

        /// <summary>
        /// Gets the current computer object.
        /// </summary>
        public static ThisComputer Computer
        {
            get { CheckClass(); return computerObjectProvider.GetInstance(); }
        }

        /// <summary>
        /// Gets the current application object.
        /// </summary>
        public static ThisApplication Application
        {
            get { CheckClass(); return applicationObjectProvider.GetInstance(); }
        }

        public static ThisApplicationType ApplicationType
        {
            get { return Application.ApplicationType; }
        }

        /// <summary>
        /// Gets the default services container.
        /// </summary>
        public static IServiceContainer Services
        {
            get { CheckClass(); return childContainer; }
        }

        /// <summary>
        /// Gets the current log service.
        /// </summary>
        public static ILogService Logger
        {
            get
            {
                CheckClass();
                return childContainer.GetService<ILogService>();
            }
        }

        /// <summary>
        /// Gets the current log manager service.
        /// </summary>
        public static ILogManagerService Loggers
        {
            get
            {
                CheckClass();
                return childContainer.GetService<ILogManagerService>();
            }
        }

        #endregion

        /// <summary>
        /// Adds the default services to the parent container.
        /// </summary>
        /// <remarks>
        /// All the services added here are added to the parent container.
        /// This way, if a client class adds a service using the <see cref="Services"/>     
        /// property, and this service's type is already present, it will work, the newly
        /// added service will hide the existing one.
        /// </remarks>
        private static void AddDefaultServices()
        {
            LogManagerService logManager = new LogManagerService(new SimpleLogService());

            parentContainer.AddService<ILogService>(logManager);

            // We don't want the log manager to be masked by another implementation.
            childContainer.AddService<ILogManagerService>(logManager);

            // Remark: we pass the child container to the IExceptionHandlerService instance
            // constructor so that it uses the services that may be added by a client class,
            // and not always the default ones (especially for the logging service);
            parentContainer.AddService<IExceptionHandlerService>(
                new BaseExceptionHandlerService(childContainer));
        }

        /// <summary>
        /// Wires the child container events to the static events exposed by <see cref="This"/>.
        /// </summary>
        private static void WireEvents()
        {
            childContainer.ServiceRequested += (s, e) =>
            {
                if (ServiceRequested != null) ServiceRequested(null, e);
            };

            childContainer.ServiceAdded += (s, e) =>
            {
                if (ServiceAdded != null) ServiceAdded(null, e);
            };

            childContainer.ServiceRemoved += (s, e) =>
            {
                if (ServiceRemoved != null) ServiceRemoved(null, e);
            };
        }

        /// <summary>
        /// Disposes all disposable members of the class (among which, the service containers).
        /// </summary>
        /// <remarks>
        /// Disposing the service containers automatically disposes all the disposable service
        /// instances they may host.
        /// </remarks>
        private static void DisposeClass()
        {
            disposed = true;

            // Remark: calling ServiceContainer.Dispose() disposes
            // all disposable services that are contained in the container.

            if (childContainer != null)
            {
                childContainer.Dispose();
                childContainer = null;
            }

            if (parentContainer != null)
            {
                parentContainer.Dispose();
                parentContainer = null;
            }
        }

        /// <summary>
        /// Checks if this class can be used; ie if <see cref="DisposeClass"/> has not 
        /// been called.
        /// </summary>
        private static void CheckClass()
        {
            if (disposed) throw new ObjectDisposedException("This");
        }

        /// <summary>
        /// Called when a exception bubbles up to here from the Windows Forms application thread.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Threading.ThreadExceptionEventArgs"/> instance containing the event data.</param>
        private static void OnApplicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception, false);
        }

        /// <summary>
        /// Called when an exception bublles up until here.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.UnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private static void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject, e.IsTerminating);
        }

        /// <summary>
        /// Handles an unhandled exception.
        /// </summary>
        /// <param name="exceptionObject">The exception object.</param>
        /// <param name="isTerminating">if set to <c>true</c> [is terminating].</param>
        private static void HandleException(object exceptionObject, bool isTerminating)
        {
            try
            {
                IExceptionHandlerService service = Services.GetService<IExceptionHandlerService>(true);
                service.HandleException(exceptionObject, isTerminating);
            }
            catch (ServiceNotFoundException)
            {
                Console.WriteLine("FATAL ERROR: ", exceptionObject);
            }
            catch (Exception ex)
            {
                try
                {
                    Console.WriteLine("An exception occured while processing an unhandled exception: ", ex);
                }
                catch
                {
                    // Here, we are allowed to eat the exception ;-)
                }
            }
        }
    }
}