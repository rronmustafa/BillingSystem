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
    public partial class frmCategories : Form
    {
        public frmCategories()
        {
            InitializeComponent();
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCategoryID_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Close();
        }

         categoriesBLL c = new categoriesBLL();
         categoriesDAL dal = new categoriesDAL();
         userDAL udal = new userDAL();
        private void btnADD_Click(object sender, EventArgs e)
        {
            //Get the values from category form
            c.title = txtTitle.Text;
            c.description = txtDescription.Text;
            c.added_date = DateTime.Now;
            
            //getting ID in added by field
            string loggeedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggeedUser);
            //passing the id of logged in user in added by field
            c.added_by = usr.id;

            //Creating bool method to insert data in database
            bool success = dal.Insert(c);

            //if the category is inserted successfully then the value of success will be true else it will be false
            if (success == true)
            {
                //New categories inserted successfully
                MessageBox.Show("New Category Inserted Successfully");
                Clear();
                //Refresh data grid view 
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }
            else
            {
                //Failed to insert new category
                MessageBox.Show("Failed to insert New Category");
            }
        }

        public void Clear()
        {
            txtCategoryID.Text = "";
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtSearch.Text = "";
        }
        private void frmCategories_Load(object sender, EventArgs e)
        {
            //Here write the code to display all the categories in data grid view
            DataTable dt = dal.Select();
            dgvCategories.DataSource = dt;
        }

        private void dgvCategories_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Finding the row index of the row clicked on data grid view
            int rowIndex = e.RowIndex;
            txtCategoryID.Text = dgvCategories.Rows[rowIndex].Cells[0].Value.ToString();
            txtTitle.Text = dgvCategories.Rows[rowIndex].Cells[1].Value.ToString();
            txtDescription.Text = dgvCategories.Rows[rowIndex].Cells[2].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get the value from the category form
            c.id = int.Parse(txtCategoryID.Text);
            c.title = txtTitle.Text;
            c.description = txtDescription.Text;
            c.added_date = DateTime.Now;
            //getting ID in added by field
            string loggeedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggeedUser);
            //passing the id of logged in user in added by field
            c.added_by = usr.id;
            //creating bool var to update category and check
            bool success = dal.Update(c);
            if (success == true)
            {
                //category updated successfully
                MessageBox.Show("Category Updated Successfully");
                Clear();
                //Refresh Data grid view
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }
            else
            {
                //Category Updated Failed
                MessageBox.Show("Category Update Failed");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Get the ID of teh category which  we want to delete
            c.id = int.Parse(txtCategoryID.Text);

            bool success = dal.Delete(c);

            if (success == true)
            {
                //Category Deleted Successfully
                MessageBox.Show("Category Deleted Successfully");
                Clear();
                //Refresh Data grid view
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }
            else
            {
                //Failed to delete category
                MessageBox.Show("Failed to Delete Category");
            }
        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            //Get the keywords
            string keywords = txtSearch.Text;

            //filter the categories based on keywords
            if (keywords != null)
            {
                //Use Search method to display categories
                DataTable dt = dal.Search(keywords);
                dgvCategories.DataSource = dt;
            }
            else
            {
                //Use select method to display all categories
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt; 
            }
        }
    }
}
