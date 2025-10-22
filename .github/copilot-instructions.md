# GitHub Copilot Instructions for ListViewDemo XAF Project

## Architecture Overview

This is a **DevExpress eXpress Application Framework (XAF) v24.2.6** project with three main components:

- **ListViewDemo.Module** - Shared business logic, data model, and platform-agnostic code
- **ListViewDemo.Win** - Windows Forms client application  
- **ListViewDemo.Blazor.Server** - Blazor Server web application

Both client applications reference the shared Module project and use **XPO (eXpress Persistent Objects)** for ORM with SQL Server LocalDB.

## Key Patterns & Conventions

### Business Objects
- Inherit from `BaseObject` (from `DevExpress.Persistent.BaseImpl`)
- Use XPO attributes: `[Size()]`, `[DefaultClassOptions]`, `[Persistent()]`
- Properties must call `SetPropertyValue()` in setters for change tracking
- Example: `ListViewDemo.Module.BusinessObjects.Product`

### Module Structure
- Main module class: `ListViewDemoModule` inherits from `ModuleBase`
- Register required modules in constructor using `RequiredModuleTypes.Add()`
- Database updates handled in `DatabaseUpdate/Updater.cs`
- Platform-agnostic controllers go in `Controllers/` folder

### Application Startup
- **Win**: `Program.cs` uses `ApplicationBuilder.BuildApplication(connectionString)`
- **Blazor**: Uses ASP.NET Core hosting with `Startup.cs`
- Both support `--updateDatabase`, `--forceUpdate`, `--silent` command line args
- Connection strings defined in `App.config` (Win) or `appsettings.json` (Blazor)

### Database Management
- Auto-updates in DEBUG mode when debugger attached
- Production requires explicit update via command line or manual handling
- Uses SQL Server LocalDB by default: `(localdb)\mssqllocaldb`
- EasyTest uses separate database: `ListViewDemoEasyTest`

## Development Workflows

### Adding Business Objects
1. Create class in `ListViewDemo.Module/BusinessObjects/`
2. Inherit from `BaseObject` or appropriate base class
3. Use XPO attributes for validation and UI hints
4. Properties must use `SetPropertyValue()` pattern

### Adding Controllers
1. Platform-agnostic: `ListViewDemo.Module/Controllers/`
2. Win-specific: `ListViewDemo.Win/Controllers/`
3. Blazor-specific: `ListViewDemo.Blazor.Server/Controllers/`

### Database Schema Changes
- Implement in `Updater.UpdateDatabaseAfterUpdateSchema()` for data
- Use `UpdateDatabaseBeforeUpdateSchema()` for schema changes like `RenameColumn()`
- Always test with `--updateDatabase` argument

### Building & Running
```bash
# Build entire solution
dotnet build ListViewDemo.sln

# Run Windows app with DB update
dotnet run --project ListViewDemo.Win -- --updateDatabase

# Run Blazor app with DB update
dotnet run --project ListViewDemo.Blazor.Server -- --updateDatabase
```

## File Structure Significance

- `Model.xafml` - Application model customizations (UI layout, permissions)
- `Model.DesignedDiffs.xafml` - Designer-generated model differences
- `ReadMe.txt` files - DevExpress documentation links for each folder purpose

## XAF-Specific Notes

- Use `IObjectSpace` for data operations, not direct XPO Session
- Controllers should inherit from `ViewController` or `ObjectViewController<T>`
- Actions defined with `[Action]` attribute or created programmatically
- Model customization via `IModelNode` interfaces or Application Model editor
- Always consider both Win and Blazor platforms when making changes

### List Editors & UI Components
- **Windows Forms**: `GridListEditor` uses `DevExpress.XtraGrid.GridControl` as underlying component
- **Blazor**: `DxGridListEditor` uses `DevExpress.Blazor.DxGrid` component
- Access Grid component: `((GridListEditor)View.Editor).GridView` in Controllers
- List Views display object collections via List Editors - not direct grid controls
- Different List Editors available: GridListEditor (default), CategorizedListEditor, ChartListEditor, etc.

### Grid Customization Patterns
- Access GridView: `((GridListEditor)View.Editor).GridView` in `OnViewControlsCreated()`
- Common customizations: single-column sorting, appearance, row selection, filtering
- Example: `SingleColumnSortController` restricts sorting to one column at a time
- Always unsubscribe from events in `OnDeactivated()` to prevent memory leaks

## Common Pitfalls

- Don't modify `obj/` or `bin/` generated files
- Model files (`.xafml`) are XML - handle merge conflicts carefully  
- Database connection issues often stem from LocalDB not running
- XPO requires parameterless constructor + `Session` constructor
- Platform-specific features should go in respective platform projects, not Module.
