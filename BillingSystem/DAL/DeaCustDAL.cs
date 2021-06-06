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
    class DeaCustDAL
    {
        //Creating static string method for data base connection
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select method fo dealer and customer

        public DataTable Select()
        {
            //creating sql connection to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);

            //data table to hold data from database and return it
            DataTable dt = new DataTable();

            try
            {
                //Writing sql query to select all the data from database
                String sql = "SELECT * FROM table_dealer_customer";

                //creating sql command to execute query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //creating sql adapter to store the value from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //open data base connection
                conn.Open();

                //passing all the value from sql data adapter to data table
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
        #region Insert method to add details for dealer or Customer

        public bool Insert(DeaCustBLL dc)
        {
            //Creating boolean var and set it default value to false
            bool isSuccess = false;

            //Creating sql connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Writing sql query to insert the details of dealer or customer
                string sql = "INSERT INTO table_dealer_customer (type, name, email, contact, address, added_date, added_by) VALUES (@type, @name, @email, @contact, @address, @added_date, @added_by)";
                
                //Create a sql command to pass the value to query and execute
                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing the value using parameters
                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);

                //Open data base conn
                conn.Open();
                
                //create int var to check if the query is executed successfully
                int rows = cmd.ExecuteNonQuery();

                //if the query is executed successfully the value of rows will be greater than 0 else it will be less than 0
                if (rows > 0)
                {
                    //Query executed successfully
                    isSuccess = true;
                }
                else
                {
                    //Failed to executed query
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
        #region Update method fo dealer and customer module

        public bool Update(DeaCustBLL dc)
        {
            //creating a bool var and set it value to false
            bool isSuccess = false;

            //creating sql connection for database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Writing the sql query to update data in database
                String sql = "UPDATE table_dealer_customer SET type=@type, name=@name, email=@email, contact=@contact, address=@address, added_date=@added_date, added_by=@added_by WHERE id=@id";

                //creating sql command to pass the value in sql
                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing the value through parameter
                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);
                cmd.Parameters.AddWithValue("@id", dc.id);

                //open the data base conn
                conn.Open();

                //creating int var to check if the query is executed successfully or not
                int rows = cmd.ExecuteNonQuery();

                //if the query is executed successfully the value of rows will be greater than 0 else it will be less than 0
                if (rows > 0)
                {
                    //Query is executed successfully
                    isSuccess = true;
                }
                else
                {
                    //Failed to executed query
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
        #region Delete method for dealer and customer

        public bool Delete(DeaCustBLL dc)
        {
            //creating bool var and set its value to false
            bool isSuccess = false;

            //creating sql connection for database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Query to delete product from database
                String sql = "DELETE FROM table_dealer_customer WHERE id=@id";

                //creating sql Command to pass the value
                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing the value using cmd
                cmd.Parameters.AddWithValue("@id", dc.id);

                //Open the database connection
                conn.Open();
                
                //creating int var to check if the query is executed
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    //Query executed successfully
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
        #region Search method for dealer and customer module

        public DataTable Search(string keywords)
        {
            //sql connection for database connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            //creating datatable to hold the data from database temporarily
            DataTable dt = new DataTable();

            try
            {
                //Sql query to search categories from database
                String sql = "SELECT * FROM table_dealer_customer WHERE id LIKE '%" + keywords + "%' OR name LIKE '%" + keywords + "%' OR type LIKE '%" + keywords + "%'";
                //Creating sql command to execute the query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database Connection
                conn.Open();

                //Passing values from adapter to data table dt
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
        #region Method to search dealer or customer for transaction module

        public DeaCustBLL SearchDealerCustomerForTransaction(string keyword)
        {
            //create an object DeaCustBLL class
            DeaCustBLL dc = new DeaCustBLL();

            //creating a database connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            //create a data table to hold the value temporarily
            DataTable dt = new DataTable();

            try
            {
                //writing the sql query to search  dealer or customer based on keyword
                string sql = "SELECT name, email, contact, address from table_dealer_customer WHERE id LIKE '%"+keyword+"%' OR name LIKE '%"+keyword+"%'";

                //create a sql data adapter to execute the query
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                //open the data base connection
                conn.Open();
                
                //transfer the data from sql data adapter to data table
                adapter.Fill(dt);

                //if we have values on dt we need to save it in dealerCustomerBLL
                if (dt.Rows.Count > 0)
                {
                    dc.name = dt.Rows[0]["name"].ToString();
                    dc.email = dt.Rows[0]["email"].ToString();
                    dc.contact = dt.Rows[0]["contact"].ToString();
                    dc.address = dt.Rows[0]["address"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //close database connection
                conn.Close();
            }

            return dc;
        }

        #endregion

        #region Method to get id of the dealer or customer based on name

        public DeaCustBLL GetDeaCustIDFromName(string name)
        {
            //first create an object of deaCustBll and return it
            DeaCustBLL dc = new DeaCustBLL();

            //creating sql connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            //data table to hold the data temporarily
            DataTable dt = new DataTable();

            try
            {
                //Writing the query to get the id name
                string sql = "SELECT id FROM table_dealer_customer WHERE name='"+name+"'";

                //creating sql data adapter to execute the query
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                
                conn.Open();
                
                //passing the value from adapter to data table
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    //pass the value from dt to DeaCustBLL dc
                    dc.id = int.Parse(dt.Rows[0]["id"].ToString());
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

            return dc;
        }

        #endregion
    }
}
