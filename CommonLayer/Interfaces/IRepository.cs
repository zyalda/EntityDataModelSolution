namespace CommonLayer.Interfaces
{
    public interface IRepository<TEntity> : IRepositoryReader<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        void Update(int id, TEntity entity);

        void Delete(int id);
    }
}
