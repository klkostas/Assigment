using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpsilonNet.Models
{
    public interface ISaleRepository
    {
        void Add(Sale sale);
        void Edit(Sale sale);
        void Delete(int id);
        IEnumerable<Sale> GetAll();
        IEnumerable<Customer> GetAllCustomers();
        IEnumerable<SaleDetail> GetSalesDetails();
    }
}
