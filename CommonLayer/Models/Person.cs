using CommonLayer.Interfaces;
using System;

namespace CommonLayer.Models
{
    public class Person : ILoggable
    {
        public int Id { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public DateTime StartDate { get; set; }
        public int Rating { get; set; }

        public string Log()
        {
            return ToString();
        }

        public override string ToString()
        {
            return string.Format(@"Id: {0} Name: {1} Family name: {2} Date: {3} Rating: {4}", Id.ToString(), GivenName, FamilyName, StartDate.Date.ToString(), Rating.ToString());
        }
    }
}
