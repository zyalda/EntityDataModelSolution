using CommonLayer.Interfaces;
using CommonLayer.Models;
using CSVData;
using DataBase.DataAccessLayer;

namespace DataAccess
{
    public static class DataAccessFactory
    {
        public static IRepository<DataBase.Customer> CreateCustomerDataProvider()
        {
            return new CustomerDataProvider();
        }

        public static IRepository<DataBase.Employee> CreateEmployeeDataProvider()
        {
            return new EmployeeDataProvider();
        }

        public static IRepository<Person> CreatePersonDataProvider()
        {
            return new CsvPersonDataProvider();
        }

        public static IEmployee CreateEmployee() { return new Employee(); }
        public static ICustomer CreateCustomer() => new Customer();
        public static Person CreatePerson() => new Person();
    }
}
