namespace CommonLayer.Interfaces
{
    public interface ICustomer : ILoggable
    {
        int CustomerId { get; set; }
        int CustomerType { get; set; }
        string EmailAddress { get; set; }
        string FirstName { get; set; }
        string FullName { get; }
        string LastName { get; set; }
    }
}