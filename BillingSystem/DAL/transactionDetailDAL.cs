using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BillingSystem.BLL;

namespace BillingSystem.DAL
{
    class transactionDetailDAL
    {
        //Creating static string method for data base connection
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region INSERT method for Transaction Detail

        public bool InsertTransactionDetail(transactionDetailBLL td)
        {
            //creating a bool var and set it value to false
            bool isSuccess = false;

            //creating sql connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Writing sql query to insert transaction detail in database
                string sql = "INSERT INTO table_transaction_detail (product_id, rate, quantity, total, dea_cust_id, added_date, added_by) VALUES (@product_id, @rate, @quantity, @total, @dea_cust_id, @added_date, @added_by)";

                //passing the value to the sql query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //pasing the value using cmd
                cmd.Parameters.AddWithValue("@product_id", td.product_id);
                cmd.Parameters.AddWithValue("@rate", td.rate);
                cmd.Parameters.AddWithValue("@quantity", td.quantity);
                cmd.Parameters.AddWithValue("@total", td.total);
                cmd.Parameters.AddWithValue("@dea_cust_id", td.dea_cust_id);
                cmd.Parameters.AddWithValue("@added_date", td.added_date);
                cmd.Parameters.AddWithValue("@added_by", td.added_by);

                //open database connection
                conn.Open();
                
                //declare the int var and execute query
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    //query executed successfully
                    isSuccess = true;
                }
                else
                {
                    //failed to executed query
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        #endregion          
    }
}
