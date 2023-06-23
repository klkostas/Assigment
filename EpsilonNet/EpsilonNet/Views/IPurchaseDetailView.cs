using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EpsilonNet.Views
{
    public interface IPurchaseDetailView
    {
        string PurchaseDetailId { get; set; }
        string PurchaseId { get; set; }
        string ItemId { get; set; }
        string Price { get; set; }
        string Quantity { get; set; }
        string Amount { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }

        //Events
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;
        //Methods
        void SetPurchaseDetailBindingSource(BindingSource purchaseDetailList);
        void SetPurchaseBindingSource(BindingSource purchaseList);
        void SetItemsBindingSource(BindingSource itemList);
    }
}
