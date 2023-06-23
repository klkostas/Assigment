using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EpsilonNet.Views
{
    public interface ISupplierView
    {
        string Supplierid { get; set; }
        string CName { get; set; }
        string SurName { get; set; }
        string Tin { get; set; }
        string Address { get; set; }
        string Phone { get; set; }
        string Fax { get; set; }

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
        void SetSupplierBindingSource(BindingSource supplierList);
    }
}
