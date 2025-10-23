using System;
using DevExpress.DashboardWin;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Dashboards.Win;
using DevExpress.Persistent.Base;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace ListViewDemo.Win.Controllers
{
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
            var viewerItem = (WinDashboardViewerViewItem)sender;
            CustomizeDashboardViewer(viewerItem.Viewer);

            if (viewerItem.Viewer.Dashboard != null)
            {
                ConfigureDashboardItems(viewerItem.Viewer.Dashboard);
            }
        }

        private void CustomizeDashboardViewer(DashboardViewer dashboardViewer)
        {
            dashboardViewer.AllowPrintDashboardItems = true;
            dashboardViewer.DashboardItemControlCreated += DashboardViewer_DashboardItemControlCreated;
            dashboardViewer.DashboardItemControlUpdated += DashboardViewer_DashboardItemControlUpdated;
            dashboardViewer.DashboardLoaded += DashboardViewer_DashboardLoaded;

            if (dashboardViewer.Dashboard != null)
            {
                ConfigureDashboardItems(dashboardViewer.Dashboard);
            }
        }

        private void DashboardViewer_DashboardLoaded(object sender, EventArgs e)
        {
            var viewer = (DashboardViewer)sender;
            if (viewer.Dashboard != null)
            {
                ConfigureDashboardItems(viewer.Dashboard);
            }
        }

        private void ConfigureDashboardItems(DevExpress.DashboardCommon.Dashboard dashboard)
        {
            foreach (var item in dashboard.Items)
            {
                if (item is DevExpress.DashboardCommon.GridDashboardItem gridItem)
                {
                    gridItem.GridOptions.ColumnWidthMode = DevExpress.DashboardCommon.GridColumnWidthMode.AutoFitToContents;

                    System.Diagnostics.Debug.WriteLine($"Configured GridDashboardItem: {gridItem.ComponentName} with AutoFitToContents mode");
                }
            }
        }

        private void DashboardViewer_DashboardItemControlCreated(object sender, DashboardItemControlEventArgs e)
        {
            if (e.GridControl != null)
            {
                var gridControl = e.GridControl;
                var gridView = gridControl.MainView as GridView;

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
                var gridControl = e.GridControl;
                var gridView = gridControl.MainView as GridView;

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

                ConfigurePriceOnlySorting(gridView);
                ConfigureAlwaysShowNameColumn(gridView);

                gridView.EndUpdate();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en CustomizeGridView: {ex.Message}");
            }
        }

        private void ConfigurePriceOnlySorting(GridView gridView)
        {
            gridView.OptionsCustomization.AllowSort = true;

            foreach (DevExpress.XtraGrid.Columns.GridColumn column in gridView.Columns)
            {
                if (column.Caption == "Price")
                {
                    column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
                }
                else
                {
                    column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                }
            }
        }

        private void ConfigureAlwaysShowNameColumn(GridView gridView)
        {
            foreach (DevExpress.XtraGrid.Columns.GridColumn column in gridView.Columns)
            {
                if (column.Caption == "Name")
                {
                    column.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    column.OptionsColumn.AllowShowHide = false;
                    column.VisibleIndex = 0;
                    break;
                }
            }
        }

        protected override void OnDeactivated()
        {
            if (dashboardViewerViewItem != null)
            {
                dashboardViewerViewItem.ControlCreated -= DashboardViewerViewItem_ControlCreated;

                if (dashboardViewerViewItem.Viewer != null)
                {
                    dashboardViewerViewItem.Viewer.DashboardItemControlCreated -= DashboardViewer_DashboardItemControlCreated;
                    dashboardViewerViewItem.Viewer.DashboardItemControlUpdated -= DashboardViewer_DashboardItemControlUpdated;
                    dashboardViewerViewItem.Viewer.DashboardLoaded -= DashboardViewer_DashboardLoaded;
                }

                dashboardViewerViewItem = null;
            }

            base.OnDeactivated();
        }
    }
}