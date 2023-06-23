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
    public class PurchaseDetailRepository : BaseRepository, IPurchaseDetailRepository
    {
        //Constructor
        public PurchaseDetailRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Add(PurchaseDetail purchaseDetail)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into PurchasesDetail values (@purchaseID,@itemID,@price,@quantity)";
                command.Parameters.Add("@purchaseID", SqlDbType.Int).Value = purchaseDetail.PurchaseId;
                command.Parameters.Add("@itemID", SqlDbType.Int).Value = purchaseDetail.ItemId;
                command.Parameters.Add("@price", SqlDbType.Decimal).Value = purchaseDetail.Price;
                command.Parameters.Add("@quantity", SqlDbType.Int).Value = purchaseDetail.Quantity;
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
                command.CommandText = "delete from PurchasesDetail where PurchaseDetailId=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        public void Edit(PurchaseDetail purchaseDetail)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"update PurchasesDetail 
                                      set  PurchaseID=@purchaseID,ItemID=@itemID,Price=@price,Quantity=@quantity
                                      where PurchaseDetailId=@id";
                command.Parameters.Add("@purchaseID", SqlDbType.Int).Value = purchaseDetail.PurchaseId;
                command.Parameters.Add("@itemID", SqlDbType.Int).Value = purchaseDetail.ItemId;
                command.Parameters.Add("@price", SqlDbType.Decimal).Value = purchaseDetail.Price;
                command.Parameters.Add("@quantity", SqlDbType.Int).Value = purchaseDetail.Quantity;
                command.Parameters.Add("@id", SqlDbType.Int).Value = purchaseDetail.PurchaseDetailId;
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<PurchaseDetail> GetAll()
        {
            var purchaseDetailList = new List<PurchaseDetail>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from PurchasesDetail order by PurchaseDetailId";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var purchaseDetail = new PurchaseDetail();
                        purchaseDetail.PurchaseDetailId = (int)reader[0];
                        purchaseDetail.PurchaseId = (int)reader[1];
                        purchaseDetail.ItemId = (int)reader[2];
                        purchaseDetail.Price = Convert.ToSingle(reader[3]);
                        purchaseDetail.Quantity = (int)reader[4];
                        purchaseDetailList.Add(purchaseDetail);
                    }
                }
            }
            return purchaseDetailList;
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
    

        public IEnumerable<Purchase> GetAllPurchases()
        {
            var purchaseList = new List<Purchase>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from Purchases order by PurchaseId";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var purchase = new Purchase();
                        purchase.PurchaseId = (int)reader[0];
                        purchase.DatePurchase = Convert.ToDateTime(reader[1]);
                        purchase.SupplierID = (int)reader[2];
                        purchase.Justification = reader[3].ToString();
                        purchaseList.Add(purchase);
                    }
                }
            }
            return purchaseList;
        }
    }
}

