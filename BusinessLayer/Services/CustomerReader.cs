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

        public delegate void EventHandler(object sender, PerformedEventArgs eventArgs);
        public EventHandler _eventHandler;
        private readonly IDataReader<Customer> _customerPresentation;

        public CustomerReader(IDataReader<Customer> customerPresentation)
        {
            CustomerMessage = "Customer is";
            Customers = new List<Customer>();
            _customerPresentation = customerPresentation;
        }

        public IEnumerable<Customer> Customers { get; set; }
        private string CustomerMessage { get; set; }
        public void RefreshCustomer()
        {
            //PropertyChanged += delegate(object sender, PropertyChangedEventArgs eventArgs)
            //{
            //    Console.WriteLine("List employees is done Loading." + eventArgs.PropertyName);
            //};
            Customers = _customerPresentation.RetrieveAll();
        }

        public void PrintCustomers()
        {
            Customers = _customerPresentation.RetrieveAll();
            Customers.WriteToFile();
            CustomerMessage = "Customers are";
            _eventHandler += DelegateMethod;
            _eventHandler(this, new PerformedEventArgs(EventsArgsTypes.loaded));
        }
        public void AddCustomer(string customer)
        {
            string[] customerArray = customer.Split(',');
            var addedCustomer = CustomerAccount.CustomerModel(customerArray);
            _customerPresentation.Add(addedCustomer);
            _eventHandler += DelegateMethod;
            _eventHandler(this, new PerformedEventArgs(EventsArgsTypes.added));
        }
        public Customer FindCustomerById(int customerID)
        {
            var customer = _customerPresentation.FindById(customerID);
            _eventHandler += DelegateMethod;
            if (!string.IsNullOrEmpty(customer.FirstName))
                _eventHandler(this, new PerformedEventArgs(EventsArgsTypes.founded));
            else
                _eventHandler(this, new PerformedEventArgs(EventsArgsTypes.notfound));

            return customer;
        }
        public void UpdateCustomer(string customer)
        {
            string[] customerArray = customer.Split(',');
            var addedCustomer = CustomerAccount.CustomerModel(customerArray);
            _customerPresentation.Update(addedCustomer.CustomerId, addedCustomer);
            _eventHandler += DelegateMethod;
            _eventHandler(this, new PerformedEventArgs(EventsArgsTypes.updated));
        }
        public void DeleteCustomer(int customerId)
        {
            _customerPresentation.Delete(customerId );
            _eventHandler += DelegateMethod;
            _eventHandler(this, new PerformedEventArgs(EventsArgsTypes.deleted));
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
