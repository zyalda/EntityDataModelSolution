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

        public delegate void EventHandler(object sender, PerformedEventArgs eventArgs);
        public EventHandler _eventHandler;
        private readonly IDataReader<Person> _PersonPresentation;

        public PeopleReader(IDataReader<Person> personPresentation)
        {
            People = new List<Person>();
            PersonMessage = "Person is";
            _PersonPresentation = personPresentation;
        }

        public IEnumerable<Person> People { get; set; }
        private string PersonMessage { get; set; }
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
            PersonMessage = "People are";
            _eventHandler += DelegateMethod;
            _eventHandler(this, new PerformedEventArgs(EventsArgsTypes.loaded));
        }
        public Person FindPersonById(int id)
        {
            var person = _PersonPresentation.FindById(id);
            _eventHandler += DelegateMethod;            
            
            if (!string.IsNullOrEmpty(person.GivenName))
                _eventHandler(this, new PerformedEventArgs(EventsArgsTypes.founded));
            else
                _eventHandler(this, new PerformedEventArgs(EventsArgsTypes.notfound));
            
            return _PersonPresentation.FindById(id);
        }
        public void AddPerson(string personString)
        {
            string[] personArray = personString.Split(',');
            _PersonPresentation.Add(PersonAccount.ProvidePerson(personArray));
            _eventHandler += DelegateMethod;
            _eventHandler(this, new PerformedEventArgs(EventsArgsTypes.added));
        }
        public void UpdatePerson(string personString)
        {
            string[] personArray = personString.Split(',');
            var person = PersonAccount.ProvidePerson(personArray);
            _PersonPresentation.Update(person.Id, person);
            _eventHandler += DelegateMethod;
            _eventHandler(this, new PerformedEventArgs(EventsArgsTypes.updated));
        }
        public void DeletePerson(int id)
        {
            _PersonPresentation.Delete(id);
            _eventHandler += DelegateMethod;
            _eventHandler(this, new PerformedEventArgs(EventsArgsTypes.deleted));
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

        public void DelegateMethod(object sender, PerformedEventArgs e)
        {
            Console.WriteLine($"{PersonMessage} {e.EventsArgsType}");
        }
        #endregion
    }
}
