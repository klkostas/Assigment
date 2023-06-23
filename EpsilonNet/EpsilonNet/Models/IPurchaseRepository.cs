using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpsilonNet.Models
{
    public interface IPurchaseRepository
    {
        void Add(Purchase purchase);
        void Edit(Purchase purchase);
        void Delete(int id);
        IEnumerable<Purchase> GetAll();
        IEnumerable<Supplier> GetAllSuppliers();
        IEnumerable<PurchaseDetail> GetPurchasesDetails();
    }
}
