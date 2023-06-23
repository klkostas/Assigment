using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpsilonNet.Models
{
    public interface IPurchaseDetailRepository
    {
        void Add(PurchaseDetail purchaseDetail);
        void Edit(PurchaseDetail purchaseDetail);
        void Delete(int id);
        IEnumerable<PurchaseDetail> GetAll();
        IEnumerable<Purchase> GetAllPurchases();
        IEnumerable<Item> GetAllItems();
    }
}
