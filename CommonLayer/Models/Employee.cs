using CommonLayer.Interfaces;

namespace CommonLayer.Models
{
    public class Employee : EntityBase, IEmployee
    {
        public Employee() : this(0)
        {       
        }
        public Employee(int employeeId)
        {
            EmployeeId = employeeId;
        }

        public int EmployeeId { get; set; }
        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public string FullName
        {
            get
            {
                string fullName = LastName;
                if (!string.IsNullOrWhiteSpace(FirstName))
                {
                    if (!string.IsNullOrWhiteSpace(fullName))
                    {
                        fullName += ", ";
                    }
                    fullName += FirstName;
                }
                return fullName;
            }
        }

        public static int InstanceCount { get; set; }

        private string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }

        public string Log() => $"Id: {EmployeeId}: Name: {FirstName}: Full name: {FullName} Email: {EmailAddress} Status: {EntityState}\n";

        public override bool Validate()
        {
            var isValid = true;

            if (string.IsNullOrWhiteSpace(LastName)) isValid = false;
            if (string.IsNullOrWhiteSpace(EmailAddress)) isValid = false;

            return isValid;
        }
    }
}
