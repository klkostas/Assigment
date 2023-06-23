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
    public partial class CustomerView : Form, ICustomerView
    {
        //Fields
        private string message;
        private bool isSuccessful;
        private bool isEdit;

        //Constructor
        public CustomerView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            tabControl1.TabPages.Remove(tabPage2);
            btnClose.Click += delegate { this.Close(); };
            
        }

        private void AssociateAndRaiseViewEvents()
        {
            //Add new
            Addbtn.Click += delegate 
            {
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPage1);
                tabControl1.TabPages.Add(tabPage2);
                tabPage2.Text = "Add new customer";
                tabPage2.Show();
            };
            //Edit
            Editbtn.Click += delegate { EditEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPage1);
                tabControl1.TabPages.Add(tabPage2);
                tabPage2.Text = "Edit customer";
            };
            //Delete
            Deletebtn.Click += delegate { 
              var result = MessageBox.Show("Are you sure you want to delete the selected customer?", "Warning",
                    MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if(result== DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };
            //Save
            Savebtn.Click += delegate { 
                SaveEvent?.Invoke(this, EventArgs.Empty); 
                if(isSuccessful)
                {
                    tabControl1.TabPages.Remove(tabPage2);
                    tabControl1.TabPages.Add(tabPage1);
                }
                MessageBox.Show(Message);
            };
            //Cancel
            Cancelbtn.Click += delegate { 
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Add(tabPage1);
            };
        }

        public string Customerid 
        { get => IDtext.Text; 
          set => IDtext.Text = value; 
        }
        public string CName 
        { get => NameText.Text; 
          set => NameText.Text = value; 
        }
        public string SurName 
        { get => SurnameText.Text; 
          set => SurnameText.Text = value; 
        }
        public string Tin 
        { get => TinText.Text; 
          set => TinText.Text = value; 
        }
        public string Address 
        { get => AddressText.Text; 
          set => AddressText.Text = value; 
        }
        public string Phone 
        { get => PhoneText.Text; 
          set => PhoneText.Text = value; 
        }
        public string Fax 
        { get => FaxText.Text; 
          set => FaxText.Text = value; 
        }
        public bool IsEdit 
        { get => isEdit; 
          set => isEdit = value; 
        }
        public bool IsSuccessful 
        { get => isSuccessful; 
          set => isSuccessful = value; 
        }
        public string Message 
        { get => message; 
          set => message = value; 
        }

        //Events
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        //Methods
        public void SetCustomerBindingSource(BindingSource customerList)
        {
            CustomersDataGridView.DataSource = customerList;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        //Singleton Pattern(Open a single form instance)
        private static CustomerView instance;
        public static CustomerView GetInstance(Form parentContainer)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new CustomerView();
                instance.MdiParent = parentContainer;
                instance.FormBorderStyle = FormBorderStyle.None;
                instance.Dock = DockStyle.Fill;
            }
            
            else
            {
                if (instance.WindowState == FormWindowState.Minimized)
                    instance.WindowState = FormWindowState.Normal;
                instance.BringToFront();
            }
            return instance;
        }
    }
}
