using CommonLayer;
using CommonLayer.Interfaces;
using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace DataAccess.Services
{
    public class PeopleDataReader : IDataReader<Person>
    {
        public IEnumerable<Person> RetrieveAll()
        {
            return DataAccessFactory.CreatePersonDataProvider().GetAll();
        }
        public void PrintAll()
        {
            RetrieveAll().WriteToFile();
        }

        public Person FindById(int id)
        {
            try
            {
                var item = DataAccessFactory.CreatePersonDataProvider().FindById(id);
                return item;
            }
            catch (ObjectNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message} Exception Caught!");
                return DataAccessFactory.CreatePerson();
            }
        }

        public void Add(Person person)
        {
            
            DataAccessFactory.CreatePersonDataProvider().Add(person);
        }

        public void Update(int id, Person dataItem)
        {
            DataAccessFactory.CreatePersonDataProvider().Update(id, dataItem);
        }

        public void Delete(int id)
        {
            DataAccessFactory.CreatePersonDataProvider().Delete(id);
        }
    }
}
