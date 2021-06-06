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
using DateTime = System.DateTime;

namespace BillingSystem.UI
{
    public partial class frmDealCust : Form
    {
        public frmDealCust()
        {
            InitializeComponent();
        }

        private void lblContact_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //Write the code to close this form
            this.Hide();
        }

        DeaCustBLL dc = new DeaCustBLL();
        DeaCustDAL dcDal = new DeaCustDAL();

        userDAL uDal = new userDAL();
        private void btnADD_Click(object sender, EventArgs e)
        {
            //get the values from form 
            dc.type = cmbDeaCust.Text;
            dc.name = txtName.Text;
            dc.email = txtEmail.Text;
            dc.contact = txtContact.Text;
            dc.address = txtAddress.Text;
            dc.added_date = DateTime.Now;
            //getting the id of logged in user and passing its value in dealer or customer module
            string loggedUsr = frmLogin.loggedIn;
            userBLL usr = uDal.GetIDFromUsername(loggedUsr);

            dc.added_by = usr.id;

            //creating a bool var to check if the dealer or customer is added or not
            bool success = dcDal.Insert(dc);

            if (success == true)
            {
                //Dealer or customer inserted successfully
                MessageBox.Show("Dealer or Customer added Successfully");
                Clear();
                //refreshing data grid view
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                //failed to insert a dealer or customer
            }
        }

        public void Clear()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            txtSearch.Text = "";

        }

        private void frmDealCust_Load(object sender, EventArgs e)
        {
            //refreshing data grid view
            DataTable dt = dcDal.Select();
            dgvDeaCust.DataSource = dt;

        }

        private void dgvDeaCust_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //int var to get id of row clicked
            int rowIndex = e.RowIndex;

            txtID.Text = dgvDeaCust.Rows[rowIndex].Cells[0].Value.ToString();
            cmbDeaCust.Text = dgvDeaCust.Rows[rowIndex].Cells[1].Value.ToString();
            txtName.Text = dgvDeaCust.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvDeaCust.Rows[rowIndex].Cells[3].Value.ToString();
            txtContact.Text = dgvDeaCust.Rows[rowIndex].Cells[4].Value.ToString();
            txtAddress.Text = dgvDeaCust.Rows[rowIndex].Cells[5].Value.ToString();
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //get the values from form
            dc.id = int.Parse(txtID.Text);
            dc.type = cmbDeaCust.Text;
            dc.name = txtName.Text;
            dc.email = txtEmail.Text;
            dc.contact = txtContact.Text;
            dc.address = txtAddress.Text;
            dc.added_date = DateTime.Now;
            //getting the id of logged in user and passing its value in dealer or customer module
            string loggedUsr = frmLogin.loggedIn;
            userBLL usr = uDal.GetIDFromUsername(loggedUsr);
            dc.added_by = usr.id;

            //create a bool var to check if the product is updated or not
            bool success = dcDal.Update(dc);
            //if the product is updated successfully then the value of success will be true else it will be false
            if (success == true)
            {
                //Updated Successfully
                MessageBox.Show("Delaer and Customer Updated Successfully");
                Clear();
                //Refreshing the data grid view
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                //Failed to Update
                MessageBox.Show("Failed to Update Dealer and Customer");
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //get the id from user to be deleted
            dc.id = int.Parse(txtID.Text);

            //create a bool var to check if the data is deleted or not
            bool success = dcDal.Delete(dc);

            if (success == true)
            {
                //Dealer or customer deleted successfully
                MessageBox.Show("Dealer or Customer Deleted Successfully");
                Clear();
                //refreshing data grid view
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                //Failed to delete dealer or customer
                MessageBox.Show("Failed to delete dealer or customer");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //get the keyword from text box
            string keywords = txtSearch.Text;

            if (keywords != null)
            {
                //search the dealer or customer
                DataTable dt = dcDal.Search(keywords);
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                //show all  the dealer or customer
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;
            }
        }
    }
}
