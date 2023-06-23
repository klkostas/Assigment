using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EpsilonNet.Views
{
    public interface ISaleView
    {
        string SaleId { get; set; }
        string SaleDate { get; set; }
        string CustomerID { get; set; }
        string Justification { get; set; }
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
        void SetSalesBindingSource(BindingSource saleList);
        void SetCustomersBindingSource(BindingSource customerList);
    }
}
