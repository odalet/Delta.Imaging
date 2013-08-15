using System;

using Delta.CertXplorer.UI;

namespace Delta.CertXplorer.CertManager
{
    public partial class PropertiesControl : ServicedUserControl
    {
        public PropertiesControl()
        {
            InitializeComponent();
            //ThemesManager.RegisterThemeAwareControl(this, (renderer) =>
            //{
            //    if (renderer is ToolStripProfessionalRenderer)
            //        ((ToolStripProfessionalRenderer)renderer).RoundedEdges = false;
            //    property.Renderer = renderer;
            //});
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            BindToSelectionService();
        }
        
        private void BindToSelectionService()
        {
            var service = GlobalSelectionService.GetOrCreateSelectionService(Services);
            service.SelectionChanged += (s, e) => propertyGrid.SelectedObject = service.SelectedObject;
        }
    }
}
