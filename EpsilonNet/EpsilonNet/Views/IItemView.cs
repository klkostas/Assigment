using EpsilonNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EpsilonNet.Views
{
    public interface IItemView
    {
        string Itemid { get; set; }
        string Description { get; set; }
        string SupplierId { get; set; }
        string DefaultPrice { get; set; }
        string AlarmPrice { get; set; }
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
        void SetItemBindingSource(BindingSource itemList);
        void SetSuppliersBindingSource(BindingSource supplierList);
    }
}
