using CommonLayer.Models;
using System;

namespace DataAccess.Services
{
    public static class PersonAccount
    {
        public static Person ProvidePerson(string[] personArray)
        {
            Person person = new Person
            {
                Id = int.Parse(personArray[0]),
                GivenName = personArray[1],
                FamilyName = personArray[2],
                StartDate = DateTime.Parse(personArray[3]).Date,
                Rating = int.Parse(personArray[4]),
            };
            return person;
        }
    }
}
