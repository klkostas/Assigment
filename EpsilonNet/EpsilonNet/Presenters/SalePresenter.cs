using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EpsilonNet.Models;
using EpsilonNet.Views;

namespace EpsilonNet.Presenters
{
    public class SalePresenter
    {
        //Fields
        private ISaleView view;
        private ISaleRepository repository;
        private BindingSource salesBindingSource;
        private BindingSource customersBindingSource;
        private IEnumerable<Sale> saleList;
        private IEnumerable<Customer> customerList;

        //Constructor
        public SalePresenter(ISaleView view, ISaleRepository repository)
        {
            this.salesBindingSource = new BindingSource();
            this.customersBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;
            //Subscribe event handler methods to view events
            this.view.AddNewEvent += AddNewSale;
            this.view.EditEvent += LoadSelectedSaleToEdit;
            this.view.DeleteEvent += DeleteSelectedSale;
            this.view.SaveEvent += SaveSale;
            this.view.CancelEvent += CancelAction;
            //Set Customers binding Source
            this.view.SetSalesBindingSource(salesBindingSource);
            this.view.SetCustomersBindingSource(customersBindingSource);
            //Load Customer list View
            LoadAllSaleList();
            PopulateBindingSource();

        }
        private void LoadAllSaleList()
        {
            saleList = repository.GetAll();
            salesBindingSource.DataSource = saleList;//Set data source
            customerList = repository.GetAllCustomers();
            customersBindingSource.DataSource = customerList;
            var saleDetails = repository.GetSalesDetails();
            var saleDetail = (Sale)salesBindingSource.Current;
            foreach (var item in saleDetails)
            {
                if (item.SaleId == saleDetail.SaleId)
                {
                    float sum = 0;
                    sum += item.Price * item.Quantity; ;
                    view.Amount = sum.ToString();
                }
            }
        }
        private void PopulateBindingSource()
        {
            salesBindingSource.CurrentChanged += new EventHandler(bindingSource_CurrentChanged);
        }
        void bindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var saleDetails = repository.GetSalesDetails();
            var saleDetail =(Sale)salesBindingSource.Current;
            foreach (var item in saleDetails)
            {
                if (item.SaleId == saleDetail.SaleId)
                {
                    float sum = 0;
                    sum += item.Price * item.Quantity; ;
                    view.Amount = sum.ToString();
                }
            }
        }
        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void SaveSale(object sender, EventArgs e)
        {
            var customer = (Customer)customersBindingSource.Current;
            var sale = new Sale();
            sale.SaleId = Convert.ToInt32(view.SaleId);
            sale.SaleDate = Convert.ToDateTime(view.SaleDate);
            if (!view.IsEdit)
            {
                sale.CustomerID = Convert.ToInt32(customer.Customerid);
            }
            else
            {
                sale.CustomerID = Convert.ToInt32(view.CustomerID);
            }
            sale.Justification = view.Justification;
            try
            {
                new Common.ModelDataValidation().Validate(sale);
                if (view.IsEdit)//edit customer
                {
                    repository.Edit(sale);
                    view.Message = "Sale edited successfuly";
                }
                else
                {
                    //repository.Add(purchase);
                    //view.Message = ("Purchaseadded successfuly");
                    if (!IsIdNotExist(sale.CustomerID))
                    {
                        view.Message = "Customer Id must be unique at all items ";
                    }
                    if (IsIdNotExist(sale.CustomerID))
                    {
                        repository.Add(sale);
                        view.Message = "Sale added successfuly";
                    }
                }
                view.IsSuccessful = true;
                LoadAllSaleList();
                CleanViewFields();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = ex.Message;
            }
        }

        private void CleanViewFields()
        {
            view.SaleId = "0";
            view.SaleDate = "";
            view.CustomerID = "";
            view.Justification = "";
        }

        private void DeleteSelectedSale(object sender, EventArgs e)
        {
            try
            {
                var sale = (Sale)salesBindingSource.Current;
                repository.Delete(sale.SaleId);
                view.IsSuccessful = true;
                view.Message = "Sale deleted successfuly";
                LoadAllSaleList();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "An error occured, could not delete sale";
            }
        }

        private void LoadSelectedSaleToEdit(object sender, EventArgs e)
        {
            var sale = (Sale)salesBindingSource.Current;
            view.SaleId = sale.SaleId.ToString();
            view.SaleDate = sale.SaleDate.ToString();
            view.CustomerID = sale.CustomerID.ToString();
            view.Justification = sale.Justification.ToString();
            view.IsEdit = true;
        }

        private void AddNewSale(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }
        private bool IsIdNotExist(int id)
        {
            var sales = repository.GetAll();
            foreach (var sale in sales)
            {
                if (sale.CustomerID == id)
                    return false;
            }
            return true;
        }
    }
}
