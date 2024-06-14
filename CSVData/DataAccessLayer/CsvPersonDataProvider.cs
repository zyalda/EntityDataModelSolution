using CommonLayer.Interfaces;
using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace CSVData
{
    public class CsvPersonDataProvider : IRepository<Person>
    {
        private string Basepath { get; }

        public CsvPersonDataProvider()
        {
            var basePath = AppContext.BaseDirectory;
            var filename = "People.csv";
            Basepath = basePath + filename;
        }

        public IEnumerable<Person> GetAll()
        {
            return LoadPeople();
        }
        public Person FindById(int id)
        {
            var people = LoadPeople();
            foreach (var person in people)
            {
                if (person.Id == id)
                    return person;
            }

            return new Person();
        }

        public void Add(Person person)
        {
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(Basepath)))
            {
                writer.Write(string.Format(@"{0},{1},{2},{3},{4},", person.Id.ToString(), person.GivenName, person.FamilyName, person.StartDate.ToString(), person.Rating.ToString()));

                //#region Loading and Adding Person to CSV file
                //using (CsvWriter csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                //{
                //    csvWriter.Context.Configuration.Delimiter = ",";
                //    csvWriter.WriteField(person.Id);
                //    csvWriter.WriteField(person.GivenName);
                //    csvWriter.WriteField(person.FamilyName);
                //    csvWriter.WriteField(person.StartDate);
                //    csvWriter.WriteField(person.Rating);
                //    csvWriter.NextRecord();
                //}
               // writer.Flush();
            }
        }

        public void Update(int id, Person person)
        {
            //using (var Context = UnitOfWork.Context)
            //{
            //    Employee item = Context.Employee.Where(x => x.Id == id).FirstOrDefault();
            //    item.LastName = employee.LastName;
            //    Context.SaveChanges();
            //    Context.Dispose();
            //};
        }

        public void Delete(int id)
        {
            //using (var Context = UnitOfWork.Context)
            //{
            //    var employee = Context.Employee.Where(x => x.Id == id).FirstOrDefault();
            //    Context.Employee.Remove(employee);
            //    Context.SaveChanges();
            //    Context.Dispose();
            //};
        }

        //private async Task<IList<Person>> Result()
        //{
        //    List<Person> list = (List<Person>)await Task.Run(()=> LoadPeople());
        //    //var people = await LoadPeople();

        //    People.AddRange(list);
        //    return People;
        //}

        private IList<Person> LoadPeople()
        {
            List<Person> People = new List<Person>();
            using (var stream = new StreamReader(File.OpenRead(Basepath)))
            {
                string line = "";
                while ((line = stream.ReadLine()) != null)
                {
                    #region Loading and Adding Person to In-Memory list

                    var segments = line.Split(',');
                    var person = new Person
                    {
                        Id = int.Parse(segments[0]),
                        GivenName = segments[1],
                        FamilyName = segments[2],
                        StartDate = DateTime.Parse(segments[3]),
                        Rating = int.Parse(segments[4])
                    };

                    if (!People.Contains(person))
                    {
                        People.Add(person);
                    }

                    #endregion
                }
            }
            return People;
        }
    }
}
