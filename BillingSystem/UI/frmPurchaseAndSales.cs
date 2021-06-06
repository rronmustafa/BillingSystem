using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using BillingSystem.BLL;
using BillingSystem.DAL;
using DGVPrinterHelper;

namespace BillingSystem.UI
{
    public partial class frmPurchaseAndSales : Form
    {
        public frmPurchaseAndSales()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //close the from
            this.Hide();
        }

        private DeaCustDAL dcDAL = new DeaCustDAL();
        private productsDAL pDAL = new productsDAL();
        private userDAL uDAL = new userDAL();
        private transactionDAL tDAL = new transactionDAL();
        private transactionDetailDAL tdDAL = new transactionDetailDAL();  
        private DataTable transactionDT = new DataTable();
        private void frmPurchaseAndSales_Load(object sender, EventArgs e)
        {
             //get the transactionType value from userDashboard
             string type = frmUserDashboard.transactionType;
             
             //set the value on lblTop
             lblTop.Text = type;

             //Specify columns fo our transactionDt
             transactionDT.Columns.Add("Product Name");
             transactionDT.Columns.Add("Rate");
             transactionDT.Columns.Add("Quantity");
             transactionDT.Columns.Add("Total");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //get the keyword from textbox
            string keyword = txtSearch.Text;

            if (keyword == "")
            {
                //clear all the textboxes
                txtName.Text = "";
                txtEmail.Text = "";
                txtContact.Text = "";
                txtAddress.Text = "";
                return;
            }

            //write the code to get the details and set the values on textboxes
            DeaCustBLL dc = dcDAL.SearchDealerCustomerForTransaction(keyword);

            //now transfer or set the value from DeaCustBLL to textboxes
            txtName.Text = dc.name;
            txtEmail.Text = dc.email;
            txtContact.Text = dc.contact;
            txtAddress.Text = dc.address;
        }

        private void txtSearchProducts_TextChanged(object sender, EventArgs e)
        {
            //get the keyword from productSearch textboxes
            string keyword = txtSearchProducts.Text;

            //check if we have value on txtSearchProduct or not
            if (keyword == "")
            {
                txtProductName.Text = "";
                txtProductInventory.Text = "";
                txtProductRate.Text = "";
                txtProductQuantity.Text = "";
                return;
            }

            //Search the product and display on respective boxes
            productsBLL p = pDAL.GetProductsForTransaction(keyword);

            txtProductName.Text = p.name;
            txtProductInventory.Text = p.quantity.ToString();
            txtProductRate.Text = p.rate.ToString();   
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //get product name rate and quantity customer wants to buy
            string productName = txtProductName.Text;
            decimal rate = decimal.Parse(txtProductRate.Text);
            decimal quantity = decimal.Parse(txtProductQuantity.Text);

            decimal total = rate * quantity; 

            //display the subtotal in text box
            //Get the total value from text box
            decimal subTotal = decimal.Parse(txtSubTotal.Text);
            subTotal = subTotal + total;
            //check whether the product is selected or not
            if (productName == "")
            {
                //display an error message
                MessageBox.Show("Select the product first.Try Again");
            }
            else
            {
                //add product to data grid view
                transactionDT.Rows.Add(productName,rate,quantity, total);

                //show in data grid view
                dgvAddedProducts.DataSource = transactionDT;
                //display subtotal in textbox
                txtSubTotal.Text = subTotal.ToString();

                //Clear the textboxes
                txtSearchProducts.Text = "";
                txtProductName.Text = "";
                txtProductInventory.Text = "0.00";
                txtProductRate.Text = "0.00";
                txtProductQuantity.Text = "0.00";
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            //get the value from discount text box
            string value = txtDiscount.Text;

            if (value == "")
            {
                //error message
                MessageBox.Show("please add discount first");
            }
            else
            {
                //get the discount in decimal value
                decimal subTotal = decimal.Parse(txtSubTotal.Text);
                decimal discount = decimal.Parse(txtDiscount.Text);

                //calculate the grand total based on discount
                decimal grandTotal = ((100 - discount) / 100) * subTotal;

                //display the grand total in text box
                txtGrandTotal.Text = grandTotal.ToString(); 
            }
        }

        private void txtVat_TextChanged(object sender, EventArgs e)
        {
            //check if grand total has value or not if it has not value then calculate the discount first
            string check = txtGrandTotal.Text;
            if (check == "")
            {
                //display the error message to calculate discount
                MessageBox.Show("calculate the discount and set the Gran Total first.");
            }
            else
            {
                //calculate vat
                //getting vat percent first
                decimal previousGT = decimal.Parse(txtGrandTotal.Text);
                decimal vat = decimal.Parse(txtVat.Text);
                decimal grandTotalWithVat = ((100 + vat) / 100) * previousGT;

                //displaying new grand total with vat
                txtGrandTotal.Text = grandTotalWithVat.ToString();
            }
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            //get the  paid amount and grand total
            decimal grandTotal = decimal.Parse(txtGrandTotal.Text);
            decimal paidAmount = decimal.Parse(txtPaidAmount.Text);

            decimal returnAmount = paidAmount - grandTotal;

            //display the returnAmount as well
            txtReturnAmount.Text = returnAmount.ToString(); 

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //get the values from PurchaseSales form first
            transacitonBLL transaction = new transacitonBLL();

            transaction.type = lblTop.Text; 

            //get the id of dealer or customer here
            //lets get name of the dealer or customer first
            string deaCustName = txtName.Text;
            DeaCustBLL dc = dcDAL.GetDeaCustIDFromName(deaCustName);

            transaction.dea_cust_id = dc.id;
            transaction.grandTotal = Math.Round(decimal.Parse(txtGrandTotal.Text),2);
            transaction.transaction_date = DateTime.Now;
            transaction.tax = decimal.Parse(txtVat.Text);
            transaction.discount = decimal.Parse(txtDiscount.Text);

            //get the user name of logged in user
            string username = frmLogin.loggedIn;
            userBLL u = uDAL.GetIDFromUsername(username);

            transaction.added_by = u.id;
            transaction.transactionDetail = transactionDT;
            
            //lets create a bool var and set its value to false
            bool success = false;

            //actual code to insert transaction and transaction Detail
            using (TransactionScope scope = new TransactionScope())
            {
                int transactionID = -1;
                //create a bool value and insert transaction
                bool w = tDAL.Insert_Transaction(transaction, out transactionID);

                //use for loop to insert transaction detail
                for (int i = 0; i < transactionDT.Rows.Count; i++)
                {
                    //get all the detail of the product
                    transactionDetailBLL transactionDetail = new transactionDetailBLL();
                    //get the product name and convert it to id
                    string ProductName = transactionDT.Rows[i][0].ToString();
                    productsBLL p = pDAL.GetProductIDFromName(ProductName);

                    transactionDetail.product_id = p.id;
                    transactionDetail.rate = decimal.Parse(transactionDT.Rows[i][1].ToString());
                    transactionDetail.quantity = decimal.Parse(transactionDT.Rows[i][2].ToString());
                    transactionDetail.total = Math.Round(decimal.Parse(transactionDT.Rows[i][3].ToString()),2);
                    transactionDetail.dea_cust_id = dc.id;
                    transactionDetail.added_date = DateTime.Now;
                    transactionDetail.added_by = u.id;

                    //increase or decrease product quantity based on purchase or sales
                    string transactionType = lblTop.Text;

                    //lets check whether we are on purchase or sales
                    bool x = false;
                    if (transactionType == "Purchase")
                    {
                        //Increase the product
                        x = pDAL.IncreaseProduct(transactionDetail.product_id, transactionDetail.quantity);
                    }
                    else if(transactionType == "Sales")
                    {
                        //decrease the product
                        x = pDAL.DecreaseProduct(transactionDetail.product_id, transactionDetail.quantity);
                    }

                    //insert transaction detail inside database
                    bool y = tdDAL.InsertTransactionDetail(transactionDetail);
                    success = w && x && y;
                }
               
                if (success == true)
                {
                    //transaction complete 
                    scope.Complete();

                    //Code to print bill
                    DGVPrinter printer = new DGVPrinter();

                    printer.Title = "\r\n\r\n BILLING SYSTEM COMPANY";
                    printer.SubTitle = "Prishtine, Kosovo \r\n Phone: 044735539";
                    printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                    printer.PageNumbers = true;
                    printer.PageNumberInHeader = false;
                    printer.PorportionalColumns = true;
                    printer.HeaderCellAlignment = StringAlignment.Near;
                    printer.Footer = "Discount: " + txtDiscount.Text + "% \r\n" + "VAT: " + txtVat.Text + "% \r\n" + "Grand Total: " + txtGrandTotal.Text + "\r\n" + "Thank You for doing business with us.";
                    printer.FooterSpacing = 15;
                    printer.PrintDataGridView(dgvAddedProducts);

                    MessageBox.Show("Transaction Completed Successfully!!");
                    //clear the data grid view and clear all text boxes
                    dgvAddedProducts.DataSource = null;
                    dgvAddedProducts.Rows.Clear();

                    txtSearch.Text = "";
                    txtName.Text = "";
                    txtEmail.Text = "";
                    txtContact.Text = "";
                    txtAddress.Text = "";
                    txtSearchProducts.Text = "";
                    txtProductName.Text = "";
                    txtProductInventory.Text = "0";
                    txtProductRate.Text = "0";
                    txtProductQuantity.Text = "0";
                    txtSubTotal.Text = "0";
                    txtDiscount.Text = "0";
                    txtVat.Text = "0";
                    txtGrandTotal.Text = "0";
                    txtPaidAmount.Text = "0";
                    txtReturnAmount.Text = "0";
                }
                else
                {
                    //transaction failed
                    MessageBox.Show("Transaction Failed!!!");
                }
            }
        }
    }
}
