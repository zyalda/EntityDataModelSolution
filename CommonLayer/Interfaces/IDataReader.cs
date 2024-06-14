using System.Collections.Generic;

namespace CommonLayer.Interfaces
{
    public interface IDataReader<T> where T : class
    {
        IEnumerable<T> RetrieveAll();
        T FindById(int id);
        void Add(T dataItem);
        void Update(int id, T dataItem);
        void Delete(int id);
        void PrintAll();
    }
}