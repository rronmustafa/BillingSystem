using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BillingSystem.BLL;

namespace BillingSystem.DAL
{
    class transactionDAL
    {
        //Creating a connection string variable
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Insert Transaction Method

        public bool Insert_Transaction(transacitonBLL t, out int transactionID)
        {
            //Creating boolean var and set it default value to false
            bool isSuccess = false;
            //Set the out transaction value to  negative 1  i.e -1
            transactionID = -1;
            //Creating a sql connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Writing sql query to insert transaction
                string sql = "INSERT INTO table_transaction (type, dea_cust_id, grandTotal, transaction_date, tax, discount, added_by) VALUES (@type, @dea_cust_id, @grandTotal, @transaction_date, @tax, @discount, @added_by); SELECT @@IDENTITY;";


                //Create a sql command to pass the value to query and execute
                SqlCommand cmd = new SqlCommand(sql, conn); 

                //Passing the value to sql query using cmd
                cmd.Parameters.AddWithValue("@type", t.type);
                cmd.Parameters.AddWithValue("@dea_cust_id", t.dea_cust_id);
                cmd.Parameters.AddWithValue("@grandTotal", t.grandTotal);
                cmd.Parameters.AddWithValue("@transaction_date", t.transaction_date);
                cmd.Parameters.AddWithValue("@tax", t.tax);
                cmd.Parameters.AddWithValue("@discount", t.discount);
                cmd.Parameters.AddWithValue("@added_by", t.added_by);

                //open database connection
                conn.Open();
                
                //execute the query
                object o = cmd.ExecuteScalar();

                //if the query is executed successfully the value of o will not be null else it will be null
                if (o != null)
                {
                    //query executed successfully
                    transactionID = int.Parse(o.ToString());
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
        #region mthod to display all the transaction

        public DataTable DisplayAllTransaction()
        {
            //create a sql conn
            SqlConnection conn = new SqlConnection(myconnstrng);

            //create a data table to hold the data from data base temporarily
            DataTable dt = new DataTable();

            try
            {
                //write the sql query to display all transactions
                string sql = "SELECT * FROM table_transaction";

                //sql command to execute query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //sql data adapter to hold the data from data base
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();

                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        #endregion
        #region method to display transaction based on transaction type

        public DataTable DisplayTransactionByType(string type)
        {
            //create a sql conn
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                //write a sql query
                string sql = "SELECT * FROM table_transaction WHERE type='"+type+"'";

                //sql command to execute the query
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                //sql data adapter to hold the data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();

                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        #endregion
    }
}
