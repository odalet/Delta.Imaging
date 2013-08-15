using System;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

using Delta.CertXplorer.UI;

namespace Delta.CertXplorer.ThisImplementation
{
	partial class ThisApplication
	{
        private static class SingleInstanceManager
        {
            [DllImport("user32.dll")]
            private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

            [DllImport("user32.dll")]
            private static extern int SetForegroundWindow(IntPtr hWnd);

            [DllImport("user32.dll")]
            private static extern int IsIconic(IntPtr hWnd);
            
            private const int SW_RESTORE = 9;

            private static Mutex mutex = null;

            /// <summary>
            /// If this is the first timùe we run this application, runs
            /// a standard application message loop on the current thread,
            /// and makes the specified form visible.
            /// </summary>
            /// <param name="form">The form.</param>
            public static void Run(System.Windows.Forms.Form form)
            {
                // set focus on already running application's main form.
                if (IsAlreadyRunning) SwitchToCurrentInstance();
                else System.Windows.Forms.Application.Run(form);
            }

            private static IntPtr GetCurrentInstanceWindowHandle()
            {
                IntPtr hWnd = IntPtr.Zero;
                Process process = Process.GetCurrentProcess();
                Process[] processes = Process.GetProcessesByName(process.ProcessName);
                foreach (var p in processes)
                {
                    // Get the first instance that is not this instance, has the
                    // same process name and was started from the same file name
                    // and location. Also check that the process has a valid
                    // window handle in this session to filter out other user's
                    // processes.
                    if (p.Id != process.Id &&
                        p.MainModule.FileName == process.MainModule.FileName &&
                        p.MainWindowHandle != IntPtr.Zero)
                    {
                        hWnd = p.MainWindowHandle;
                        break;
                    }
                }

                return hWnd;
            }

            /// <summary>
            /// Switches to current instance.
            /// </summary>
            private static void SwitchToCurrentInstance()
            {
                IntPtr hWnd = GetCurrentInstanceWindowHandle();
                if (hWnd != IntPtr.Zero)
                {
                    // Restore window if minimised. Do not restore if already in
                    // normal or maximized window state, since we don't want to
                    // change the current state of the window.
                    if (IsIconic(hWnd) != 0) ShowWindow(hWnd, SW_RESTORE);

                    // Set foreground window.
                    SetForegroundWindow(hWnd);
                }
                else // We couldn't find the other instance's window handle... 
                {
                    ErrorBox.Show("Another instance of this application is already running.");
                }
            }

            /// <summary>
            /// Gets a value indicating whether the current program is already running.
            /// </summary>
            /// <value>
            /// 	<c>true</c> if the current program is already running; otherwise, <c>false</c>.
            /// </value>
            private static bool IsAlreadyRunning
            {
                get
                {
                    bool createdNew;

                    mutex = new Mutex(true, string.Format("Global\\{0}", ApplicationInstanceId), out createdNew);
                    if (createdNew) mutex.ReleaseMutex();

                    return !createdNew;
                }
            }

            /// <summary>
            /// Gets the application instance id.
            /// </summary>
            /// <value>The application instance id.</value>
            private static string ApplicationInstanceId
            {
                get
                {
                    var entryAssembly = This.Application.EntryAssembly;
                    Guid guid = Marshal.GetTypeLibGuidForAssembly(entryAssembly);
                    Version version = entryAssembly.GetName().Version;
                    return string.Format("{0}/{1}.{2}", guid, version.Major, version.Minor);
                }
            }
        }
	}
}
