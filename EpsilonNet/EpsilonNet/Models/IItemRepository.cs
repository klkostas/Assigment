using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpsilonNet.Models
{
    public interface IItemRepository
    {
        void Add(Item item);
        void Edit(Item item);
        void Delete(int id);
        IEnumerable<Item> GetAll();
        IEnumerable<Supplier> GetAllSuppliers();
    }
}
