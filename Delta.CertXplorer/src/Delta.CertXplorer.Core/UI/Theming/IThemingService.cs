using System;
using System.Windows.Forms;

namespace Delta.CertXplorer.UI.Theming
{
    /// <summary>
    /// Provides a means of globally managing and applying 
    /// themes to a Windows Forms application.
    /// </summary>
    public interface IThemingService
    {
        /// <summary>
        /// Gets the list of theme identifiers.
        /// </summary>
        /// <value>The theme identifiers.</value>
        string[] Themes { get; }

        /// <summary>
        /// Gets the current theme identifier.
        /// </summary>
        /// <value>The current theme identifier.</value>
        string Current { get; }

        /// <summary>
        /// Determines whether the specified theme id is registered with this service.
        /// </summary>
        /// <param name="themeId">The theme id.</param>
        /// <returns>
        /// 	<c>true</c> if the specified theme id is registered; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsTheme(string themeId);

        /// <summary>
        /// Registers a theme identified by <paramref name="id"/>.
        /// </summary>
        /// <remarks>
        /// When executed, the theme id is passed to the action <paramref name="onCreateTheme"/>.
        /// </remarks>
        /// <param name="themeId">The theme id.</param>
        /// <param name="onCreateTheme">
        /// The action to execute when applying the theme: should return a <see cref="System.Windows.Forms.ToolStripRenderer"/> instance
        /// </param>
        void RegisterTheme(string themeId, Func<string, ToolStripRenderer> onCreateTheme);

        /// <summary>
        /// Registers a theme identified by <paramref name="themeId"/>.
        /// </summary>
        /// <param name="themeId">The theme id.</param>
        /// <param name="colorTable">
        /// The color table to pass to a professional toolstrip renderer that will be applied when
        /// calling <see cref="ApplyTheme"/>.
        /// </param>
        void RegisterTheme(string themeId, ProfessionalColorTable colorTable);

        /// <summary>
        /// Applies the theme identified by <paramref name="themeId"/>.
        /// </summary>
        /// <param name="themeId">The theme id.</param>
        void ApplyTheme(string themeId);

        /// <summary>
        /// Creates a new instance of the <see cref="System.Windows.Forms.ToolStripRenderer"/>
        /// associated with the pecified theme.
        /// </summary>
        /// <param name="themeId">The theme id.</param>
        ToolStripRenderer CreateThemeRenderer(string themeId);
    }
}
