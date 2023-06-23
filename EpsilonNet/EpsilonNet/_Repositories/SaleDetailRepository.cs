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
    public class SaleDetailRepository : BaseRepository, ISaleDetailRepository
    {
        public SaleDetailRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void Add(SaleDetail saleDetail)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into SalesDetail values (@saleID,@itemID,@price,@quantity)";
                command.Parameters.Add("@saleID", SqlDbType.Int).Value = saleDetail.SaleId;
                command.Parameters.Add("@itemID", SqlDbType.Int).Value = saleDetail.ItemId;
                command.Parameters.Add("@price", SqlDbType.Decimal).Value = saleDetail.Price;
                command.Parameters.Add("@quantity", SqlDbType.Int).Value = saleDetail.Quantity;
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
                command.CommandText = "delete from SalesDetail where SaleDetailId=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        public void Edit(SaleDetail saleDetail)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"update SalesDetail 
                                      set  SaleID=@saleID,ItemID=@itemID,Price=@price,Quantity=@quantity
                                      where SaleDetailId=@id";
                command.Parameters.Add("@saleID", SqlDbType.Int).Value = saleDetail.SaleId;
                command.Parameters.Add("@itemID", SqlDbType.Int).Value = saleDetail.ItemId;
                command.Parameters.Add("@price", SqlDbType.Decimal).Value = saleDetail.Price;
                command.Parameters.Add("@quantity", SqlDbType.Int).Value = saleDetail.Quantity;
                command.Parameters.Add("@id", SqlDbType.Int).Value = saleDetail.SaleDetailId;
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<SaleDetail> GetAll()
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

        public IEnumerable<Item> GetAllItems()
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
                        item.Description = reader[1].ToString();
                        item.SupplierId = (int)reader[2];
                        item.DefaultPrice = Convert.ToSingle(reader[3]);
                        item.AlarmPrice = Convert.ToSingle(reader[4]);
                        itemList.Add(item);
                    }
                }
            }
            return itemList;
        }

        public IEnumerable<Sale> GetAllSales()
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
    }
}
