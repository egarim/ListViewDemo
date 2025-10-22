using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ListViewDemo.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://docs.devexpress.com/eXpressAppFramework/112701/business-model-design-orm/data-annotations-in-data-model).
    public class Product : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://docs.devexpress.com/eXpressAppFramework/113146/business-model-design-orm/business-model-design-with-xpo/base-persistent-classes).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public Product(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://docs.devexpress.com/eXpressAppFramework/112834/getting-started/in-depth-tutorial-winforms-webforms/business-model-design/initialize-a-property-after-creating-an-object-xpo?v=22.1).
        }
        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue(nameof(PersistentProperty), ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://docs.devexpress.com/eXpressAppFramework/112619/ui-construction/controllers-and-actions/actions/how-to-create-an-action-using-the-action-attribute).
        //    this.PersistentProperty = "Paid";
        //}

        string colimn15;
        string column14;
        string column13;
        string column12;
        string column11;
        string column10;
        string column9;
        string column8;
        string column7;
        string column6;
        string column5;
        string column4;
        string column3;
        string column2;
        string column1;
        string address;
        string field1;
        decimal price;
        string description;
        string name;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        public decimal Price
        {
            get => price;
            set => SetPropertyValue(nameof(Price), ref price, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Description
        {
            get => description;
            set => SetPropertyValue(nameof(Description), ref description, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Column1
        {
            get => column1;
            set => SetPropertyValue(nameof(Column1), ref column1, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Column2
        {
            get => column2;
            set => SetPropertyValue(nameof(Column2), ref column2, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Column3
        {
            get => column3;
            set => SetPropertyValue(nameof(Column3), ref column3, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Column4
        {
            get => column4;
            set => SetPropertyValue(nameof(Column4), ref column4, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Column5
        {
            get => column5;
            set => SetPropertyValue(nameof(Column5), ref column5, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Column6
        {
            get => column6;
            set => SetPropertyValue(nameof(Column6), ref column6, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Column7
        {
            get => column7;
            set => SetPropertyValue(nameof(Column7), ref column7, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Column8
        {
            get => column8;
            set => SetPropertyValue(nameof(Column8), ref column8, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Column9
        {
            get => column9;
            set => SetPropertyValue(nameof(Column9), ref column9, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Column10
        {
            get => column10;
            set => SetPropertyValue(nameof(Column10), ref column10, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Column11
        {
            get => column11;
            set => SetPropertyValue(nameof(Column11), ref column11, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Column12
        {
            get => column12;
            set => SetPropertyValue(nameof(Column12), ref column12, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Column13
        {
            get => column13;
            set => SetPropertyValue(nameof(Column13), ref column13, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Column14
        {
            get => column14;
            set => SetPropertyValue(nameof(Column14), ref column14, value);
        }
        
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Colimn15
        {
            get => colimn15;
            set => SetPropertyValue(nameof(Colimn15), ref colimn15, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Field1
        {
            get => field1;
            set => SetPropertyValue(nameof(Field1), ref field1, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Address
        {
            get => address;
            set => SetPropertyValue(nameof(Address), ref address, value);
        }
    }
}