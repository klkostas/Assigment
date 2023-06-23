using EpsilonNet.Models;
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
    public class SaleRepository : BaseRepository, ISaleRepository
    {
        public SaleRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Add(Sale sale)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into Sales values (@date,@customerID,@justification)";
                command.Parameters.Add("@date", SqlDbType.Date).Value = sale.SaleDate;
                command.Parameters.Add("@customerID", SqlDbType.Int).Value = sale.CustomerID;
                command.Parameters.Add("@justification", SqlDbType.VarChar).Value = sale.Justification;
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
                command.CommandText = "delete from Sales where SaleId=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        public void Edit(Sale sale)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"update Sales 
                                      set  DateSale=@date, CustomerID=@customerID,Justification=@justification
                                      where SaleId=@id";
                command.Parameters.Add("@date", SqlDbType.Date).Value = sale.SaleDate;
                command.Parameters.Add("@customerID", SqlDbType.Int).Value = sale.CustomerID;
                command.Parameters.Add("@justification", SqlDbType.VarChar).Value = sale.Justification;
                command.Parameters.Add("@id", SqlDbType.Int).Value = sale.SaleId;
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Sale> GetAll()
        {
            var saleList = new List<Sale>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from Sales order by SaleId";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var sale = new Sale();
                        sale.SaleId = (int)reader[0];
                        sale.SaleDate = Convert.ToDateTime(reader[1]);
                        sale.CustomerID = (int)reader[2];
                        sale.Justification = reader[3].ToString();
                        saleList.Add(sale);
                    }
                }
            }
            return saleList;
        }

        public IEnumerable<Customer> GetAllCustomers()
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
                    while (reader.Read())
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

        public IEnumerable<SaleDetail> GetSalesDetails()
        {
            var saleDetailList = new List<SaleDetail>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from SalesDetail order by SaleDetailId";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var saleDetail = new SaleDetail();
                        saleDetail.SaleDetailId = (int)reader[0];
                        saleDetail.SaleId = (int)reader[1];
                        saleDetail.ItemId = (int)reader[2];
                        saleDetail.Price = Convert.ToSingle(reader[3]);
                        saleDetail.Quantity = (int)reader[4];
                        saleDetailList.Add(saleDetail);
                    }
                }
            }
            return saleDetailList;
        }
    }
}
