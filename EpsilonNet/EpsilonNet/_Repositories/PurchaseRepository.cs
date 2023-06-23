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
    public class PurchaseRepository: BaseRepository,IPurchaseRepository
    {
        //Constructor
        public PurchaseRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Add(Purchase purchase)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into Purchases values (@date,@supplierID,@justification)";
                command.Parameters.Add("@date", SqlDbType.Date).Value = purchase.DatePurchase;
                command.Parameters.Add("@supplierID", SqlDbType.Int).Value = purchase.SupplierID;
                command.Parameters.Add("@justification", SqlDbType.VarChar).Value = purchase.Justification;
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
                command.CommandText = "delete from Purchases where PurchaseId=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        public void Edit(Purchase purchase)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"update Purchases 
                                      set  DatePurchase=@date, SupplierID=@supplierID,Justification=@justification
                                      where PurchaseId=@id";
                command.Parameters.Add("@date", SqlDbType.Date).Value = purchase.DatePurchase;
                command.Parameters.Add("@supplierID", SqlDbType.Int).Value = purchase.SupplierID;
                command.Parameters.Add("@justification", SqlDbType.VarChar).Value = purchase.Justification;
                command.Parameters.Add("@id", SqlDbType.Int).Value = purchase.PurchaseId;
                command.ExecuteNonQuery();
            }

        }

        public IEnumerable<Purchase> GetAll()
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

        public IEnumerable<PurchaseDetail> GetPurchasesDetails()
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
    }
}
