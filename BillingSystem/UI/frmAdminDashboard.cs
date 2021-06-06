using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BillingSystem.UI;

namespace BillingSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void menuStripTop_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //frmUsers user = new frmUsers();
            //user.Show();
        }
        private void Users_Click(object sender, EventArgs e)
        {
            frmUsers user = new frmUsers();
            user.Show();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblLoggedInUser.Text = frmLogin.loggedIn;
        }

        private void Category_Click(object sender, EventArgs e)
        {
            frmCategories category = new frmCategories();
            category.Show();
        }

        private void Products_Click(object sender, EventArgs e)
        {
            frmProducts product = new frmProducts();
            product.Show();
        }

        private void DealerCustomer_Click(object sender, EventArgs e)
        {
            frmDealCust deaCust = new frmDealCust();
            deaCust.Show();
        }

        private void Transcriptions_Click(object sender, EventArgs e)
        {
            frmTransaction transaction = new frmTransaction();
            transaction.Show();
        }

        private void Inventory_Click(object sender, EventArgs e)
        {
            frmInventory inventory = new frmInventory();
            inventory.Show();
        }
    }
}
