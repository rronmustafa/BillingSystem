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
    class categoriesDAL
    {
        //Static string method for database connection string
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Method

        public DataTable Select()
        {
            //creating database connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                //Writing sql query to get all the data from database
                string sql = "SELECT * FROM table_categories";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //open database connection
                conn.Open();
                //Adding the value from adapter to data table dt
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
        #region Insert New Category

        public bool Insert(categoriesBLL c)
        {
            //Creating a bool var and set it default value to false
            bool isSuccess = false;

            //Creating sql Connection to database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Writing query to add new category
                string sql = "INSERT INTO table_categories (title, description, added_date, added_by) VALUES (@title, @description, @added_date, @added_by)";
                //Creating sql command to pass value in our query
                SqlCommand cmd = new SqlCommand(sql,conn);
                //Passing values through parameter
                cmd.Parameters.AddWithValue("@title", c.title);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);

                //Open Database Connection
                conn.Open();

                //creating the int var to execute query
                int rows = cmd.ExecuteNonQuery();

                //if query is execute successfully then its value will be greater than 0 else it will be less than 0
                if(rows > 0)
                {
                    //Query execute successfully
                    isSuccess = true;
                }
                else
                {
                    //Failed to execute  query
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Closing database connection
                conn.Close();
            }
            return isSuccess;
        }


        #endregion
        #region Update Method

        public bool Update(categoriesBLL c)
        {
            //Creating bool var and set it default value to false
            bool isSuccess = false;

            //Creating sql connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //query to update the category
                string sql = "UPDATE table_categories SET title=@title, description=@description, added_date=@added_date, added_by=@added_by WHERE id=@id";
                
                //creating sql command to pass the value on sql query
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                //passing value using cmd
                cmd.Parameters.AddWithValue("@title", c.title);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);
                cmd.Parameters.AddWithValue("@id", c.id);

                //Open data base connection
                conn.Open();
                
                //creating int var to execute query
                int rows = cmd.ExecuteNonQuery();

                //if the query is successfully execute then the value will be greater than 0 else it will be less
                if (rows > 0)
                {
                    //Query execute successfully 
                    isSuccess = true;
                }
                else
                {
                    //Failed to execute query
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Closing connection
                conn.Close();
            }
            return isSuccess;
        }

        #endregion
        #region Delete Category Method

        public bool Delete(categoriesBLL c)
        {
            //creating a bool var and set it to false
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //SQL query to delete from database
                string sql = "DELETE FROM table_categories WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                //passing the value using cmd
                cmd.Parameters.AddWithValue("@id", c.id);

                //Open sql connection
                conn.Open();

                //create an int var 
                int rows = cmd.ExecuteNonQuery();

                //if the query is execute successfully then the value of rows will be greater than 0 else it will be less than 0
                if (rows > 0)
                {
                    //Query execute successfully
                    isSuccess = true;
                }
                else
                {
                    //Failed to Execute Query
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

        #region Method for Search Funtionality

        public DataTable Search(string keywords)
        {
            //sql connection for database connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            //creating datatable to hold the data from database temporarily
            DataTable dt = new DataTable();

            try
            {
                //Sql query to search categories from database
                String sql = "SELECT * FROM table_categories WHERE id LIKE '%"+keywords+"%' OR title LIKE '%"+keywords+"%' OR description LIKE '%"+keywords+"%'";
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
    }
}
