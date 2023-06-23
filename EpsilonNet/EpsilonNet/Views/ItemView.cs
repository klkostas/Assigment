using EpsilonNet.Models;
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
    public partial class ItemView : Form, IItemView
    {
        //Fields
        private string message;
        private bool isSuccessful;
        private bool isEdit;

        //Constructor
        public ItemView()
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
                tabPage2.Text = "Add new item";
                tabPage2.Show();
            };
            //Edit
            Editbtn.Click += delegate {
                EditEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPage1);
                tabControl1.TabPages.Add(tabPage2);
                tabPage2.Text = "Edit item";
            };
            //Delete
            Deletebtn.Click += delegate {
                var result = MessageBox.Show("Are you sure you want to delete the selected item?", "Warning",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };
            //Save
            Savebtn.Click += delegate {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (isSuccessful)
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
        public string Itemid
        {
            get => IDtext.Text;
            set => IDtext.Text = value;
        }
        public string Description
        {
            get => DescriptionText.Text;
            set => DescriptionText.Text = value;
        }
        public string SupplierId 
        {
            get => SupplierIdText.Text;
            set => SupplierIdText.Text = value;
        }
        public string AlarmPrice
        {
            get => AlarmPriceText.Text;
            set => AlarmPriceText.Text = value;
        }
        public string DefaultPrice
        {
            get => DefaultPriceText.Text;
            set => DefaultPriceText.Text = value;
        }
        public bool IsEdit
        {
            get => isEdit;
            set => isEdit = value;
        }
        public bool IsSuccessful
        {
            get => isSuccessful;
            set => isSuccessful = value;
        }
        public string Message
        {
            get => message;
            set => message = value;
        }

        //Events
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        //Methods
        public void SetItemBindingSource(BindingSource itemList)
        {
            ItemsDataGridView.DataSource = itemList;
        }

        public void SetSuppliersBindingSource(BindingSource supplierList)
        {
            SuppliersDataGridView.DataSource = supplierList;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        //Singleton Pattern(Open a single form instance
        private static ItemView instance;
        public static ItemView GetInstance(Form parentContainer)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new ItemView();
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

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
