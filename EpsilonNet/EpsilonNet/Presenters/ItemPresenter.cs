using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EpsilonNet.Models;
using EpsilonNet.Views;

namespace EpsilonNet.Presenters
{
    public class ItemPresenter
    {
        //Fields
        private IItemView view;
        private IItemRepository repository;
        private BindingSource itemsBindingSource;
        private BindingSource supplierBindingSource;
        private IEnumerable<Item> itemList;
        private IEnumerable<Supplier> supplierList;

        //Constructor
        public ItemPresenter(IItemView view, IItemRepository repository)
        {
            this.itemsBindingSource = new BindingSource();
            this.supplierBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;
            //Subscribe event handler methods to view events
            this.view.AddNewEvent += AddNewCustomer;
            this.view.EditEvent += LoadSelectedItemToEdit;
            this.view.DeleteEvent += DeleteSelectedItem;
            this.view.SaveEvent += SaveItem;
            this.view.CancelEvent += CancelAction;
            //Set Customers binding Source
            this.view.SetItemBindingSource(itemsBindingSource);
            this.view.SetSuppliersBindingSource(supplierBindingSource);

            //Load Customer list View
            LoadAllItemList();

        }

        private void LoadAllItemList()
        {
            itemList = repository.GetAll();
            itemsBindingSource.DataSource = itemList;//Set data source
            supplierList = repository.GetAllSuppliers();
            supplierBindingSource.DataSource = supplierList;           
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void SaveItem(object sender, EventArgs e)
        {
            var supplier = (Supplier)supplierBindingSource.Current;
            var item = new Item();
            item.Itemid = Convert.ToInt32(view.Itemid);
            item.Description = view.Description;
            if (!view.IsEdit)
            {
                item.SupplierId = Convert.ToInt32(supplier.Supplierid);            
            }
            else
            {
                item.SupplierId = Convert.ToInt32(view.SupplierId);
            }
            item.DefaultPrice = Single.Parse(view.DefaultPrice);
            item.AlarmPrice = Single.Parse(view.AlarmPrice);
            try
            {
                new Common.ModelDataValidation().Validate(item);
                if ((view.IsEdit) && (Comparison(item.AlarmPrice, item.DefaultPrice) && CompareWithZero(item.AlarmPrice, item.DefaultPrice)))//edit customer
                {
                    repository.Edit(item);
                    view.Message = "Item edited successfuly";
                }
                else
                {
                    if(!(Comparison(item.AlarmPrice, item.DefaultPrice)))
                    {
                        view.Message = "Alarm Price must be greater than Default Price";
                    }
                    if(!CompareWithZero(item.AlarmPrice, item.DefaultPrice))
                    {
                        view.Message = "Alarm Price or Default Price must be greater than 0";
                    }
                    if(!IsIdNotExist(item.SupplierId))
                    {
                        view.Message = "Supplier Id must be unique at all items ";
                    }
                    if((Comparison(item.AlarmPrice, item.DefaultPrice) && CompareWithZero(item.AlarmPrice, item.DefaultPrice)))
                    {
                        repository.Add(item);
                        view.Message = "Item added successfuly";
                    }
                }
                view.IsSuccessful = true;
                LoadAllItemList();
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
            view.Itemid = "0";
            view.Description = "";
            view.SupplierId = "";
            view.DefaultPrice = "0";
            view.AlarmPrice = "0";
        }

        private void DeleteSelectedItem(object sender, EventArgs e)
        {
            try
            {
                var item = (Item)itemsBindingSource.Current;
                repository.Delete(item.Itemid);
                view.IsSuccessful = true;
                view.Message = "Item deleted successfuly";
                LoadAllItemList();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "An error occured, could not delete customer";
            }
        }

        private void LoadSelectedItemToEdit(object sender, EventArgs e)
        {
            var item = (Item)itemsBindingSource.Current;
            view.Itemid = item.Itemid.ToString();
            view.Description = item.Description;
            view.SupplierId = item.SupplierId.ToString();
            view.DefaultPrice = item.DefaultPrice.ToString();
            view.AlarmPrice = item.AlarmPrice.ToString();
            view.IsEdit = true;

        }
        private void AddNewCustomer(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }

        private bool Comparison(double alarmPrice, double defaultPrice)
        {
            if (defaultPrice < alarmPrice)
                return true;
            else
                return false;
        }
        private bool CompareWithZero(double alarmPrice, double defaultPrice)
        {
            if (defaultPrice>0 && alarmPrice>0)
                return true;
            else
                return false;
        }
        private bool IsIdNotExist(int id)
        {
            var items = repository.GetAll();
            foreach (var item in items)
            {
                if (item.SupplierId == id)
                    return false;
            }
            return true;
        }
    }
}
