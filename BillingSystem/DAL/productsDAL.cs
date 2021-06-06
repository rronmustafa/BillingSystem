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
    class productsDAL
    {
        //Creating static string method for Database connection
        private static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select method for product module

        public DataTable Select()
        {
            //Creating Sql Connection to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);

            //DataTable to hold the data from data base
            DataTable dt = new DataTable();

            try
            {
                //Writing the query to select all the product from database
                String sql = "SELECT * FROM table_products";

                //Creating sql command to execute query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Sql Data Adapter to hold the value from database temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database connection
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
        #region method to insert porduct in database

        public bool Insert(productsBLL p)
        {
            //Creating boolean var and set it default value to false
            bool isSuccess = false;

            //Sql conn for database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Sql Query to insert products into database
                String sql = "INSERT INTO table_products (name, category, description, rate, quantity, added_date, added_by) VALUES (@name, @category, @description, @rate, @quantity, @added_date, @added_by)";

                //Creating sql command to pass the values
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing the values through parameters
                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@rate", p.rate);
                cmd.Parameters.AddWithValue("@quantity", p.quantity);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);

                //Opening the database connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //if the query is executed successfully then the value of rows will be greater than 0 else it will be less than 0
                if (rows > 0)
                {
                    //query executed successfully
                    isSuccess = true;
                }
                else
                {
                    //Failed to execute
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
        #region method to update  product in database

        public bool Update(productsBLL p)
        {
            //creating a bool var and set it to false 
            bool isSuccess = false;

            //Creating sql connection for database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                 //sql query to update data in database
                 String sql = "UPDATE table_products SET name=@name, category=@category, description=@description, rate=@rate, added_date=@added_date, added_by=@added_by WHERE id=@id";
                 
                 //creating cql command to pass the value to query
                 SqlCommand cmd = new SqlCommand(sql, conn);

                 //passing the values using parameter and cmd
                 cmd.Parameters.AddWithValue("@name", p.name);
                 cmd.Parameters.AddWithValue("@category", p.category);
                 cmd.Parameters.AddWithValue("@description", p.description);
                 cmd.Parameters.AddWithValue("@rate", p.rate);
                 cmd.Parameters.AddWithValue("@quantity", p.quantity);
                 cmd.Parameters.AddWithValue("@added_date", p.added_date);
                 cmd.Parameters.AddWithValue("@added_by", p.added_by);
                 cmd.Parameters.AddWithValue("@id", p.id);

                 //Open database connection
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
        #region method to delete product from database

        public bool Delete(productsBLL p)
        {
            //creating bool var and set it to false
            bool isSuccess = false;

            //creating sql connection for database connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Query to delete product from database
                String sql = "DELETE FROM table_products WHERE id=@id";

                //creating sql Command to pass the value
                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing the value using cmd
                cmd.Parameters.AddWithValue("@id", p.id);

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
        #region Search method for product module

        public DataTable Search(string keywords)
        {
            //sql connection for database connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            //creating datatable to hold the data from database temporarily
            DataTable dt = new DataTable();

            try
            {
                //Sql query to search categories from database
                String sql = "SELECT * FROM table_products WHERE id LIKE '%" + keywords + "%' OR name LIKE '%" + keywords + "%' OR category LIKE '%" + keywords + "%' OR description LIKE '%" + keywords + "%'";
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
        #region Method to search products in transaction module

        public productsBLL GetProductsForTransaction(string keyword)
        {
            //create an object of productsBLL and return it
            productsBLL p = new productsBLL();
            
            //sql connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            //data table to store data temporarily
            DataTable dt = new DataTable();

            try
            {
                //write the query to get the details
                string sql = "SELECT name, rate, quantity FROM table_products WHERE id LIKE '%"+keyword+"%' OR name LIKE '%"+keyword+"%'";

                //Create sql data adapter to executed the query
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                //open data base connection
                conn.Open();
                
                //passing the value from adapter to dt
                adapter.Fill(dt);

                //if we have any values on dt then set the values to productsBLL
                if (dt.Rows.Count > 0)
                {
                    p.name = dt.Rows[0]["name"].ToString();
                    p.rate = decimal.Parse(dt.Rows[0]["rate"].ToString());
                    p.quantity = decimal.Parse(dt.Rows[0]["quantity"].ToString());
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
            
            return p;
        }

        #endregion
        #region method to get product id based on product name

        public productsBLL GetProductIDFromName(string ProductName)
        {
            //first create an object of deaCustBll and return it
            productsBLL p = new productsBLL();

            //creating sql connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            //data table to hold the data temporarily
            DataTable dt = new DataTable();

            try
            {
                //Writing the query to get the id name
                string sql = "SELECT id FROM table_products WHERE name='" + ProductName + "'";

                //creating sql data adapter to execute the query
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                conn.Open();

                //passing the value from adapter to data table
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    //pass the value from dt to DeaCustBLL dc
                    p.id = int.Parse(dt.Rows[0]["id"].ToString());
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

            return p;
        }


        #endregion
        #region method to get current quantity from database based on product id

        public decimal GetProductQuantity(int ProductID)
        {
            //creating a sql connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            //create a decimal var and set it default value to 0
            decimal quantity = 0;
            
            //create a data table to save the data from database temporarily
            DataTable dt = new DataTable();

            try
            {
                //writing sql query to get quantity from database
                string sql = "SELECT quantity FROM table_products WHERE id = " + ProductID;

                //create a sql command 
                SqlCommand cmd = new SqlCommand(sql, conn);

                //create a sql data adapter to execute the query
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();

                //passing the value from adapter to data table dt
                adapter.Fill(dt);

                //lets check if the data table has value or not
                if (dt.Rows.Count > 0)
                {
                    quantity = decimal.Parse(dt.Rows[0]["quantity"].ToString());
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

            return quantity;
        }

        #endregion
        #region Method to update quantity

        public bool UpdateQuantity(int ProductID, decimal quantity)
        {
            //create a bool var and return it
            bool isSuccess = false;

            //creating sql connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //write the sql query to update quantity
                string sql = "UPDATE table_products SET quantity=@quantity WHERE id=@id";

                //creating sql command to pass the value into query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing the value through parameters
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@id", ProductID);

                //open data base connection
                conn.Open();

                //create  int var and check whether the query is executed successfully or not
                int rows = cmd.ExecuteNonQuery();
                //if the query is executed successfully or not
                if (rows > 0)
                {
                    //query executed successfully
                    isSuccess = true;
                }
                else
                {
                    //failed to execute query
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
        #region Method to increase products

        public bool IncreaseProduct(int ProductID, decimal IncreaseQuantity)
        {
            //create a bool var and set its value to false
            bool success = false;

            //create a sql conn
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //get the current quantity from data base  based on id
                decimal currentQuantity = GetProductQuantity(ProductID);

                //increase the current quantity by the quantity purchase form dealer
                decimal newQuantity = currentQuantity + IncreaseQuantity;

                //update the product quantity now
                success = UpdateQuantity(ProductID, newQuantity);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return success;
        }

        #endregion  
        #region Method to decrease product

        public bool DecreaseProduct(int ProductID, decimal quantity)
        {
            //creating bool var and return it
            bool success = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //get the current product quantity
                decimal currentQuantity = GetProductQuantity(ProductID);

                //decrease the product quantity based on product sales
                decimal newQuantity = currentQuantity - quantity;

                //update the product in database
                success = UpdateQuantity(ProductID, newQuantity);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return success;
        }

        #endregion

        #region Display products based on categories

        public DataTable DisplayProductsByCategory(string category)
        {
            //creating a sql conn
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                //sql query to display products based on category
                string sql = "SELECT * FROM table_products WHERE category='"+category+"'";

                //create a sql command to execute the query
                SqlCommand cmd = new SqlCommand(sql, conn);

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
