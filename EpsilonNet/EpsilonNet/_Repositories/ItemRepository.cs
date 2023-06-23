using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using EpsilonNet.Models;

namespace EpsilonNet._Repositories
{
    public class ItemRepository: BaseRepository, IItemRepository
    {
        //Constructor
        public ItemRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        //Methods
        public void Add(Item item)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into Items values (@description, @supplierID,@defaultPrice,@alarmPrice)";
                command.Parameters.Add("@description", SqlDbType.VarChar).Value = item.Description;
                command.Parameters.Add("@supplierID", SqlDbType.Int).Value = item.SupplierId;
                command.Parameters.Add("@defaultPrice", SqlDbType.Decimal).Value = item.DefaultPrice;
                command.Parameters.Add("@alarmPrice", SqlDbType.Decimal).Value = item.AlarmPrice;
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
                command.CommandText = "delete from Items where ItemId=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        public void Edit(Item item)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"update Items 
                                      set  Description=@description, SupplierID=@supplierID,DefaultPrice=@defaultPrice,AlarmPrice=@alarmPrice
                                      where ItemId=@id";
                command.Parameters.Add("@description", SqlDbType.VarChar).Value = item.Description;
                command.Parameters.Add("@supplierID", SqlDbType.Int).Value = item.SupplierId;
                command.Parameters.Add("@defaultPrice", SqlDbType.Decimal).Value = item.DefaultPrice;
                command.Parameters.Add("@alarmPrice", SqlDbType.Decimal).Value = item.AlarmPrice;
                command.Parameters.Add("@id", SqlDbType.Int).Value = item.Itemid;
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Item> GetAll()
        {
            var itemList = new List<Item>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from Items order by ItemId";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new Item();
                        item.Itemid = (int)reader[0];
                        item.Description= reader[1].ToString();
                        item.SupplierId = (int)reader[2];
                        item.DefaultPrice = Convert.ToSingle(reader[3]);
                        item.AlarmPrice = Convert.ToSingle(reader[4]);
                        itemList.Add(item);
                    }
                }
            }
            return itemList;
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            var supplierList = new List<Supplier>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from Suppliers order by SupplierId";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var supplier = new Supplier();
                        supplier.Supplierid = (int)reader[0];
                        supplier.Name = reader[1].ToString();
                        supplier.SurName = reader[2].ToString();
                        supplier.Tin = reader[3].ToString();
                        supplier.Address = reader[4].ToString();
                        supplier.Phone = reader[5].ToString();
                        supplier.Fax = reader[6].ToString();
                        supplierList.Add(supplier);
                    }
                }
            }
            return supplierList;
        }
    }
}

