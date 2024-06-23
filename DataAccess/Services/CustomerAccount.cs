using CommonLayer;
using CommonLayer.Models;

namespace DataAccess.Services
{
    public class CustomerAccount
    {
        public static Customer CreateCustomer(DataBase.Customer item, Customer customer)
        {
            customer.CustomerId = item.CustomerId;
            customer.FirstName = item.FirstName;
            customer.LastName = item.LastName;
            customer.EmailAddress = item.EmailAddress;

            return customer;
        }

        public static DataBase.Customer AddCustomer(Customer item)
        {
            DataBase.Customer customer = new DataBase.Customer();
            customer.CustomerId = item.CustomerId;
            customer.FirstName = item.FirstName;
            customer.LastName = item.LastName;
            customer.EmailAddress = item.EmailAddress;

            return customer;
        }

        public static Customer CustomerModel(string[] customerString)
        {
            Customer customer = new Customer();
            Validations.ValidationEntityArray(customerString);
            customer.CustomerId = int.Parse(customerString[0]);
            customer.FirstName = customerString[1];
            customer.LastName = customerString[2];
            customer.EmailAddress = customerString[3];
            return customer;
        }
    }
}
