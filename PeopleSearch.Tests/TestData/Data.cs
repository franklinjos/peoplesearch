using PeopleSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleSearch.Tests.TestData
{
    public static class Data
    {
        public static IList<Person> GetPeople()
        {
            return new List<Person>
            {
                new Person() { FirstName = "Taylor", LastName = "Smith", DateOfBirth = new System.DateTime(2012, 6, 8), Picture= new byte[] { 0x12},
                    Interests = "[\"hiking\",\"biking\",\"skiing\"]",  Address = new Address() { AddressLine1 = "123 9th Ave", City = "Redmond", State = "WA", ZipCode = "98052", Country = "United States" } },
                new Person() { FirstName = "Jerome", LastName = "Taylor", DateOfBirth = new System.DateTime(2001, 10, 18),  Picture= new byte[] { 0x11}, Interests= "[\"cooking\",\"Jogging\"]", Address = new Address() { AddressLine1 = "124 9th Ave", City = "Redmond", State = "WA", ZipCode = "98052", Country = "United States" } },
                new Person() { FirstName = "Peter", LastName = "Rodes", DateOfBirth = new System.DateTime(1998, 1, 21),  Picture= new byte[] { 0x15}, Interests= "[\"football\",\"biking\",\"basketball\"]", Address = new Address() { AddressLine1 = "425 6th Street", City = "Bellevue", State = "WA", ZipCode = "98006", Country = "United States" } },
            };
        }
    }
}
