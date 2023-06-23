using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EpsilonNet.Views
{
    public partial class MainView : Form, IMainView
    {
        public MainView()
        {
            InitializeComponent();
            btnCustomers.Click += delegate { ShowCustomerView?.Invoke(this, EventArgs.Empty); };
            btnSuppliers.Click += delegate { ShowSupplierView?.Invoke(this, EventArgs.Empty); };
            btnItems.Click += delegate { ShowItemView?.Invoke(this, EventArgs.Empty); };
            btnPurchases.Click += delegate { ShowPurchaseView?.Invoke(this, EventArgs.Empty); };
            btnPurchasesDetails.Click += delegate { ShowPurchaseDetailView?.Invoke(this, EventArgs.Empty); };
            btnSales.Click += delegate { ShowSaleView?.Invoke(this, EventArgs.Empty); };
            btnSalesDetails.Click += delegate { ShowSaleDetailView?.Invoke(this, EventArgs.Empty); };
        }

        public event EventHandler ShowCustomerView;
        public event EventHandler ShowSupplierView;
        public event EventHandler ShowItemView;
        public event EventHandler ShowPurchaseView;
        public event EventHandler ShowPurchaseDetailView;
        public event EventHandler ShowSaleView;
        public event EventHandler ShowSaleDetailView;

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
