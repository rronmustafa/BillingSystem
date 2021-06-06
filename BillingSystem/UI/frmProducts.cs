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
    public partial class frmProducts : Form
    {
        public frmProducts()
        {
            InitializeComponent();
        }

         categoriesDAL cdal = new categoriesDAL();
         productsBLL p = new productsBLL();
         productsDAL pdal = new productsDAL();
         userDAL udal = new userDAL();

        private void frmProducts_Load(object sender, EventArgs e)
        {
            //creating a data table to hold the categories from database
            DataTable categoriesDT = cdal.Select();
            //Specify datasource for category combobox
            cmbCategory.DataSource = categoriesDT;
            //specify display member and value member for combobox
            cmbCategory.DisplayMember = "title";
            cmbCategory.ValueMember = "title";
             
            //Load all the products in data grid view
            DataTable dt = pdal.Select();
            dgvProducts.DataSource = dt;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //get the keyword from form
            string keywords = txtSearch.Text;

            if (keywords != null)
            {
                //Search the products
                DataTable dt = pdal.Search(keywords);
                dgvProducts.DataSource = dt;
            }
            else
            {
                //display all the products
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //Add code to hide this form
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get all values from Product form
            p.name = txtName.Text;
            p.category = cmbCategory.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.quantity = 0; 
            p.added_date = DateTime.Now;
            //Get the username of logged in user
            String loggedUsr = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUsr);

            p.added_by = usr.id;

            //Create bool var to check if the product is added successfully or not
            bool success = pdal.Insert(p);
            //If the product is added successfully  then the value of success  will be true else it will be false
            if (success == true)
            {
                //Product Inserted Successfully
                MessageBox.Show("Product Added Successfully");
                //calling the clear method
                Clear();
                //Refreshing data grid view
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Failed to add new product
                MessageBox.Show("Failed to Add New Product"); 
            }

        }

        public void Clear()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtDescription.Text = "";
            cmbCategory.Text = "";
            txtRate.Text = "";
            txtSearch.Text = "";
        }

        private void dgvProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Creating a int var to know which product was clicked
            int rowIndex = e.RowIndex;
            //Display the value on Respective Textboxes
            txtID.Text = dgvProducts.Rows[rowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvProducts.Rows[rowIndex].Cells[1].Value.ToString();
            cmbCategory.Text = dgvProducts.Rows[rowIndex].Cells[2].Value.ToString();
            txtDescription.Text = dgvProducts.Rows[rowIndex].Cells[3].Value.ToString();
            txtRate.Text = dgvProducts.Rows[rowIndex].Cells[4].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get the values from UI or product form
            p.id = int.Parse(txtID.Text);
            p.name = txtName.Text;
            p.category = cmbCategory.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.added_date = DateTime.Now;
            //Get the username of logged in user for added_by
            String loggedUsr = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUsr);

            p.added_by = usr.id;

            //create a bool var to check if the product is updated or not
            bool success = pdal.Update(p);
            //if the product is updated successfully then the value of success will be true else it will be false
            if (success == true)
            {
                //Updated Successfully
                MessageBox.Show("Product Updated Successfully");
                Clear();
                //Refreshing the data grid view
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Failed to Update
                MessageBox.Show("Failed to Update Product");
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Get the Id of the product to be deleted
            p.id = int.Parse(txtID.Text);

            //create a bool var to check if the product is updated or not
            bool success = pdal.Delete(p);
            //If the product is deleted successfully then the value of success will be true else it will be false
            if (success == true)
            {
                //Deleted Successfully
                MessageBox.Show("Product Successfully Deleted");
                Clear();
                //Refreshing the data grid view
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Failed to Delete
                MessageBox.Show("Failed to Delete Product");
            }
        }
    }
}
