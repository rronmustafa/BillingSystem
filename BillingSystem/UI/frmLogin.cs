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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

         loginBLL l = new loginBLL();
         loginDAL dal = new loginDAL();

        public static string loggedIn;
        private void lblUserType_Click(object sender, EventArgs e)
        {
            
        }

        private void pboxClose_Click(object sender, EventArgs e)
        {
            //Code to close the Login From
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            l.username = txtUsername.Text.Trim();
            l.password = txtPassword.Text.Trim();
            l.user_type = cmbUserType.Text.Trim();

            //Checking the login credentials
            bool success = dal.loginCheck(l);
            if (success == true)
            {
                //Login Succesfully
                MessageBox.Show("Login Successfully");
                loggedIn = l.username; 
                //Need to open respective forms based on user type
                switch (l.user_type)
                {
                    case "Admin":
                    {
                        //Display Admin Dashboard
                        Form1 admin = new Form1();
                        admin.Show();
                        this.Hide();
                    }
                    break;
                    case "User":
                    {
                        //Display User Dashboard
                        frmUserDashboard user = new frmUserDashboard();
                        user.Show();
                        this.Hide();
                        
                    } 
                    break;
                    default:
                    {
                        //Display an error message
                        MessageBox.Show("Invalid User Type...");
                    } 
                    break;
                }
            }
            else
            {
                //Login Failed
                MessageBox.Show("Login Failed.. Try Again");
            }
        }
    }
}
