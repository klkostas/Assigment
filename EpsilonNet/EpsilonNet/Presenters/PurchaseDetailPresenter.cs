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
    public class PurchaseDetailPresenter
    {
        //Fields
        private IPurchaseDetailView view;
        private IPurchaseDetailRepository repository;
        private BindingSource purchasesBindingSource;
        private BindingSource itemsBindingSource;
        private BindingSource purchasesDetailBindingSource;
        private IEnumerable<PurchaseDetail> purchaseDetailList;
        private IEnumerable<Purchase> purchaseList;
        private IEnumerable<Item> itemList;

        //Constructor
        public PurchaseDetailPresenter(IPurchaseDetailView view, IPurchaseDetailRepository repository)
        {
            this.purchasesDetailBindingSource = new BindingSource();
            this.purchasesBindingSource = new BindingSource();
            this.itemsBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;
            //Subscribe event handler methods to view events
            this.view.AddNewEvent += AddNewPurchaseDetail;
            this.view.EditEvent += LoadSelectedPurchaseDetailToEdit;
            this.view.DeleteEvent += DeleteSelectedPurchaseDetail;
            this.view.SaveEvent += SavePurchaseDetail;
            this.view.CancelEvent += CancelAction;
            //Set Customers binding Source
            this.view.SetPurchaseBindingSource(purchasesBindingSource);
            this.view.SetPurchaseDetailBindingSource(purchasesDetailBindingSource);
            this.view.SetItemsBindingSource(itemsBindingSource);          
            //Load Customer list View
            LoadAllPurchaseDetailList();
            PopulateBindingSource();
        }

        private void LoadAllPurchaseDetailList()
        {
            purchaseDetailList = repository.GetAll();
            purchasesDetailBindingSource.DataSource = purchaseDetailList;//Set data source
            purchaseList = repository.GetAllPurchases();
            purchasesBindingSource.DataSource = purchaseList;
            itemList = repository.GetAllItems();
            itemsBindingSource.DataSource = itemList;
            var purchaseDetail = (PurchaseDetail)purchasesDetailBindingSource.Current;
            foreach (var item in purchaseDetailList)
            {
                if (item.PurchaseDetailId == purchaseDetail.PurchaseDetailId)
                {
                    float sum = 0;
                    sum += item.Price * item.Quantity; ;
                    view.Amount = sum.ToString();
                }
            }
        }
        private void PopulateBindingSource()
        {
            purchasesDetailBindingSource.CurrentChanged += new EventHandler(bindingSource_CurrentChanged);
        }
        void bindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var purchaseDetail = (PurchaseDetail)purchasesDetailBindingSource.Current;
            foreach (var item in purchaseDetailList)
            {
                if (item.PurchaseDetailId == purchaseDetail.PurchaseDetailId)
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

        private void SavePurchaseDetail(object sender, EventArgs e)
        {
            var purchase = (Purchase)purchasesBindingSource.Current;
            var item = (Item)itemsBindingSource.Current;
            var purchaseDetail = new PurchaseDetail();
            purchaseDetail.PurchaseDetailId = Convert.ToInt32(view.PurchaseDetailId);
            if (!view.IsEdit)
            {
                purchaseDetail.PurchaseId = Convert.ToInt32(purchase.PurchaseId);
                purchaseDetail.ItemId = Convert.ToInt32(item.Itemid);
            }
            else
            {
                purchaseDetail.PurchaseId = Convert.ToInt32(view.PurchaseId);
                purchaseDetail.ItemId = Convert.ToInt32(view.ItemId);
            }
            purchaseDetail.Price = Single.Parse(view.Price);
            purchaseDetail.Quantity = Convert.ToInt32(view.Quantity);
            try
            {
                new Common.ModelDataValidation().Validate(purchaseDetail);
                if (view.IsEdit)//edit customer
                {
                    repository.Edit(purchaseDetail);
                    view.Message = "Purchase detail edited successfuly";
                }
                else
                {
                    if (!IsPurchaseIdNotExist(purchaseDetail.PurchaseId))
                    {
                        view.Message = "Purchase Id must be unique at all items ";
                    }
                    if (!IsItemIdNotExist(purchaseDetail.ItemId))
                    {
                        view.Message = "Item Id must be unique at all items ";
                    }
                    if ((IsPurchaseIdNotExist(purchaseDetail.PurchaseId)) && (IsItemIdNotExist(purchaseDetail.ItemId)))
                    {
                        repository.Add(purchaseDetail);
                        view.Message = "Purchase detail added successfuly";
                    }
                }
                view.IsSuccessful = true;
                LoadAllPurchaseDetailList();
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
            view.PurchaseDetailId = "0";
            view.PurchaseId = "";
            view.ItemId = "";
            view.Price = "";
            view.Quantity = "1";
        }

        private void DeleteSelectedPurchaseDetail(object sender, EventArgs e)
        {
            try
            {
                var purchaseDetail = (PurchaseDetail)purchasesDetailBindingSource.Current;
                repository.Delete(purchaseDetail.PurchaseDetailId);
                view.IsSuccessful = true;
                view.Message = "Purchase detail deleted successfuly";
                LoadAllPurchaseDetailList();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "An error occured, could not delete purchase detail";
            }
        }

        private void LoadSelectedPurchaseDetailToEdit(object sender, EventArgs e)
        {
            var purchaseDetail = (PurchaseDetail)purchasesDetailBindingSource.Current;
            view.PurchaseDetailId = purchaseDetail.PurchaseDetailId.ToString();
            view.PurchaseId = purchaseDetail.PurchaseId.ToString();
            view.ItemId = purchaseDetail.ItemId.ToString();
            view.Price = purchaseDetail.Price.ToString();
            view.Quantity = purchaseDetail.Quantity.ToString();
            view.IsEdit = true;
        }

        private void AddNewPurchaseDetail(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }

        private bool IsPurchaseIdNotExist(int id)
        {
            var purchasesDetails = repository.GetAll();
            foreach (var purchaseDetail in purchasesDetails)
            {
                if (purchaseDetail.PurchaseId == id)
                    return false;
            }
            return true;
        }

        private bool IsItemIdNotExist(int id)
        {
            var purchasesDetails = repository.GetAll();
            foreach (var purchaseDetail in purchasesDetails)
            {
                if (purchaseDetail.ItemId == id)
                    return false;
            }
            return true;
        }
    }
}
