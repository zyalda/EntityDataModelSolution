using CommonLayer.Models;
using System.Collections.Generic;

namespace CommonLayer.Interfaces
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetPeople();

        Person GetPerson(int id);

        void AddPerson(Person newPerson);

        void UpdatePerson(int id, Person updatedPerson);

        void DeletePerson(int id);
    }
}
