using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BillingSystem.BLL;
using BillingSystem.DAL;

namespace BillingSystem.UI
{
    public partial class frmInventory : Form
    {
        public frmInventory()
        {
            InitializeComponent();
        }

        private categoriesDAL cDAL = new categoriesDAL();
        private productsDAL pDAL = new productsDAL();
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //closing the form
            this.Hide();
        }

        private void frmInventory_Load(object sender, EventArgs e)
        {
            //displaying the categories on combo box
            DataTable cDt = cDAL.Select();

            cmbCategories.DataSource = cDt;

            //give the value member and display member for combo box
            cmbCategories.DisplayMember = "title";
            cmbCategories.ValueMember = "title";

            //display all the products in data grid view when the from is loaded
            DataTable pDt = pDAL.Select();
            dgvInvetory.DataSource = pDt;
        }

        private void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Display all the products  based on Selected category
            string category = cmbCategories.Text;
            DataTable dt = pDAL.DisplayProductsByCategory(category);
            dgvInvetory.DataSource = dt;
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            //Display all the products
            DataTable dt = pDAL.Select();
            dgvInvetory.DataSource = dt;
        }
    }
}
