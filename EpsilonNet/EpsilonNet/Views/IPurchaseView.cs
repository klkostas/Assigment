using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EpsilonNet.Views
{
    public interface IPurchaseView
    {
        string PurchaseId { get; set; }
        string DatePurchase { get; set; }
        string SupplierID { get; set; }
        string Justification { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }
        string Amount { get; set; }

        //Events
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;
        //Methods
        void SetPurchaseBindingSource(BindingSource purchaseList);
        void SetSuppliersBindingSource(BindingSource supplierList);
    }
}
