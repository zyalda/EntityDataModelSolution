namespace DataBase.DataAccessLayer
{
    internal class UnitOfWork
    {

        public static restfullDBEntities Context => new restfullDBEntities();
    }
}
