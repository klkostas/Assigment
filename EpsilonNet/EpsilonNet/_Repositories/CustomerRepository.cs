using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using EpsilonNet.Models;
using EpsilonNet.Presenters.Common;

namespace EpsilonNet._Repositories
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        //Constructor
        public CustomerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        //Methods
        public void Add(Customer customer)
        {
            using (var connection = new SqlConnection(connectionString))
            //using (var connection = SqlDatabaseConnection.Instance)
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into Customers values (@name, @surname,@tin,@address,@phone,@fax)";
                command.Parameters.Add("@name", SqlDbType.VarChar).Value = customer.Name;
                command.Parameters.Add("@surname", SqlDbType.VarChar).Value = customer.SurName;
                command.Parameters.Add("@tin", SqlDbType.VarChar).Value = customer.Tin;
                command.Parameters.Add("@address", SqlDbType.VarChar).Value = customer.Address;
                command.Parameters.Add("@phone", SqlDbType.VarChar).Value = customer.Phone;
                command.Parameters.Add("@fax", SqlDbType.VarChar).Value = customer.Fax;
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "delete from Customers where CustomerId=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        public void Edit(Customer customer)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"update Customers 
                                      set  Name=@name, Surname=@surname,Tin=@tin,
                                      Address=@address,Phone=@phone,Fax=@fax 
                                      where CustomerId=@id";
                command.Parameters.Add("@name", SqlDbType.VarChar).Value = customer.Name;
                command.Parameters.Add("@surname", SqlDbType.VarChar).Value = customer.SurName;
                command.Parameters.Add("@tin", SqlDbType.VarChar).Value = customer.Tin;
                command.Parameters.Add("@address", SqlDbType.VarChar).Value = customer.Address;
                command.Parameters.Add("@phone", SqlDbType.VarChar).Value = customer.Phone;
                command.Parameters.Add("@fax", SqlDbType.VarChar).Value = customer.Fax;
                command.Parameters.Add("@id", SqlDbType.Int).Value = customer.Customerid;
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Customer> GetAll()
        {
            var customerList = new List<Customer>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from Customers order by CustomerId";

                using (var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        var customer = new Customer();
                        customer.Customerid = (int)reader[0];
                        customer.Name = reader[1].ToString();
                        customer.SurName = reader[2].ToString();
                        customer.Tin = reader[3].ToString();
                        customer.Address = reader[4].ToString();
                        customer.Phone = reader[5].ToString();
                        customer.Fax = reader[6].ToString();
                        customerList.Add(customer);
                    }
                }
            }
            return customerList;
        }
    }
}
