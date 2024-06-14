using CommonLayer.Interfaces;

namespace CommonLayer.Models
{
    public class Customer : EntityBase, ICustomer
    {
        public int CustomerId { get; set; }
        public int CustomerType { get; set; }
        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public string FullName
        {
            get
            {
                string fullName = FirstName;
                if (!string.IsNullOrWhiteSpace(FirstName))
                {
                    if (!string.IsNullOrWhiteSpace(fullName))
                    {
                        fullName += " ";
                    }
                    fullName += LastName;
                }
                return fullName;
            }
        }

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
        public string Log() => $"Id: {CustomerId}: Name: {FullName} Email: {EmailAddress} state: {EntityState}";

        public override bool Validate()
        {
            var isValid = true;

            if (string.IsNullOrWhiteSpace(LastName)) isValid = false;
            if (string.IsNullOrWhiteSpace(EmailAddress)) isValid = false;

            return isValid;
        }

        //public static implicit operator Customer(global::DataBase.Customer v)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
