using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpsilonNet.Models
{
    public interface ISupplierRepository
    {
        void Add(Supplier supplier);
        void Edit(Supplier supplier);
        void Delete(int id);
        IEnumerable<Supplier> GetAll();
    }
}
