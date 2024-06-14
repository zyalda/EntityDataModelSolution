namespace CommonLayer.Interfaces
{
    public interface IEmployee : ILoggable
    {
        string EmailAddress { get; set; }
        int EmployeeId { get; set; }
        string FirstName { get; set; }
        string FullName { get; }
        string LastName { get; set; }
        //IAccountEmployee EmployeeAccount { get; }
    }
}