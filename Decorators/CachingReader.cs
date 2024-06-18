using CommonLayer;
using CommonLayer.Interfaces;
using CommonLayer.Models;
using System;
using System.Collections.Generic;

namespace Decorators
{
    public class CachingReader : IRepository<Employee>
    {
        private IRepository<Employee> _wrappedReader;
        private IEnumerable<Employee> _cachedItems;

        private TimeSpan _cacheDuration = new TimeSpan(0, 0, 10);
        private DateTime _dataDateTime;

        public CachingReader(IRepository<Employee> wrappedReader)
        {
            _wrappedReader = wrappedReader;
        }

        public IEnumerable<Employee> GetAll()
        {
            ValidateCache();
            return _cachedItems;
        }

        private bool IsCacheValid
        {
            get
            {
                if (_cachedItems == null)
                    return false;
                var _cacheAge = DateTime.Now - _dataDateTime;
                return _cacheAge < _cacheDuration;
            }
        }

        private void ValidateCache()
        {
            if (IsCacheValid)
                return;

            try
            {
                _cachedItems = _wrappedReader.GetAll();
                _dataDateTime = DateTime.Now;
            }
            catch
            {
                _cachedItems = new List<Employee>()
                {
                    new Employee()
                    {
                        FirstName = "No Data Available",
                    }
                };
                InvalidateCache();
            }
        }

        private void InvalidateCache()
        {
            _dataDateTime = DateTime.MinValue;
        }

        void PrintAll()
        {
            GetAll().WriteToFile();
        }

        public Employee FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Employee dataItem)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Employee dataItem)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
