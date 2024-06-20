using CommonLayer.Interfaces;
using CommonLayer.Models;
using DataAccess.Services;
using System;

namespace BusinessLayer.Services
{
    public static class RepositoryFactory
    {
        public static IRepository<T> GetRepository<T>(string repositoryType) where T : class
        {
            IRepository<T> repository = null;

            switch (repositoryType)
            {
                case "Employee":
                    IRepository<Employee> repositoryEmployee = new EmployeeDataReader();
                    repository = (IRepository<T>)repositoryEmployee;
                    break;
                case "Customer":
                    IRepository<Customer> repositoryCustomer = new CustomerDataReader();
                    repository = (IRepository<T>)repositoryCustomer;
                    break;
                case "Person":
                    IRepository<Person> repositoryPeople = new PeopleDataReader();
                    repository = (IRepository<T>)repositoryPeople;
                    break;
                default:
                    throw new ArgumentException("Invalid repository type");
            }

            return repository;
        }
    }
}
