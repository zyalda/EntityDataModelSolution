using CommonLayer;
using CommonLayer.Interfaces;
using CommonLayer.Models;
using DataAccess.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BusinessLayer.Services
{
    public class CustomerReader
    {

        public delegate void EntityEventHandler(object sender, PerformedEventArgs eventArgs);
        public EntityEventHandler _entityEventHandler;
        private readonly IRepository<Customer> _customerPresentation;

        public CustomerReader(IRepository<Customer> customerPresentation)
        {
            CustomerMessage = "Customer is";
            Customers = new List<Customer>();
            _customerPresentation = customerPresentation;
        }

        public IEnumerable<Customer> Customers { get; set; }
        private string CustomerMessage { get; set; }
        public void RefreshCustomer()
        {
            Customers = _customerPresentation.GetAll();
        }

        public void PrintCustomers()
        {
            Customers = _customerPresentation.GetAll();
            Customers.WriteToFile();
            CustomerMessage = "Customers are";
            
            _entityEventHandler += DelegateMethod;
            _entityEventHandler(this, new PerformedEventArgs(EventsArgsTypes.loaded));
        }
        public void AddCustomer(string customer)
        {
            string[] customerArray = customer.Split(',');
            try
            {
                var addedCustomer = CustomerAccount.CustomerModel(customerArray);
                _customerPresentation.Add(addedCustomer);
                _entityEventHandler += DelegateMethod;
                _entityEventHandler(this, new PerformedEventArgs(EventsArgsTypes.added));
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("The values can not be empty. " + ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"The id you entered is not valid. {ex.Message}");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
           
        }
        public Customer FindCustomerById(int customerID)
        {
            var customer = _customerPresentation.FindById(customerID);
            _entityEventHandler += DelegateMethod;
            if (!string.IsNullOrEmpty(customer.FirstName))
                _entityEventHandler(this, new PerformedEventArgs(EventsArgsTypes.founded));
            else
                _entityEventHandler(this, new PerformedEventArgs(EventsArgsTypes.notfound));

            return customer;
        }
        public void UpdateCustomer(string customer)
        {
            string[] customerArray = customer.Split(',');
            var addedCustomer = CustomerAccount.CustomerModel(customerArray);
            _customerPresentation.Update(addedCustomer.CustomerId, addedCustomer);
            _entityEventHandler += DelegateMethod;
            _entityEventHandler(this, new PerformedEventArgs(EventsArgsTypes.updated));
        }
        public void DeleteCustomer(int customerId)
        {
            _customerPresentation.Delete(customerId );
            _entityEventHandler += DelegateMethod;
            _entityEventHandler(this, new PerformedEventArgs(EventsArgsTypes.deleted));
        }

        public string DataReaderType
        {
            get { return _customerPresentation.GetType().ToString(); }
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void DelegateMethod(object sender, PerformedEventArgs e)
        {
            Console.WriteLine($"{CustomerMessage} {e.EventsArgsType}.");
        }
        #endregion
    }
}
