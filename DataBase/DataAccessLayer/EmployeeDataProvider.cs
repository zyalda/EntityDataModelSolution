using CommonLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataBase.DataAccessLayer
{
    public class EmployeeDataProvider : IRepository<Employee>
    {
        public EmployeeDataProvider()
        {
        }

        public IEnumerable<Employee> GetAll()
        {
            using (var Context = UnitOfWork.Context)
            {
                return Context.Employee.ToList();
            };
        }
        public Employee FindById(int id)
        {
            using (var Context = UnitOfWork.Context)
            {
               return Context.Employee.Where(x=>x.EmployeeId == id).FirstOrDefault();
            };
        }

        public void Add(Employee employee)
        {
            using (var Context = UnitOfWork.Context)
            {
                var item = Context.Employee.Where(x => x.EmployeeId == employee.EmployeeId).FirstOrDefault();
                if (item != null)
                {
                   new ArgumentException(employee.FirstName);
                }

                Context.Employee.Add(employee);
                Context.SaveChanges();
                Context.Dispose();
            };
        }

        public void Update(int employeeId, Employee employee)
        {
            using (var Context = UnitOfWork.Context)
            {
                Employee item = Context.Employee.Where(x => x.EmployeeId == employeeId).FirstOrDefault();
                item.EmployeeId = employee.EmployeeId;
                item.FirstName = employee.FirstName;
                item.LastName = employee.LastName;
                item.LastName = employee.LastName;
                item.EmailAddress = employee.EmailAddress;
                Context.SaveChanges();
                Context.Dispose();
            };
        }

        public void Delete(int employeeId)
        {
            using (var Context = UnitOfWork.Context)
            {
                try
                {
                    var employee = Context.Employee.Where(x => x.EmployeeId == employeeId).FirstOrDefault();
                    Context.Employee.Remove(employee);
                    Context.SaveChanges();
                    Context.Dispose();
                }
                catch(ArgumentNullException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            };
        }
    }
}
