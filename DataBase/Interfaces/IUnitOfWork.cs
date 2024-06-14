namespace DataBase.Interfaces
{
    internal interface IUnitOfWork
    {
        restfullDBEntities Context { get; }
    }
}
