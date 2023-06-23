using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpsilonNet.Views
{
    public interface IMainView
    {
        event EventHandler ShowCustomerView;
        event EventHandler ShowSupplierView;
        event EventHandler ShowItemView;
        event EventHandler ShowPurchaseView;
        event EventHandler ShowPurchaseDetailView;
        event EventHandler ShowSaleView;
        event EventHandler ShowSaleDetailView;
    }
}
