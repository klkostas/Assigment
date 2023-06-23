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
    public class SupplierPresenter
    {
        //Fields
        private ISupplierView view;
        private ISupplierRepository repository;
        private BindingSource suppliersBindingSource;
        private IEnumerable<Supplier> supplierList;

        //Constructor
        public SupplierPresenter(ISupplierView view, ISupplierRepository repository)
        {
            this.suppliersBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;
            //Subscribe event handler methods to view events
            this.view.AddNewEvent += AddNewSupplier;
            this.view.EditEvent += LoadSelectedSupplierToEdit;
            this.view.DeleteEvent += DeleteSelectedSupplier;
            this.view.SaveEvent += SaveSupplier;
            this.view.CancelEvent += CancelAction;
            //Set Customers binding Source
            this.view.SetSupplierBindingSource(suppliersBindingSource);
            //Load Customer list View
            LoadAllSupplierList();

        }

        private void LoadAllSupplierList()
        {
            supplierList = repository.GetAll();
            suppliersBindingSource.DataSource = supplierList;//Set data source
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void SaveSupplier(object sender, EventArgs e)
        {
            var supplier = new Supplier();
            supplier.Supplierid = Convert.ToInt32(view.Supplierid);
            supplier.Name = view.CName;
            supplier.SurName = view.SurName;
            supplier.Tin = view.Tin;
            supplier.Address = view.Address;
            supplier.Phone = view.Phone;
            supplier.Fax = view.Fax;
            try
            {
                new Common.ModelDataValidation().Validate(supplier);
                if ((view.IsEdit))//edit customer
                {
                    repository.Edit(supplier);
                    view.Message = "Supplier edited successfuly";
                }
                else
                {
                    if (ValidateAFM(supplier.Tin))
                    {
                        if (IsAFMNotExist(supplier.Tin))
                        {
                            repository.Add(supplier);
                            view.Message = "Supplier added successfuly";
                        }
                        else
                        {
                            view.Message = "Supplier's Tin already exists";
                        }
                    }
                    else
                    {
                        view.Message = "Supplier's Tin is not valid";
                    }
                }
                view.IsSuccessful = true;
                LoadAllSupplierList();
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
            view.Supplierid = "0";
            view.CName = "";
            view.SurName = "";
            view.Tin = "";
            view.Address = "";
            view.Phone = "";
            view.Fax = "";
        }

        private void DeleteSelectedSupplier(object sender, EventArgs e)
        {
            try
            {
                var supplier = (Supplier)suppliersBindingSource.Current;
                repository.Delete(supplier.Supplierid);
                view.IsSuccessful = true;
                view.Message = "Supplier deleted successfuly";
                LoadAllSupplierList();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "An error occured, could not delete supplier";
            }
        }

        private void LoadSelectedSupplierToEdit(object sender, EventArgs e)
        {
            var supplier = (Supplier)suppliersBindingSource.Current;
            view.Supplierid = supplier.Supplierid.ToString();
            view.CName = supplier.Name;
            view.SurName = supplier.SurName;
            view.Tin = supplier.Tin;
            view.Address = supplier.Address;
            view.Phone = supplier.Phone;
            view.Fax = supplier.Fax;
            view.IsEdit = true;

        }

        private void AddNewSupplier(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }

        // method gia elegxo egkyrothtas AFM
        public static bool ValidateAFM(string afm)
        {
            int _numAFM = 0;
            if (afm.Length != 9 || !int.TryParse(afm, out _numAFM))
                return false;
            else
            {
                double sum = 0;
                int iter = afm.Length - 1;
                afm.ToCharArray().Take(iter).ToList().ForEach(c =>
                {
                    sum += double.Parse(c.ToString()) * Math.Pow(2, iter);
                    iter--;
                });
                if (sum % 11 == double.Parse(afm.Substring(8)) || double.Parse(afm.Substring(8)) == 0)
                    return true;
                else
                    return false;
            }
        }
        private bool IsAFMNotExist(string afm)
        {
            var suppliers = repository.GetAll();
            foreach (var supplier in suppliers)
            {
                if (supplier.Tin == afm)
                    return false;
            }
            return true;
        }
    }
}
