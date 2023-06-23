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
    public partial class PurchaseDetailView : Form, IPurchaseDetailView
    {
        //Fields
        private string message;
        private bool isSuccessful;
        private bool isEdit;

        //Constructor
        public PurchaseDetailView()
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
                tabPage2.Text = "Add new purchase detail";
                tabPage2.Show();
            };
            //Edit
            Editbtn.Click += delegate
            {
                EditEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPage1);
                tabControl1.TabPages.Add(tabPage2);
                tabPage2.Text = "Edit purchase detail";
            };
            //Delete
            Deletebtn.Click += delegate
            {
                var result = MessageBox.Show("Are you sure you want to delete the selected purchase detail?", "Warning",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };
            //Save
            Savebtn.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (isSuccessful)
                {
                    tabControl1.TabPages.Remove(tabPage2);
                    tabControl1.TabPages.Add(tabPage1);
                }
                MessageBox.Show(Message);
            };
            //Cancel
            Cancelbtn.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Add(tabPage1);
            };
        }
        public string PurchaseDetailId
        {
            get => IdText.Text;
            set => IdText.Text = value;
        }
        public string PurchaseId
        {
            get => PurchaseText.Text;
            set => PurchaseText.Text = value;
        }
        public string ItemId
        {
            get => ItemIdText.Text;
            set => ItemIdText.Text = value;
        }
        public string Price
        {
            get => PriceText.Text;
            set => PriceText.Text = value;
        }
        public string Quantity
        {
            get => QuantityText.Text;
            set => QuantityText.Text = value;
        }

        public string Amount
        {
            get => amounttext.Text;
            set => amounttext.Text = value;
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
        public void SetPurchaseDetailBindingSource(BindingSource purchaseDetailList)
        {
            PurchasesDetailsDataGridView.DataSource = purchaseDetailList;
        }
        public void SetPurchaseBindingSource(BindingSource purchaseList)
        {
            PurchasesDataGridView.DataSource = purchaseList;
        }

        public void SetItemsBindingSource(BindingSource itemList)
        {
            ItemsDataGridView.DataSource = itemList;
        }

        //Singleton Pattern(Open a single form instance)
        private static PurchaseDetailView instance;
        public static PurchaseDetailView GetInstance(Form parentContainer)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new PurchaseDetailView();
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

