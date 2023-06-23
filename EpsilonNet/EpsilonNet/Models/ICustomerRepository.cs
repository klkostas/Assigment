using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpsilonNet.Models
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        void Edit(Customer customer);
        void Delete(int id);
        IEnumerable<Customer> GetAll();
    }
}
