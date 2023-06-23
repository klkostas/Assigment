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
    public class SaleDetailPresenter
    {
        //Fields
        private ISaleDetailView view;
        private ISaleDetailRepository repository;
        private BindingSource salesBindingSource;
        private BindingSource itemsBindingSource;
        private BindingSource salesDetailBindingSource;
        private IEnumerable<SaleDetail> saleDetailList;
        private IEnumerable<Sale> saleList;
        private IEnumerable<Item> itemList;

        //Constructor
        public SaleDetailPresenter(ISaleDetailView view, ISaleDetailRepository repository)
        {
            this.salesDetailBindingSource = new BindingSource();
            this.salesBindingSource = new BindingSource();
            this.itemsBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;
            //Subscribe event handler methods to view events
            this.view.AddNewEvent += AddNewPurchaseDetail;
            this.view.EditEvent += LoadSelectedSaleDetailToEdit;
            this.view.DeleteEvent += DeleteSelectedPurchaseDetail;
            this.view.SaveEvent += SaveSaleDetail;
            this.view.CancelEvent += CancelAction;
            //Set Customers binding Source
            this.view.SetSaleBindingSource(salesBindingSource);
            this.view.SetSaleDetailBindingSource(salesDetailBindingSource);
            this.view.SetItemsBindingSource(itemsBindingSource);
            //Load Customer list View
            LoadAllSaleDetailList();
            PopulateBindingSource();
        }

        private void LoadAllSaleDetailList()
        {
            saleDetailList = repository.GetAll();
            salesDetailBindingSource.DataSource = saleDetailList;//Set data source
            saleList = repository.GetAllSales();
            salesBindingSource.DataSource = saleList;
            itemList = repository.GetAllItems();
            itemsBindingSource.DataSource = itemList;
            var saleDetail = (SaleDetail)salesDetailBindingSource.Current;
            foreach (var item in saleDetailList)
            {
                if (item.SaleDetailId == saleDetail.SaleDetailId)
                {
                    float sum = 0;
                    sum += item.Price * item.Quantity; ;
                    view.Amount = sum.ToString();
                }
            }
        }
        private void PopulateBindingSource()
        {
            salesDetailBindingSource.CurrentChanged += new EventHandler(bindingSource_CurrentChanged);
        }
        void bindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var saleDetail = (SaleDetail)salesDetailBindingSource.Current;
            foreach (var item in saleDetailList)
            {
                if (item.SaleDetailId == saleDetail.SaleDetailId)
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

        private void SaveSaleDetail(object sender, EventArgs e)
        {
            var sale = (Sale)salesBindingSource.Current;
            var item = (Item)itemsBindingSource.Current;
            var saleDetail = new SaleDetail();
            saleDetail.SaleDetailId = Convert.ToInt32(view.SaleDetailId);
            if (!view.IsEdit)
            {
                saleDetail.SaleId = Convert.ToInt32(sale.SaleId);
                saleDetail.ItemId = Convert.ToInt32(item.Itemid);
            }
            else
            {
                saleDetail.SaleId = Convert.ToInt32(view.SaleId);
                saleDetail.ItemId = Convert.ToInt32(view.ItemId);
            }
            saleDetail.Price = Single.Parse(view.Price);
            saleDetail.Quantity = Convert.ToInt32(view.Quantity);
            try
            {
                new Common.ModelDataValidation().Validate(saleDetail);
                if (view.IsEdit)//edit customer
                {
                    repository.Edit(saleDetail);
                    view.Message = "Sale detail edited successfuly";
                }
                else
                {
                    if (!IsPurchaseIdNotExist(saleDetail.SaleId))
                    {
                        view.Message = "Sale Id must be unique at all items ";
                    }
                    if (!IsItemIdNotExist(saleDetail.ItemId))
                    {
                        view.Message = "Item Id must be unique at all items ";
                    }
                    if ((IsPurchaseIdNotExist(saleDetail.SaleId)) && (IsItemIdNotExist(saleDetail.ItemId)))
                    {
                        repository.Add(saleDetail);
                        view.Message = "Sale detail added successfuly";
                    }
                }
                view.IsSuccessful = true;
                LoadAllSaleDetailList();
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
            view.SaleDetailId = "0";
            view.SaleId = "";
            view.ItemId = "";
            view.Price = "";
            view.Quantity = "1";
        }

        private void DeleteSelectedPurchaseDetail(object sender, EventArgs e)
        {
            try
            {
                var saleDetail = (SaleDetail)salesDetailBindingSource.Current;
                repository.Delete(saleDetail.SaleDetailId);
                view.IsSuccessful = true;
                view.Message = "Sale detail deleted successfuly";
                LoadAllSaleDetailList();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "An error occured, could not delete sale detail";
            }
        }

        private void LoadSelectedSaleDetailToEdit(object sender, EventArgs e)
        {
            var saleDetail = (SaleDetail)salesDetailBindingSource.Current;
            view.SaleDetailId = saleDetail.SaleDetailId.ToString();
            view.SaleId = saleDetail.SaleId.ToString();
            view.ItemId = saleDetail.ItemId.ToString();
            view.Price = saleDetail.Price.ToString();
            view.Quantity = saleDetail.Quantity.ToString();
            view.IsEdit = true;
        }

        private void AddNewPurchaseDetail(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }

        private bool IsPurchaseIdNotExist(int id)
        {
            var salesDetails = repository.GetAll();
            foreach (var saleDetail in salesDetails)
            {
                if (saleDetail.SaleId == id)
                    return false;
            }
            return true;
        }

        private bool IsItemIdNotExist(int id)
        {
            var salesDetails = repository.GetAll();
            foreach (var saleDetail in salesDetails)
            {
                if (saleDetail.ItemId == id)
                    return false;
            }
            return true;
        }
    }
}
