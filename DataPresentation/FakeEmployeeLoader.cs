using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace DataPresentation
{
    public class FakeEmployeeLoader
    {
        private string _filePath;

        public FakeEmployeeLoader(string filePath)
        {
            _filePath = filePath;
        }

        public IList<Employee> LoadFakeEmployees()
        {
            var employees = new List<Employee>();
            if (File.Exists(_filePath))
            {
                using (var reader = new StreamReader(_filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var elements = line.Split(',');
                        var employee = new Employee()
                        {
                            EmployeeId = Int32.Parse(elements[0]),
                            FirstName = elements[1],
                            LastName = elements[2],
                            EmailAddress = elements[3],
                        };
                        employees.Add(employee);
                    }
                }
            }
            return employees;
        }
    }
}
