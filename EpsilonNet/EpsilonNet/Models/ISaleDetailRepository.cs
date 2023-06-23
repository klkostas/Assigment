using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpsilonNet.Models
{
    public interface ISaleDetailRepository
    {
        void Add(SaleDetail saleDetail);
        void Edit(SaleDetail saleDetail);
        void Delete(int id);
        IEnumerable<SaleDetail> GetAll();
        IEnumerable<Sale> GetAllSales();
        IEnumerable<Item> GetAllItems();
    }
}
