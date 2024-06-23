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
        public delegate void EntityEventHandler(object sender, PerformedEventArgs eventArgs);
        public EntityEventHandler _entityEventHandler;
        private readonly IRepository<Employee> _employeePresentation;

        public EmployeeReader(IRepository<Employee> employeePresentation)
        {
            EmployeeMessage = "Employee is";
            Employee = new List<Employee>();
            _employeePresentation = employeePresentation;
        }

        private IEnumerable<Employee> _employee;
        public IEnumerable<Employee> Employee
        {
            get { return _employee; }
            set
            {
                if (_employee == value)
                    return;
                _employee = value;
                RaisePropertyChanged();
            }
        }

        private string EmployeeMessage { get; set; }
        public void RefreshEmployee()
        {
            Employee = _employeePresentation.GetAll();
        }

        public void PrintEmployees()
        {
            EmployeeMessage = "Employees are";
            Employee = _employeePresentation.GetAll();
            Employee.WriteToFile();
            _entityEventHandler += DelegateMethod;
            _entityEventHandler(this, new PerformedEventArgs(EventsArgsTypes.loaded));
        }
        public Employee FindEmployeeById(int id)
        {
            var employee = _employeePresentation.FindById(id);
            _entityEventHandler += DelegateMethod;

            if(!string.IsNullOrEmpty(employee.FirstName))
                _entityEventHandler(this, new PerformedEventArgs(EventsArgsTypes.founded));
            else
                _entityEventHandler(this, new PerformedEventArgs(EventsArgsTypes.notfound));
            
            return employee;
        }
        public void AddEmployee(string employeeString)
        {
            string[] employeeArray = employeeString.Split(',');
            try
            {
                _employeePresentation.Add(EmployeeAccount.EmployeeModel(employeeArray));
                PrintEmployees();
            }
            catch(ArgumentNullException ex)
            {
                Console.WriteLine("The values can not be empty. " + ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"The id or email you entered is not valid. {ex.Message}");
            }catch(ArgumentOutOfRangeException ex)
            {
            Console.WriteLine($"{ex.Message}");
            }
        }

        public void UpdateEmployee(string employeeString)
        {
            string[] employeeArray = employeeString.Split(',');
            var employee = EmployeeAccount.EmployeeModel(employeeArray);
            _employeePresentation.Update(employee.EmployeeId, employee);
            _entityEventHandler += DelegateMethod;
            _entityEventHandler(this, new PerformedEventArgs(EventsArgsTypes.updated));
        }

        public void DeleteEmployee(int id)
        {
            _employeePresentation.Delete(id);
            _entityEventHandler += DelegateMethod;
            _entityEventHandler(this, new PerformedEventArgs(EventsArgsTypes.deleted));
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
