using NotiblingBackend.Domain.Entities;
using NotiblingBackend.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public Task<bool> Add(Customer obj)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Customer>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Customer obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int? id)
        {
            throw new NotImplementedException();
        }

    }
}
