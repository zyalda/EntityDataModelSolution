using CommonLayer.Interfaces;
using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSVData
{
    public class CsvPersonDataProvider : IRepository<Person>
    {
        private string Basepath { get; }

        public CsvPersonDataProvider()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
                //@"C:\Users\zeenay\source\repos\EntityDataModelSolution\CSVData\bin\Debug\";
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
            using (FileStream fileStream = new FileStream(Basepath, FileMode.Append, FileAccess.Write))
            {
                StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.Write(string.Format(@"{0},{1},{2},{3},{4},", person.Id.ToString(), person.GivenName, person.FamilyName, person.StartDate.Date.ToString(), person.Rating.ToString()));
                streamWriter.Close();
                fileStream.Close();
            }
        }

        public void Update(int id, Person person)
        {
           var personString = string.Format(@"{0},{1},{2},{3},{4},", person.Id.ToString(), person.GivenName, person.FamilyName, person.StartDate.Date.ToString(), person.Rating.ToString());

            var lines = File.ReadAllLines(Basepath);

            for (int i = 0; i < lines.Length; ++i)
            {
                var line = lines[i].Split(',');
                if (int.Parse(line[0]) == person.Id)
                    lines[i] = lines[i].Replace(lines[i], personString);
            }
             File.WriteAllLines(Basepath, lines);
        }

        public void Delete(int id)
        {
            var lines = File.ReadAllLines(Basepath);

            for (int i = 0; i < lines.Length; ++i)
            {
                var line = lines[i].Split(',');
                if (int.Parse(line[0]) == id)
                {
                    var newLines = lines.Where(x => !x.Contains(line[i])).ToArray();
                    File.WriteAllLines(Basepath, newLines);
                }
            }
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
                        StartDate = DateTime.Parse(segments[3]).Date,
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
