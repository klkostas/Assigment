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
    public class CustomerPresenter
    {
        //Fields
        private ICustomerView view;
        private ICustomerRepository repository;
        private BindingSource customersBindingSource;
        private IEnumerable<Customer> customerList;

        //Constructor
        public CustomerPresenter(ICustomerView view, ICustomerRepository repository)
        {
            this.customersBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;
            //Subscribe event handler methods to view events
            this.view.AddNewEvent += AddNewCustomer;
            this.view.EditEvent += LoadSelectedCustomerToEdit;
            this.view.DeleteEvent += DeleteSelectedCustomer;
            this.view.SaveEvent += SaveCustomer;
            this.view.CancelEvent += CancelAction;
            //Set Customers binding Source
            this.view.SetCustomerBindingSource(customersBindingSource);
            //Load Customer list View
            LoadAllCustomerList();

        }
        //public static string pattern = @"\d+";
        //public static Regex regex = new Regex(pattern);
        private void LoadAllCustomerList()
        {
            customerList = repository.GetAll();
            customersBindingSource.DataSource = customerList;//Set data source
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void SaveCustomer(object sender, EventArgs e)
        {
            
            var customer = new Customer();
            customer.Customerid = Convert.ToInt32(view.Customerid);
            customer.Name = view.CName ;
            customer.SurName = view.SurName;
            customer.Tin = view.Tin;
            customer.Address = view.Address;
            customer.Phone = view.Phone;
            customer.Fax = view.Fax;
            try
            {
                new Common.ModelDataValidation().Validate(customer);
                if ((view.IsEdit))//edit customer
                {
                    repository.Edit(customer);
                    view.Message = "Customer edited successfuly";
                }
                else
                {
                    if(ValidateAFM(customer.Tin))
                    {
                        if(IsAFMNotExist(customer.Tin))
                        {
                            repository.Add(customer);
                            view.Message = "Customer added successfuly";
                        }
                        else
                        {
                            view.Message = "Customer's Tin already exists";
                        }
                    }
                    else
                    {
                        view.Message = "Customer's Tin is not valid";
                    }
                }
                view.IsSuccessful = true;
                LoadAllCustomerList();
                CleanViewFields();
            }
            catch(Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = ex.Message;
            }
        }

        private void CleanViewFields()
        {
            view.Customerid = "0";
            view.CName = "";
            view.SurName = "";
            view.Tin = "";
            view.Address = "";
            view.Phone = "";
            view.Fax = "";
        }

        private void DeleteSelectedCustomer(object sender, EventArgs e)
        {
            try 
            {
                var customer = (Customer)customersBindingSource.Current;
                repository.Delete(customer.Customerid);
                view.IsSuccessful = true;
                view.Message = "Customer deleted successfuly";
                LoadAllCustomerList();
            }
            catch(Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "An error occured, could not delete customer";
            }
        }

        private void LoadSelectedCustomerToEdit(object sender, EventArgs e)
        {
            var customer = (Customer)customersBindingSource.Current;
            view.Customerid = customer.Customerid.ToString();
            view.CName = customer.Name;
            view.SurName = customer.SurName;
            view.Tin = customer.Tin;
            view.Address = customer.Address;
            view.Phone = customer.Phone;
            view.Fax = customer.Fax;
            view.IsEdit = true;

        }

        private void AddNewCustomer(object sender, EventArgs e)
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
            var customers = repository.GetAll();
            foreach( var customer in customers)
            {
                if (customer.Tin == afm)
                    return false;
            }
                return true;
        }
    }
}
