using System;
using System.Windows.Forms;

namespace Delta.CertXplorer.ThisImplementation
{
    /// <summary>
    /// Provides properties for manipulating computer components such as 
    /// audio, the clock, the keyboard, the file system, and so on.
    /// </summary>
    public class ThisComputer
    {
        private static Clock clock = null;
        private static ClipboardProxy clipboardProxy = null;
        private static Keyboard keyboard = null;
        private static Mouse mouse = null;
        private ThisComputerInfo computerInfo = null;
        private FileSystemProxy fileSystemProxy = null;
        private Network network = null;
        private RegistryProxy registryProxy = null;
        private Audio audio = null;
        private Ports ports = null;

        /// <summary>Gets the computer name.</summary>
        public string Name
        {
            get { return Environment.MachineName; }
        }

        /// <summary>
        /// Gets the <see cref="System.Windows.Forms.Screen"/> object that represents 
        /// the computer's primary display screen.
        /// </summary>
        public Screen Screen { get { return Screen.PrimaryScreen; } }

        /// <summary>
        /// Gets an object that provides properties for accessing the current local time and 
        /// Universal Coordinated Time (the equivalent to Greenwich Mean Time) from the system clock.
        /// </summary>
        public Clock Clock
        {
            get
            {
                if (clock == null) clock = new Clock();
                return clock;
            }
        }

        /// <summary>
        /// Gets an object that provides properties and methods for working with drives, 
        /// files, and directories.
        /// </summary>
        public FileSystemProxy FileSystem
        {
            get
            {
                if (this.fileSystemProxy == null) fileSystemProxy = new FileSystemProxy();
                return fileSystemProxy;
            }
        }

        /// <summary>
        /// Gets an object that provides properties for getting information 
        /// about the computer's memory, loaded assemblies, name, and 
        /// operating system.
        /// </summary>
        public ThisComputerInfo Info
        {
            get
            {
                if (computerInfo == null) computerInfo = new ThisComputerInfo();
                return computerInfo;
            }
        }

        /// <summary>
        /// Gets an object that provides properties and methods for manipulating the registry.
        /// </summary>
        public RegistryProxy Registry
        {
            get
            {
                if (registryProxy == null) registryProxy = new RegistryProxy();
                return registryProxy;
            }
        }

        /// <summary>
        /// Gets an object that provides properties for methods for playing sounds.
        /// </summary>
        public Audio Audio
        {
            get
            {
                if (audio == null) audio = new Audio();
                return audio;
            }
        }

        /// <summary>
        /// Gets an object that provides methods for manipulating the Clipboard.
        /// </summary>
        public ClipboardProxy Clipboard
        {
            get
            {
                if (clipboardProxy == null) clipboardProxy = new ClipboardProxy();
                return clipboardProxy;
            }
        }

        /// <summary>
        /// Gets an object that provides properties for accessing the current state 
        /// of the keyboard, such as what keys are currently pressed, and provides 
        /// a method to send keystrokes to the active window.
        /// </summary>
        /// <value>The keyboard.</value>
        public Keyboard Keyboard
        {
            get
            {
                if (keyboard == null) keyboard = new Keyboard();
                return keyboard;
            }
        }

        /// <summary>
        /// Gets an object that provides properties for getting information 
        /// about the format and configuration of the mouse installed 
        /// on the local computer.
        /// </summary>
        public Mouse Mouse
        {
            get
            {
                if (mouse == null) mouse = new Mouse();
                return mouse;
            }
        }

        /// <summary>
        /// Gets an object that provides a property and a method for 
        /// accessing the computer's serial ports.
        /// </summary>
        public Ports Ports
        {
            get
            {
                if (ports == null) ports = new Ports();
                return ports;
            }
        }

        /// <summary>
        /// Gets an object that provides a property and methods for interacting 
        /// with the network to which the computer is connected.
        /// </summary>
        public Network Network
        {
            get
            {
                if (network == null) network = new Network();
                return network;
            }
        }
    }
}
