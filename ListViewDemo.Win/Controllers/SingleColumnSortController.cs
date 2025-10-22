using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;
using System;
using System.Linq;

namespace ListViewDemo.Win.Controllers
{
    /// <summary>
    /// Controller that allows sorting only by the Price column in GridListEditor
    /// </summary>
    public class SingleColumnSortController : ViewController<ListView>
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
                    // Configure to allow sorting only by Price
                    ConfigurePriceOnlySorting();
                    
                    // Also configure when columns are added dynamically
                    gridListEditor.ColumnCreated += GridListEditor_ColumnCreated;
                }
            }
        }

        private void GridListEditor_ColumnCreated(object sender, DevExpress.ExpressApp.Win.Editors.ColumnCreatedEventArgs e)
        {
            // Disable sorting on all columns except Price
            if (e.Column.FieldName != "Price")
            {
                e.Column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            }
            else
            {
                e.Column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            }
        }

        private void ConfigurePriceOnlySorting()
        {
            // Allow general sorting in the GridView
            gridView.OptionsCustomization.AllowSort = true;
            
            // Configure sorting only on specific columns
            foreach (GridColumn column in gridView.Columns)
            {
                if (column.FieldName != "Price")
                {
                    column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                }
                else
                {
                    column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
                }
            }
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