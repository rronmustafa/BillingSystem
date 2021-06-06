using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BillingSystem.DAL;

namespace BillingSystem.UI
{
    public partial class frmTransaction : Form
    {
        public frmTransaction()
        {
            InitializeComponent();
        }

        private transactionDAL tDAL = new transactionDAL();
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //closing the form
            this.Hide();
        }

        private void frmTransaction_Load(object sender, EventArgs e)
        {
            //display all the transaction
            DataTable dt = tDAL.DisplayAllTransaction();
            dgvTransactions.DataSource = dt;


        }

        private void cmbTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get the value from combo box
            string type = cmbTransactionType.Text;

            DataTable dt = tDAL.DisplayTransactionByType(type);
            dgvTransactions.DataSource = dt;
        }

        private void btnALL_Click(object sender, EventArgs e)
        { 
            //display all the transaction
            DataTable dt = tDAL.DisplayAllTransaction();
            dgvTransactions.DataSource = dt;
        }
    }
}
