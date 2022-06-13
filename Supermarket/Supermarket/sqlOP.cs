using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Supermarket
{
    internal class sqlOP
    {
        public SqlConnection conn;
        
        public sqlOP() // constructor that will make a sqlconnection to a specified database
        {
            conn = new SqlConnection("Data Source=a7med;Initial Catalog=supermarket;Integrated Security=True;Connect Timeout = 30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False");
        }

        public void open() //start connection
        {
            conn.Open();
        }
        public void close() // terminate connection
        {
            conn.Close();
        }

        public void insert(string name ,int quantity  , int price  , int catId , int sellerId)  // insertion
        {
            SqlCommand cmd = new SqlCommand("insert into [production].[products_view] values (@name , @quantity , @price , @category , @seller) ", conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@category", catId);
            cmd.Parameters.AddWithValue("@seller", sellerId);
            cmd.ExecuteNonQuery();
        }

        public void update(int id , string name, int quantity, int price, int catId , int sellerId) // update
        {
            SqlCommand cmd = new SqlCommand("update [production].[products_view] set productName=@name , prodQuantity = @quantity , prodPrice=@price,categoryId=@category , sellerId=@seller  where productId =@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@category", catId);
            cmd.Parameters.AddWithValue("@seller", sellerId);
            cmd.ExecuteNonQuery();
        }

        public void delete(int id ) // delete
        { 
          SqlCommand cmd = new SqlCommand("delete from [production].[products_view] where productId="+id , conn);
            cmd.ExecuteNonQuery();
        }


        public void categOperation(string sp, int id ,string name , string desc) // method to handle crud on categories table
        {
            if (id == -1)
            {
                SqlCommand cmd = new SqlCommand(sp, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@desc", desc);
                cmd.ExecuteNonQuery();
            }
            else
            {
                SqlCommand cmd = new SqlCommand(sp, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@desc", desc);
                cmd.ExecuteNonQuery();
            }
        }

        public void sellersOP(string sp, int id, string name, int age,string phone , string address , string password) // method to handle crud on sellers table using stored procedures
        {
            if (id == -1)
            {
                SqlCommand cmd = new SqlCommand(sp, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.ExecuteNonQuery();
            }
            else
            {
                SqlCommand cmd = new SqlCommand(sp, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.ExecuteNonQuery();
            }
        }





    }
}
