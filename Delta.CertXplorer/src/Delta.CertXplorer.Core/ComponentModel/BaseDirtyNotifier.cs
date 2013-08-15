using System;
using System.ComponentModel;

namespace Delta.CertXplorer.ComponentModel
{
    /// <summary>
    /// Base implementation for the <see cref="IDirtyNotifier"/> interface.
    /// </summary>
    public abstract class BaseDirtyNotifier : IDirtyNotifier, INotifyPropertyChanged
    {
        /// <summary>Dirty state</summary>
        private bool dirty = false;

        #region IDirtyNotifier Members

        /// <summary>
        /// Gets the current 'dirty' state.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if dirty (non consistent with the saved state); otherwise, <c>false</c>.
        /// </value>
        public virtual bool Dirty { get { return dirty; } }

        /// <summary>
        /// Occurs when the dirty state has changed.
        /// </summary>
        public event DirtyChangedEventHandler DirtyChanged;

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        /// <summary>
        /// Sets the dirty state to <c>true</c>.
        /// </summary>
        protected void SetDirty() { SetDirty(true); }

        /// <summary>
        /// Sets the dirty to <paramref name="isDirty"/>.
        /// </summary>
        /// <param name="isDirty">new value of the dirty state</param>
        protected virtual void SetDirty(bool isDirty)
        {
            if (isDirty != dirty)
            {
                dirty = isDirty;
                if (DirtyChanged != null) DirtyChanged(this, new DirtyChangedEventArgs(dirty));

                // Don't use OnPropertyChanged("Dirty"): we don't want to create an infinite loop 
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Dirty"));
            }
        }

        /// <summary>
        /// Called when a property value has changed.
        /// </summary>
        /// <remarks>
        /// This method tries to raise the <see cref="PropertyChanged"/> handler, then
        /// sets the 'dirty' state to <c>true</c>.
        /// </remarks>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (propertyName == "Dirty") throw new ArgumentException(
                "This method can't be called on the 'Dirty' property.", "propertyName");

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            SetDirty();
        }
    }
}
