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
    public class EmployeeReader : INotifyPropertyChanged
    {
        public delegate void EventHandler(object sender, PerformedEventArgs eventArgs);
        public EventHandler _eventHandler;
        private readonly IDataReader<Employee> _employeePresentation;

        public EmployeeReader(IDataReader<Employee> employeePresentation)
        {
            EmployeeMessage = "Employee is";
            Employee = new List<Employee>();
            _employeePresentation = employeePresentation;
        }

        public IEnumerable<Employee> Employee { get; set; }
        private string EmployeeMessage { get; set; }
        public void RefreshEmployee()
        {
            Employee = _employeePresentation.RetrieveAll();
        }

        public void PrintEmployees()
        {
            //RefreshEmployee();
            EmployeeMessage = "Employees are";
            Employee = _employeePresentation.RetrieveAll();
            Employee.WriteToFile();
            _eventHandler += DelegateMethod;
            _eventHandler(this, new PerformedEventArgs(EventsArgsTypes.loaded));
        }
        public Employee FindEmployeeById(int id)
        {
            var employee = _employeePresentation.FindById(id);
            _eventHandler += DelegateMethod;
            if (!string.IsNullOrEmpty(employee.FirstName))
                _eventHandler(this, new PerformedEventArgs(EventsArgsTypes.founded));
            else
                _eventHandler(this, new PerformedEventArgs(EventsArgsTypes.notfound));
            
            return employee;
        }
        public void AddEmployee(string employeeString)
        {
            string[] employeeArray = employeeString.Split(',');
            _employeePresentation.Add(EmployeeAccount.EmployeeModel(employeeArray));
            _eventHandler += DelegateMethod;
            _eventHandler(this, new PerformedEventArgs(EventsArgsTypes.added));
        }

        public void UpdateEmployee(string employeeString)
        {
            string[] employeeArray = employeeString.Split(',');
            var employee = EmployeeAccount.EmployeeModel(employeeArray);
            _employeePresentation.Update(employee.EmployeeId, employee);
            _eventHandler += DelegateMethod;
            _eventHandler(this, new PerformedEventArgs(EventsArgsTypes.updated));
        }

        public void DeleteEmployee(int id)
        {
            _employeePresentation.Delete(id);
            _eventHandler += DelegateMethod;
            _eventHandler(this, new PerformedEventArgs(EventsArgsTypes.deleted));
        }
        public void ClearEmployee()
        {
            Employee = new List<Employee>(0);            
        }

        public string DataReaderType
        {
            get {return _employeePresentation.GetType().ToString(); }
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void DelegateMethod(object sender, PerformedEventArgs e)
        {
            Console.WriteLine($"{EmployeeMessage} {e.EventsArgsType}.");
        }
        #endregion
    }
}
