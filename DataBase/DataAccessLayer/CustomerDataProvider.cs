using CommonLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataBase.DataAccessLayer
{
    public class CustomerDataProvider : IRepository<Customer>
    {
        public IEnumerable<Customer> GetAll()
        {
            using (var Context = UnitOfWork.Context)
            {
                return Context.Customer.ToList();
            };
        }

        public Customer FindById(int customerId)
        {
            using (var Context = UnitOfWork.Context)
            {
               return Context.Customer.Where(x=>x.CustomerId == customerId).FirstOrDefault();
            };
        }

        public void Add(Customer customer)
        {
            using (var Context = UnitOfWork.Context)
            {
                Customer item = Context.Customer.Where(x => x.CustomerId == customer.CustomerId).FirstOrDefault();
                if (item != null)
                {
                    new ArgumentNullException(customer.FirstName);
                }

                Context.Customer.Add(customer);
                Context.SaveChanges();
                Context.Dispose();
            };
        }

        public void Update(int id, Customer customer)
        {
            using (var Context = UnitOfWork.Context)
            {
                Customer item = Context.Customer.Where(x => x.CustomerId == id).FirstOrDefault();
                item.CustomerId = customer.CustomerId;
                item.FirstName = customer.FirstName;
                item.LastName = customer.LastName;
                item.EmailAddress = customer.EmailAddress;
                Context.SaveChanges();
                Context.Dispose();
            };
        }

        public void Delete(int customerId)
        {
            using (var Context = UnitOfWork.Context)
            {
                var customer = Context.Customer.Where(x => x.CustomerId == customerId).FirstOrDefault();
                Context.Customer.Remove(customer);
                Context.SaveChanges();
                Context.Dispose();
            };
        }
    }
}