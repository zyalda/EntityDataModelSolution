using CommonLayer;
using CommonLayer.Interfaces;
using CommonLayer.Models;
using System.Collections.Generic;

namespace DataAccess.Services
{
    public class EmployeeDataReader : IRepository<Employee>//IDataReader<Employee>
    {
        public IEnumerable<Employee> GetAll()
        {
            IList<Employee> list = new List<Employee>();
            var items = DataAccessFactory.CreateEmployeeDataProvider().GetAll();
            foreach (var item in items)
            {
                var employee = DataAccessFactory.CreateEmployee();
                var emplo = EmployeeAccount.ProvideEmployee(item, (Employee)employee);
                list.Add(emplo);
            }
            return list;
        }

        public Employee FindById(int id)
        {
            var item = DataAccessFactory.CreateEmployeeDataProvider().FindById(id);
            if (item != null)
            {
                var employee = DataAccessFactory.CreateEmployee();
                var findedEmployee = EmployeeAccount.ProvideEmployee(item, (Employee)employee);
                return findedEmployee;
            }
            return new Employee();
        }

        public void PrintAll()
        {
            GetAll().WriteToFile();
        }

        public void Add(Employee dataItem)
        {
            var employee = EmployeeAccount.ProvideEmployeeDataBase(dataItem);
            DataAccessFactory.CreateEmployeeDataProvider().Add(employee);
        }

        public void Update(int id, Employee dataItem)
        {
            var employee = EmployeeAccount.ProvideEmployeeDataBase(dataItem);
            DataAccessFactory.CreateEmployeeDataProvider().Update(id, employee);
        }
        public void Delete(int id)
        {
            DataAccessFactory.CreateEmployeeDataProvider().Delete(id);
        }
    }
}
