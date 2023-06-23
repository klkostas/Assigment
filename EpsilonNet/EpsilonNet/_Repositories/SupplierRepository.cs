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
    public class SupplierRepository : BaseRepository, ISupplierRepository
    {
        public SupplierRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Add(Supplier supplier)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into Suppliers values (@name, @surname,@tin,@address,@phone,@fax)";
                command.Parameters.Add("@name", SqlDbType.VarChar).Value = supplier.Name;
                command.Parameters.Add("@surname", SqlDbType.VarChar).Value = supplier.SurName;
                command.Parameters.Add("@tin", SqlDbType.VarChar).Value = supplier.Tin;
                command.Parameters.Add("@address", SqlDbType.VarChar).Value = supplier.Address;
                command.Parameters.Add("@phone", SqlDbType.VarChar).Value = supplier.Phone;
                command.Parameters.Add("@fax", SqlDbType.VarChar).Value = supplier.Fax;
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
                command.CommandText = "delete from Suppliers where SupplierId=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        public void Edit(Supplier supplier)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"update Suppliers 
                                      set  Name=@name, Surname=@surname,Tin=@tin,
                                      Address=@address,Phone=@phone,Fax=@fax 
                                      where SupplierId=@id";
                command.Parameters.Add("@name", SqlDbType.VarChar).Value = supplier.Name;
                command.Parameters.Add("@surname", SqlDbType.VarChar).Value = supplier.SurName;
                command.Parameters.Add("@tin", SqlDbType.VarChar).Value = supplier.Tin;
                command.Parameters.Add("@address", SqlDbType.VarChar).Value = supplier.Address;
                command.Parameters.Add("@phone", SqlDbType.VarChar).Value = supplier.Phone;
                command.Parameters.Add("@fax", SqlDbType.VarChar).Value = supplier.Fax;
                command.Parameters.Add("@id", SqlDbType.Int).Value = supplier.Supplierid;
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Supplier> GetAll()
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
