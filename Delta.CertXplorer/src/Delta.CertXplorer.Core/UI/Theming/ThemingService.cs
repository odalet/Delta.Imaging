using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Delta.CertXplorer.UI.Theming
{
    /// <summary>
    /// Implementation of <see cref="IThemingService"/>: provides a means 
    /// of globally managing and applying themes to a Windows Forms application.
    /// </summary>
    public class ThemingService : IThemingService
    {
        /// <summary>
        /// Stores the association between theme identifiers and the 
        /// function to execute when applying a theme.
        /// </summary>
        private Dictionary<string, Func<string, ToolStripRenderer>> themes = 
            new Dictionary<string, Func<string, ToolStripRenderer>>();

        private string currentThemeId = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThemingService"/> class
        /// and registers the default themes.
        /// </summary>
        public ThemingService() { RegisterDefaultThemes(); }

        #region IThemingService Members

        /// <summary>
        /// Gets the list of theme identifiers.
        /// </summary>
        /// <value>The theme identifiers.</value>
        public string[] Themes
        {
            get { return themes.Keys.ToArray(); }
        }

        /// <summary>
        /// Gets the current theme identifier.
        /// </summary>
        /// <value>The current theme identifier.</value>
        public string Current
        {
            get { return currentThemeId; }
        }

        /// <summary>
        /// Determines whether the specified theme id is registered with this service.
        /// </summary>
        /// <param name="themeId">The theme id.</param>
        /// <returns>
        /// 	<c>true</c> if the specified theme id is registered; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsTheme(string themeId)
        {
            if (string.IsNullOrEmpty(themeId)) return false;
            else return themes.ContainsKey(themeId);
        }

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
        public void RegisterTheme(string themeId, Func<string, ToolStripRenderer> onCreateTheme)
        {
            if (onCreateTheme == null) throw new ArgumentNullException("onCreateTheme");
            if (themes.ContainsKey(themeId)) throw new ArgumentException(string.Format(
                "theme {0} is already registered", themeId, "themeId"));

            themes.Add(themeId, onCreateTheme);
        }

        /// <summary>
        /// Registers a theme identified by <paramref name="themeId"/>.
        /// </summary>
        /// <param name="themeId">The theme id.</param>
        /// <param name="colorTable">The color table to pass to the toolstrip renderer that will be applied when
        /// calling <see cref="ApplyTheme"/>.</param>
        public void RegisterTheme(string themeId, ProfessionalColorTable colorTable)
        {
            RegisterTheme(themeId, (t) => CreateRenderer(colorTable));
        }

        /// <summary>
        /// Applies the theme identified by <paramref name="themeId"/>.
        /// </summary>
        /// <param name="themeId">The theme id.</param>
        public void ApplyTheme(string themeId)
        {
            var renderer = CreateThemeRenderer(themeId);
            if (renderer != null)
            {
                currentThemeId = themeId;
                ToolStripManager.Renderer = renderer;                
            }
            else This.Logger.Warning(string.Format(
                "Creation of theme {0} failed: associated renderer is null.", themeId));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="System.Windows.Forms.ToolStripRenderer"/>
        /// associated with the pecified theme.
        /// </summary>
        /// <param name="themeId">The theme id.</param>
        public ToolStripRenderer CreateThemeRenderer(string themeId)
        {
            if (!themes.ContainsKey(themeId)) This.Logger.Warning(string.Format(
                "Theme {0} could not be found", themeId));
            return themes[themeId](themeId);
        }
        
        #endregion

        public const string SystemTheme = "System";
        public const string CertXplorerTheme = "Delta.CertXplorer";
        public const string TanTheme = "Tan";
        public const string OfficeTheme = "Office";
        public const string OliveTheme = "Olive";
        public const string LunaTheme = "Luna";
        public const string RoyaleTheme = "Royale";
        public const string Office2007BlueTheme = "Office2007Blue";
        public const string VisualStudio2008Theme = "VisualStudio2008";
        
        private static string[] defaultThemes = 
        {
            SystemTheme, CertXplorerTheme, TanTheme,
            OfficeTheme, OliveTheme, LunaTheme, RoyaleTheme,
            Office2007BlueTheme, VisualStudio2008Theme
        };

        /// <summary>
        /// Registers the default themes.
        /// </summary>
        protected virtual void RegisterDefaultThemes()
        {
            foreach (string themeId in defaultThemes)
                RegisterTheme(themeId, CreateDefaultThemes);

            currentThemeId = SystemTheme;
        }

        /// <summary>
        /// Creates a toolstrip renderer.
        /// </summary>
        /// <param name="colorTable">The color table passed to the renderer.</param>
        /// <returns>A <see cref="ToolStripProfessionalRenderer"/> instance.</returns>
        protected virtual ToolStripProfessionalRenderer CreateRenderer(ProfessionalColorTable colorTable)
        {
            return new BaseToolStripRenderer(colorTable);
        }

        private ToolStripRenderer CreateDefaultThemes(string id)
        {
            switch (id)
            {
                case SystemTheme: return CreateRenderer(
                    new ProfessionalColorTable() { UseSystemColors = true });
                case CertXplorerTheme: return Office2007Renderer.CertXplorer;
                case TanTheme: return CreateRenderer(
                    new TanLunaColorTable());
                case OfficeTheme: return CreateRenderer(
                    OfficeColorTable.Normal);
                case OliveTheme: return CreateRenderer(
                    OfficeColorTable.Olive);
                case LunaTheme: return CreateRenderer(
                    OfficeColorTable.Royale);
                case RoyaleTheme: return CreateRenderer(
                    OfficeColorTable.Royale);
                case Office2007BlueTheme: return Office2007Renderer.Office2007Blue;
                case VisualStudio2008Theme: return new VisualStudio2008Renderer();
            }

            return null;
        }
    }
}
