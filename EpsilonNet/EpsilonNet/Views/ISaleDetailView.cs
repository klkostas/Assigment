using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EpsilonNet.Views
{
    public interface ISaleDetailView
    {
        string SaleDetailId { get; set; }
        string SaleId { get; set; }
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
        void SetSaleDetailBindingSource(BindingSource saleDetailList);
        void SetSaleBindingSource(BindingSource saleList);
        void SetItemsBindingSource(BindingSource itemList);
    }
}
