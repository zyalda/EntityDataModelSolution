using BusinessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public class BL_CustomerRepository
    {
        public BL_CustomerRepository()
        {

        }
        public IEnumerable<CustomerModel> RetrieveAll()
        {
            List<CustomerModel> customerList = new List<CustomerModel>();

            return customerList;
        }
        public CustomerModel RetrieveById(int customerId)
        {
            // Create the instance of the Customer class
            // Pass in the requested id
            CustomerModel customer = new CustomerModel(customerId);

            // Code that retrieves the defined customer

            // Temporary hard-coded values to return 
            // a populated customer
            if (customerId == 1)
            {
                customer.EmailAddress = "fbaggins@hobbiton.me";
                customer.FirstName = "Frodo";
                customer.LastName = "Baggins";
            }
            return customer;
        }

        /// <summary>
        /// Saves the current customer.
        /// </summary>
        /// <returns></returns>
        public bool Save(CustomerModel customer)
        {
            var success = true;

            if (customer.HasChanges)
            {
                if (customer.IsValid)
                {
                    if (customer.IsNew)
                    {
                        // Call an Insert Stored Procedure

                    }
                    else
                    {
                        // Call an Update Stored Procedure
                    }
                }
                else
                {
                    success = false;
                }
            }
            return success;
        }
    }
}
