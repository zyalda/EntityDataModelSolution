using CommonLayer;
using CommonLayer.Interfaces;
using CommonLayer.Models;
using DataAccess.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BusinessLayer.Services
{
    public class PeopleReader
    {

        public delegate void EventHandler(object sender, EventArgs eventArgs);
        public EventHandler _eventHandler;
        private readonly IDataReader<Person> _PersonPresentation;

        public PeopleReader(IDataReader<Person> personPresentation)
        {
            People = new List<Person>();
            _PersonPresentation = personPresentation;
        }

        public IEnumerable<Person> People { get; set; }
        public void RefreshPeople()
        {
            //PropertyChanged += delegate(object sender, PropertyChangedEventArgs eventArgs)
            //{
            //    Console.WriteLine("List employees is done Loading." + eventArgs.PropertyName);
            //};
            People = _PersonPresentation.RetrieveAll();
        }

        public void PrintPeople()
        {
            People = _PersonPresentation.RetrieveAll();
            People.WriteToFile();
            _eventHandler += DelegateMethod;
            _eventHandler(this, new EventArgs());
        }
        public Person FindPersonById(int id)
        {
            return _PersonPresentation.FindById(id);
        }
        public void AddPerson(string personString)
        {
            string[] personArray = personString.Split(',');
            _PersonPresentation.Add(PersonAccount.ProvidePerson(personArray));
        }
        public void UpdatePerson(string personString)
        {
            string[] personArray = personString.Split(',');
            var person = PersonAccount.ProvidePerson(personArray);
            _PersonPresentation.Update(person.Id, person);
        }
        public void DeletePerson(int id)
        {
            _PersonPresentation.Delete(id);
        }

        public string DataReaderType
        {
            get { return _PersonPresentation.GetType().ToString(); }
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void DelegateMethod(object sender, EventArgs e)
        {
            Console.WriteLine("Employees are loaded.");
        }
        #endregion
    }
}
