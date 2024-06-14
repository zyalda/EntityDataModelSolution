using CommonLayer;
using CommonLayer.Interfaces;
using CommonLayer.Models;
using System;
using System.Collections.Generic;

namespace DataPresentation
{
    public class FakeEmployeeReader : IDataReader<Employee>
    {
        public IList<Employee> FileEmployeesLoader { get; set; }

        public FakeEmployeeReader()
        {
            string filePath = AppContext.BaseDirectory
            + "\\FakeEmployee.txt";

            FakeEmployeeLoader loader = new FakeEmployeeLoader(filePath);
            FileEmployeesLoader = loader.LoadFakeEmployees();
        }

        public IEnumerable<Employee> RetrieveAll()
        {
            return FileEmployeesLoader;
        }

        public void PrintAll()
        {
            RetrieveAll().WriteToFile();
        }

        public Employee FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Employee dataItem)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Employee dataItem)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
