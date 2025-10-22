using System;
using DevExpress.DashboardWin;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Dashboards.Win;
using DevExpress.Persistent.Base;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace ListViewDemo.Win.Controllers
{
    /// <summary>
    /// Controller that customizes the GridView in Dashboard items with column sizing and sorting configurations
    /// </summary>
    public partial class WinDashboardController : ObjectViewController<DetailView, IDashboardData>
    {
        private WinDashboardViewerViewItem dashboardViewerViewItem;

        public WinDashboardController()
        {
            InitializeComponent();
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            dashboardViewerViewItem = View.FindItem("DashboardViewer") as WinDashboardViewerViewItem;
            if (dashboardViewerViewItem != null)
            {
                if (dashboardViewerViewItem.Viewer != null)
                {
                    CustomizeDashboardViewer(dashboardViewerViewItem.Viewer);
                }
                else
                {
                    dashboardViewerViewItem.ControlCreated += DashboardViewerViewItem_ControlCreated;
                }
            }
        }

        private void DashboardViewerViewItem_ControlCreated(object sender, EventArgs e)
        {
            CustomizeDashboardViewer(((WinDashboardViewerViewItem)sender).Viewer);
        }

        private void CustomizeDashboardViewer(DashboardViewer dashboardViewer)
        {
            dashboardViewer.AllowPrintDashboardItems = true;
            dashboardViewer.DashboardItemControlCreated += DashboardViewer_DashboardItemControlCreated;
            dashboardViewer.DashboardItemControlUpdated += DashboardViewer_DashboardItemControlUpdated;
        }

        private void DashboardViewer_DashboardItemControlCreated(object sender, DashboardItemControlEventArgs e)
        {
            if (e.GridControl != null)
            {
              
                var gridView = e.GridControl.MainView as GridView;
                gridView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;

                if (gridView != null)
                {
                    CustomizeGridView(gridView, e.DashboardItemName);
                }
            }
        }

        private void DashboardViewer_DashboardItemControlUpdated(object sender, DashboardItemControlEventArgs e)
        {
            if (e.GridControl != null)
            {
                var gridView = e.GridControl.MainView as GridView;

                if (gridView != null)
                {
                    CustomizeGridView(gridView, e.DashboardItemName);
                }
            }
        }

        private void CustomizeGridView(GridView gridView, string dashboardItemName)
        {
            try
            {
                gridView.BeginUpdate();

                // Disable auto-width to allow horizontal scrolling
                gridView.OptionsView.ColumnAutoWidth = false;

                // Enable horizontal scroll
                gridView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;

                // Show horizontal lines for better readability
                gridView.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.True;
              
                // Configure embedded navigator
                if (gridView.GridControl != null)
                {
                    var gridControl = gridView.GridControl;
                    gridControl.UseEmbeddedNavigator = false;
                    gridControl.EmbeddedNavigator.Buttons.Append.Visible = false;
                    gridControl.EmbeddedNavigator.Buttons.Remove.Visible = false;
                }

                // Configure fixed widths for columns to force scrolling
                foreach (DevExpress.XtraGrid.Columns.GridColumn column in gridView.Columns)
                {
                    ConfigureColumnWidth(column);

                    // Special configuration for the Name column
                    //if (column.FieldName == "Name")
                    //{
                    //    ConfigureNameColumn(column);
                    //}
                }

                gridView.EndUpdate();

                // Refresh the grid control
                if (gridView.GridControl != null)
                {
                    gridView.GridControl.Refresh();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in CustomizeGridView: {ex.Message}");
            }

            // Apply additional configurations
            ConfigurePriceOnlySorting(gridView);
        }

        private void ConfigurePriceOnlySorting(GridView gridView)
        {
            // Allow general sorting in the GridView
            gridView.OptionsCustomization.AllowSort = true;

            // Configure sorting only on specific columns
            foreach (DevExpress.XtraGrid.Columns.GridColumn column in gridView.Columns)
            {
                if (column.FieldName == "Price")
                {
                    column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
                }
                else
                {
                    column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                }
            }
        }

        private void ConfigureColumnWidth(DevExpress.XtraGrid.Columns.GridColumn column)
        {
            // Set generous fixed widths to force horizontal scrolling
            //caption because fieldname may vary in dashboards
            switch (column.Caption)
            {
                case "Name":
                    column.Width = 1500;
                    column.OptionsColumn.AllowSize = false; // Prevent resizing for Name column
                    return;
                case "Price":
                    column.Width = 150;
                    break;
                case "Description":
                    column.Width = 300;
                    break;
                case "Address":
                    column.Width = 250;
                    break;
                case "Field1":
                    column.Width = 180;
                    break;
                case "Column1":
                case "Column2":
                case "Column3":
                case "Column4":
                case "Column5":
                    column.Width = 180;
                    break;
                case "Column6":
                case "Column7":
                case "Column8":
                case "Column9":
                case "Column10":
                    column.Width = 160;
                    break;
                case "Column11":
                case "Column12":
                case "Column13":
                case "Column14":
                case "Colimn15":
                    column.Width = 140;
                    break;
                default:
                    column.Width = 150; // Default width
                    break;
            }

            // Allow the user to manually resize columns
            column.OptionsColumn.AllowSize = true;
            column.Visible = true;
        }


        protected override void OnDeactivated()
        {
            // Clean up events when deactivating the controller
            if (dashboardViewerViewItem != null)
            {
                dashboardViewerViewItem.ControlCreated -= DashboardViewerViewItem_ControlCreated;

                if (dashboardViewerViewItem.Viewer != null)
                {
                    dashboardViewerViewItem.Viewer.DashboardItemControlCreated -= DashboardViewer_DashboardItemControlCreated;
                    dashboardViewerViewItem.Viewer.DashboardItemControlUpdated -= DashboardViewer_DashboardItemControlUpdated;
                }

                dashboardViewerViewItem = null;
            }

            base.OnDeactivated();
        }
    }
}