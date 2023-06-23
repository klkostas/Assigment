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
    public class PurchasePresenter
    {
        //Fields
        private IPurchaseView view;
        private IPurchaseRepository repository;
        private BindingSource purchasesBindingSource;
        private BindingSource supplierBindingSource;
        private IEnumerable<Purchase> purchaseList;
        private IEnumerable<Supplier> supplierList;

        //Constructor
        public PurchasePresenter(IPurchaseView view, IPurchaseRepository repository)
        {
            this.purchasesBindingSource = new BindingSource();
            this.supplierBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;
            //Subscribe event handler methods to view events
            this.view.AddNewEvent += AddNewPurchase;
            this.view.EditEvent += LoadSelectedPurchaseToEdit;
            this.view.DeleteEvent += DeleteSelectedPurchase;
            this.view.SaveEvent += SavePurchase;
            this.view.CancelEvent += CancelAction;
            //Set Customers binding Source
            this.view.SetPurchaseBindingSource(purchasesBindingSource);
            this.view.SetSuppliersBindingSource(supplierBindingSource);
            //Load Customer list View
            LoadAllPurchaseList();
            PopulateBindingSource();

        }
        private void LoadAllPurchaseList()
        {
            purchaseList = repository.GetAll();
            purchasesBindingSource.DataSource = purchaseList;//Set data source
            supplierList = repository.GetAllSuppliers();
            supplierBindingSource.DataSource = supplierList;
            var purchaseDetails = repository.GetPurchasesDetails();
            var purchaseDetail = (Purchase)purchasesBindingSource.Current;
            foreach (var item in purchaseDetails)
            {
                if (item.PurchaseId == purchaseDetail.PurchaseId)
                {
                    float sum = 0;
                    sum += item.Price * item.Quantity; ;
                    view.Amount = sum.ToString();
                }
            }
        }
        private void PopulateBindingSource()
        {
            purchasesBindingSource.CurrentChanged += new EventHandler(bindingSource_CurrentChanged);
        }
        void bindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var purchaseDetails = repository.GetPurchasesDetails();
            var purchaseDetail = (Purchase)purchasesBindingSource.Current;
            foreach (var item in purchaseDetails)
            {
                if (item.PurchaseId == purchaseDetail.PurchaseId)
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

        private void SavePurchase(object sender, EventArgs e)
        {
            var supplier = (Supplier)supplierBindingSource.Current;
            var purchase = new Purchase();
            purchase.PurchaseId = Convert.ToInt32(view.PurchaseId);
            purchase.DatePurchase = Convert.ToDateTime(view.DatePurchase);
            if (!view.IsEdit)
            {
                purchase.SupplierID = Convert.ToInt32(supplier.Supplierid); 
            }
            else
            {
                purchase.SupplierID = Convert.ToInt32(view.SupplierID);
            }
            purchase.Justification = view.Justification;
            try
            {
                new Common.ModelDataValidation().Validate(purchase);
                if (view.IsEdit)//edit customer
                {
                    repository.Edit(purchase);
                    view.Message = "Purchase edited successfuly";
                }
                else
                {
                    //repository.Add(purchase);
                    //view.Message = ("Purchaseadded successfuly");
                    if (!IsIdNotExist(purchase.SupplierID))
                    {
                        view.Message = "Supplier Id must be unique at all items ";
                    }
                    if (IsIdNotExist(purchase.SupplierID))
                    {
                        repository.Add(purchase);
                        view.Message = "Purchase added successfuly";
                    }
                }
                view.IsSuccessful = true;
                LoadAllPurchaseList();
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
            view.PurchaseId = "0";
            view.DatePurchase = "";
            view.SupplierID = "";
            view.Justification = "";
        }

        private void DeleteSelectedPurchase(object sender, EventArgs e)
        {
            try
            {
                var purchase = (Purchase)purchasesBindingSource.Current;
                repository.Delete(purchase.PurchaseId);
                view.IsSuccessful = true;
                view.Message = "Purchase deleted successfuly";
                LoadAllPurchaseList();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "An error occured, could not delete purchase";
            }
        }

        private void LoadSelectedPurchaseToEdit(object sender, EventArgs e)
        {
            var purchase = (Purchase)purchasesBindingSource.Current;
            view.PurchaseId = purchase.PurchaseId.ToString();
            view.DatePurchase = purchase.DatePurchase.ToString();
            view.SupplierID = purchase.SupplierID.ToString();
            view.Justification = purchase.Justification.ToString();
            view.IsEdit = true;
        }

        private void AddNewPurchase(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }
        private bool IsIdNotExist(int id)
        {
            var purchases = repository.GetAll();
            foreach (var purchase in purchases)
            {
                if (purchase.SupplierID == id)
                    return false;
            }
            return true;
        }
    }
}
