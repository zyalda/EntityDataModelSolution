using CommonLayer;
using CommonLayer.Models;

namespace DataAccess.Services
{
    public static class EmployeeAccount
    {
        public static Employee ProvideEmployee(DataBase.Employee item, Employee employee)
        {
            employee.EmployeeId = item.EmployeeId;
            employee.FirstName = item.FirstName;
            employee.LastName = item.LastName;
            employee.EmailAddress = item.EmailAddress;
            employee.EntityState = EntityStateOption.Active;
            return employee;
        }
        public static DataBase.Employee ProvideEmployeeDataBase(Employee modelEmployee)
        {
            DataBase.Employee employee = new DataBase.Employee()
            {
                EmployeeId = modelEmployee.EmployeeId,
                FirstName = modelEmployee.FirstName,
                LastName = modelEmployee.LastName,
                EmailAddress = modelEmployee.EmailAddress,
            };
            return employee;
        }

        public static Employee EmployeeModel(string[] employeeString)
        {
            Employee employee = new Employee();
            Validations.ValidationEntityArray(employeeString);
            employee.EmployeeId = int.Parse(employeeString[0]);
            employee.FirstName = employeeString[1];
            employee.LastName = employeeString[2];
            employee.EmailAddress = employeeString[3];
            return employee;
        }
    }
}
