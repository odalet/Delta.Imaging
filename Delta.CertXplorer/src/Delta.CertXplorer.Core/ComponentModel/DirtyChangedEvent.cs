using System;
using System.ComponentModel;

namespace Delta.CertXplorer.ComponentModel
{
    /// <summary>
    /// Représente la méthode qui traite les événements <see cref="IDirtyNotifier.DirtyChanged"/>.
    /// </summary>
    /// <param name="sender">Source de l'événement.</param>
    /// <param name="e">Données de l'événement.</param>
    public delegate void DirtyChangedEventHandler(object sender, DirtyChangedEventArgs e);

    /// <summary>
    /// Fournit des données pour l'événement <see cref="IDirtyNotifier.DirtyChanged"/>.
    /// </summary>
    public class DirtyChangedEventArgs : PropertyChangedEventArgs
    {
        private bool dirty = false;

        /// <summary>
        /// Construit un objet <see cref="DirtyChangedEventArgs"/>
        /// </summary>
        /// <param name="isDirty">Valeur de la propriété <see cref="Dirty"/>.</param>
        public DirtyChangedEventArgs(bool isDirty) : base("Dirty") { dirty = isDirty; }

        /// <summary>
        /// Obtient une valeur indiquant si l'état de l'objet source de 
        /// l'événement a changé et nécessite une sauvegarde.
        /// </summary>
        public bool Dirty { get { return dirty; } }
    }
}
