using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using Microsoft.Extensions.DependencyInjection;
using ListViewDemo.Module.BusinessObjects;
using System;
using System.Linq;

namespace ListViewDemo.Module.DatabaseUpdate;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
public class Updater : ModuleUpdater {
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
        base(objectSpace, currentDBVersion) {
    }
  public override void UpdateDatabaseAfterUpdateSchema() {
      base.UpdateDatabaseAfterUpdateSchema();
        
        // Check if there are any existing Product records
        var existingProducts = ObjectSpace.GetObjectsCount(typeof(Product), null);
   
        if (existingProducts == 0) {
   // Create 20 Product records with specified character lengths
for (int i = 1; i <= 20; i++) {
           var product = ObjectSpace.CreateObject<Product>();
                
    // Name: 45-99 characters
      product.Name = GenerateText($"Product Name {i:D2}", 45, 99);
   
       // Description: 10-35 characters
           product.Description = GenerateText($"Description {i}", 10, 35);
      
        // Price: Random decimal between 10 and 10000
   product.Price = (decimal)(new Random(i * 1000).NextDouble() * 9990 + 10);
           
           // Field1: 10-35 characters
        product.Field1 = GenerateText($"Field1 Value {i}", 10, 35);
  
        // Address: 10-35 characters
          product.Address = GenerateText($"Address {i}", 10, 35);
     
    // Column1-15: 10-35 characters each
     product.Column1 = GenerateText($"Column1 Data {i}", 10, 35);
        product.Column2 = GenerateText($"Column2 Data {i}", 10, 35);
             product.Column3 = GenerateText($"Column3 Data {i}", 10, 35);
     product.Column4 = GenerateText($"Column4 Data {i}", 10, 35);
   product.Column5 = GenerateText($"Column5 Data {i}", 10, 35);
  product.Column6 = GenerateText($"Column6 Data {i}", 10, 35);
     product.Column7 = GenerateText($"Column7 Data {i}", 10, 35);
                product.Column8 = GenerateText($"Column8 Data {i}", 10, 35);
       product.Column9 = GenerateText($"Column9 Data {i}", 10, 35);
      product.Column10 = GenerateText($"Column10 Data {i}", 10, 35);
        product.Column11 = GenerateText($"Column11 Data {i}", 10, 35);
   product.Column12 = GenerateText($"Column12 Data {i}", 10, 35);
     product.Column13 = GenerateText($"Column13 Data {i}", 10, 35);
    product.Column14 = GenerateText($"Column14 Data {i}", 10, 35);
        product.Colimn15 = GenerateText($"Column15 Data {i}", 10, 35);
   }
        
            ObjectSpace.CommitChanges();
        }
    }
    
    /// <summary>
    /// Generates text with a length between minLength and maxLength characters
    /// </summary>
    private string GenerateText(string baseText, int minLength, int maxLength) {
 var random = new Random(baseText.GetHashCode());
        int targetLength = random.Next(minLength, maxLength + 1);
  
        if (baseText.Length >= targetLength) {
      return baseText.Substring(0, targetLength);
        }
        
        // Pad with additional text to reach target length
        string filler = " - Lorem ipsum dolor sit amet consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore";
        string result = baseText;
        
        while (result.Length < targetLength) {
            int remaining = targetLength - result.Length;
          if (remaining > 0) {
          result += filler.Substring(0, Math.Min(remaining, filler.Length));
    }
        }
        
      return result.Substring(0, targetLength);
    }
    
    public override void UpdateDatabaseBeforeUpdateSchema() {
        base.UpdateDatabaseBeforeUpdateSchema();
  //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
        //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
        //}
    }
}
