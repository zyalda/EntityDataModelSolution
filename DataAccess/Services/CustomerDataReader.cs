using CommonLayer;
using CommonLayer.Interfaces;
using CommonLayer.Models;
using System.Collections.Generic;

namespace DataAccess.Services
{
    public class CustomerDataReader : IRepository<Customer>
    {
        ICustomer customer = DataAccessFactory.CreateCustomer();
        public IEnumerable<Customer> GetAll()
        {
            List<Customer> list = new List<Customer>();
            var items = DataAccessFactory.CreateCustomerDataProvider().GetAll();
            foreach (var item in items)
            {
                var customer = DataAccessFactory.CreateCustomer();
                var custo = CustomerAccount.CreateCustomer(item, (Customer)customer);
                list.Add(custo);
            }
            return list;
        }

        public void PrintAll()
        {
            GetAll().WriteToFile();
        }

        public Customer FindById(int customerId)
        {
            var customerDataBase = DataAccessFactory.CreateCustomerDataProvider().FindById(customerId);
            if (customerDataBase != null)
                return CustomerAccount.CreateCustomer(customerDataBase, (Customer)customer);
            else
                return new Customer();
        }

        public void Add(Customer customer)
        {
            var custo = CustomerAccount.AddCustomer(customer);
            DataAccessFactory.CreateCustomerDataProvider().Add(custo);
        }

        public void Update(int id, Customer dataItem)
        {
            var customer = CustomerAccount.AddCustomer(dataItem);
            DataAccessFactory.CreateCustomerDataProvider().Update(id, customer);
        }

        public void Delete(int customerId)
        {
            DataAccessFactory.CreateCustomerDataProvider().Delete(customerId);
        }
    }
}
