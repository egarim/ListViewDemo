using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System;

namespace ListViewDemo.Win.Controllers
{
    /// <summary>
    /// Controller that always shows the content of the Name column in GridListEditor
    /// </summary>
    public class AlwaysShowNameController : ViewController<ListView>
    {
        private GridListEditor gridListEditor;
        private GridView gridView;

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            // Only apply to GridListEditor in Windows Forms
            gridListEditor = View.Editor as GridListEditor;
            if (gridListEditor != null)
            {
                gridView = gridListEditor.GridView;
                if (gridView != null)
                {
                    // Configure to always show the Name column
                    ConfigureAlwaysShowAddress();
                    
                    // Also configure when columns are added dynamically
                    gridListEditor.ColumnCreated += GridListEditor_ColumnCreated;
                }
            }
        }

        private void GridListEditor_ColumnCreated(object sender, DevExpress.ExpressApp.Win.Editors.ColumnCreatedEventArgs e)
        {
            // Configure width for all dynamically created columns
            ConfigureColumnWidth(e.Column);
            
            // Special configuration for the Name column
            if (e.Column.FieldName == "Name")
            {
                ConfigureAddressColumn(e.Column);
            }
        }

        private void ConfigureAlwaysShowAddress()
        {
            // Disable auto-width to allow horizontal scrolling
            gridView.OptionsView.ColumnAutoWidth = false;
            
            // Enable horizontal scroll
            gridView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            
            // Configure fixed widths for columns to force scrolling
            foreach (GridColumn column in gridView.Columns)
            {
                ConfigureColumnWidth(column);
                
                if (column.FieldName == "Name")
                {
                    ConfigureAddressColumn(column);
                }
            }
        }

        private void ConfigureColumnWidth(GridColumn column)
        {
            // Set generous fixed widths to force horizontal scrolling
            switch (column.FieldName)
            {
                case "Name":
                    column.Width = 200;
                    break;
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
                default:
                    column.Width = 150; // Default width
                    break;
            }
            
            // Allow the user to manually resize columns
            column.OptionsColumn.AllowSize = true;
        }

        private void ConfigureAddressColumn(GridColumn addressColumn)
        {
            // Make the Name column always visible
            addressColumn.Visible = true;

            // Prevent the user from hiding it
            addressColumn.OptionsColumn.AllowShowHide = false;

            // Fix the column to the left so it's always visible
            addressColumn.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            // Set a minimum width
            addressColumn.MinWidth = 800;

            // Optional: Give it priority in column order (show first)
            addressColumn.VisibleIndex = 0;
        }
        
        protected override void OnDeactivated()
        {
            // Clean up events when deactivating the controller
            if (gridListEditor != null)
            {
                gridListEditor.ColumnCreated -= GridListEditor_ColumnCreated;
            }

            gridListEditor = null;
            gridView = null;

            base.OnDeactivated();
        }
    }
}