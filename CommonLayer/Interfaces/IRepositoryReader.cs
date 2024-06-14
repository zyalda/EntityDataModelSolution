using System.Collections.Generic;

namespace CommonLayer.Interfaces
{
    public interface IRepositoryReader<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        TEntity FindById(int id);
    }
}
