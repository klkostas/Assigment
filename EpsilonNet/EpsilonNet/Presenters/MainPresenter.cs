using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EpsilonNet.Views;
using EpsilonNet.Models;
using EpsilonNet._Repositories;
using System.Windows.Forms;

namespace EpsilonNet.Presenters
{
    public class MainPresenter
    {
        private IMainView mainView;
        private readonly string sqlConnectionString;

        public MainPresenter(IMainView mainView, string sqlConnectionString)
        {
            this.mainView = mainView;
            this.sqlConnectionString = sqlConnectionString;
            this.mainView.ShowCustomerView += ShowCustomerView;
            this.mainView.ShowSupplierView += ShowSupplierView;
            this.mainView.ShowItemView += ShowItemView;
            this.mainView.ShowPurchaseView += ShowPurchaseView;
            this.mainView.ShowPurchaseDetailView += ShowPurchaseDetailView;
            this.mainView.ShowSaleView += ShowSaleView;
            this.mainView.ShowSaleDetailView += ShowSaleDetailView;
        }

        private void ShowCustomerView(object sender, EventArgs e)
        {
            CustomerView customerView = CustomerView.GetInstance((MainView)mainView);
            ICustomerView view = customerView;
            ICustomerRepository repository = new CustomerRepository(sqlConnectionString);
            new CustomerPresenter(view, repository);
            customerView.Show();
        }

        private void ShowSupplierView(object sender, EventArgs e)
        {
            SupplierView supplierView = SupplierView.GetInstance((MainView)mainView);
            ISupplierView view = supplierView;
            ISupplierRepository repository = new SupplierRepository(sqlConnectionString);
            new SupplierPresenter(view, repository);
            supplierView.Show();
        }

        private void ShowItemView(object sender, EventArgs e)
        {
            ItemView itemView = ItemView.GetInstance((MainView)mainView);
            IItemView view = itemView;
            IItemRepository repository = new ItemRepository(sqlConnectionString);
            new ItemPresenter(view, repository);
            itemView.Show();
        }

        private void ShowPurchaseView(object sender, EventArgs e)
        {
            PurchaseView purchaseView = PurchaseView.GetInstance((MainView)mainView);
            IPurchaseView view = purchaseView;
            IPurchaseRepository repository = new PurchaseRepository(sqlConnectionString);
            new PurchasePresenter(view, repository);
            purchaseView.Show();
        }

        private void ShowPurchaseDetailView(object sender, EventArgs e)
        {
            PurchaseDetailView purchaseDetailView = PurchaseDetailView.GetInstance((MainView)mainView);
            IPurchaseDetailView view = purchaseDetailView;
            IPurchaseDetailRepository repository = new PurchaseDetailRepository(sqlConnectionString);
            new PurchaseDetailPresenter(view, repository);
            purchaseDetailView.Show();
        }

        private void ShowSaleView(object sender, EventArgs e)
        {
            SaleView saleView= SaleView.GetInstance((MainView)mainView);
            ISaleView view = saleView;
            ISaleRepository repository = new SaleRepository(sqlConnectionString);
            new SalePresenter(view, repository);
            saleView.Show();
        }
        private void ShowSaleDetailView(object sender, EventArgs e)
        {
            SaleDetailView saleDetailView = SaleDetailView.GetInstance((MainView)mainView);
            ISaleDetailView view = saleDetailView;
            ISaleDetailRepository repository = new SaleDetailRepository(sqlConnectionString);
            new SaleDetailPresenter(view, repository);
            saleDetailView.Show();
        }
    }
}
