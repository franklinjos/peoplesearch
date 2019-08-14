using PeopleSearch.Models;
using SQLite.CodeFirst;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Web;

namespace PeopleSearch.EntityFramework
{
    public class PeopleDbInitializer : SqliteDropCreateDatabaseAlways<PeopleContext>
    {

        public PeopleDbInitializer(DbModelBuilder modelBuilder)
       : base(modelBuilder) { }

        protected override void Seed(PeopleContext context)
        {
            var curContext = HttpContext.Current;

            IList<Person> people = new List<Person>
            {
                new Person() { FirstName = "John", LastName = "Doe", DateOfBirth = new System.DateTime(2012, 6, 8), Picture=  curContext != null ? File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/App_Data/seed/1.jpg")) : new byte[] { 0x12}, Interests= "[\"hiking\",\"biking\",\"skiing\"]",  Address = new Address() { AddressLine1 = "123 9th Ave", City = "Redmond", State = "WA", ZipCode = "98052", Country = "United States" } },
                new Person() { FirstName = "James", LastName = "Taylor", DateOfBirth = new System.DateTime(2001, 10, 18),  Picture= curContext != null ? File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/App_Data/seed/2.jpg")) : new byte[] { 0x12}, Interests= "[\"cooking\",\"Jogging\"]", Address = new Address() { AddressLine1 = "124 9th Ave", City = "Redmond", State = "WA", ZipCode = "98052", Country = "United States" } },
                new Person() { FirstName = "Anthony", LastName = "Glunt", DateOfBirth = new System.DateTime(1998, 1, 21),  Picture= curContext != null ? File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/App_Data/seed/3.jpg")) : new byte[] { 0x12}, Interests= "[\"football\",\"biking\",\"basketball\"]", Address = new Address() { AddressLine1 = "425 6th Street", City = "Bellevue", State = "WA", ZipCode = "98006", Country = "United States" } },
                new Person() { FirstName = "Kurt", LastName = "Nagel", DateOfBirth = new System.DateTime(1998, 5, 13), Picture= curContext != null ? File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/App_Data/seed/4.jpg")) : new byte[] { 0x12}, Interests= "[\"cricket\",\"tennis\",\"car racing\"]", Address = new Address() { AddressLine1 = "326 9th Ave", City = "Redmond", State = "WA", ZipCode = "98052", Country = "United States" } },
                new Person() { FirstName = "Chad", LastName = "Brown", DateOfBirth = new System.DateTime(1978, 4, 16),  Picture= curContext != null ? File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/App_Data/seed/5.jpg")) : new byte[] { 0x12}, Interests= "[\"soccer\",\"hockey\"]", Address = new Address() { AddressLine1 = "623 9th Ave", City = "Bothell", State = "WA", ZipCode = "98021", Country = "United States" } },
                new Person() { FirstName = "Jacob", LastName = "Leach", DateOfBirth = new System.DateTime(1975, 12, 25),  Picture= curContext != null ? File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/App_Data/seed/6.jpg")) : new byte[] { 0x12}, Interests= "[\"hunting\",\"camping\",\"spear fishing\"]", Address = new Address() { AddressLine1 = "624 9th Ave", City = "Bellevue", State = "WA", ZipCode = "98004", Country = "United States" } },
                new Person() { FirstName = "David", LastName = "Edwards", DateOfBirth = new System.DateTime(1988, 8, 7),  Picture= curContext != null ? File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/App_Data/seed/7.jpg")) : new byte[] { 0x12}, Interests= "[\"skiing\",\"snowboarding\",\"photography\",\"travelling\"]", Address = new Address() { AddressLine1 = "175 9th Ave", City = "Kirkland", State = "WA", ZipCode = "98033", Country = "United States" } }
            };

            context.People.AddRange(people);

            base.Seed(context);
        }
    }
}