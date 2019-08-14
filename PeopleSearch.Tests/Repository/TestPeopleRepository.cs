using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleSearch.EntityFramework;
using PeopleSearch.Models;
using PeopleSearch.Repository;
using PeopleSearch.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleSearch.Tests.Repository
{
    /// <summary>
    /// Intergration test for PeopleRepository
    /// </summary>
    [TestClass]
    public class TestPeopleRepository
    {
        private IPeopleRepository _repository;
        private readonly IPeopleContext _dbContext;

        public TestPeopleRepository()
        {
            _dbContext = new PeopleContext();
        }

        [TestMethod]
        public void RepositoryShouldGetAllPeopleWithAddress()
        {
            //Arrage
            _repository = new PeopleRepository(_dbContext);

            //Execute
            var results = _repository.GetAll().ToList();

            //Assert
            Assert.AreEqual(_dbContext.People.ToList().Count, results.Count);
        }


        [TestMethod]
        public void RepositoryShouldAddPersonWithAddressAndDelete()
        {
            //Arrage
            _repository = new PeopleRepository(_dbContext);
            var person = Data.GetPeople().First<Person>();

            //Execute
            var personCountBeforeAdd = _dbContext.People.ToList().Count;
            var addressCountBeforeAdd = _dbContext.Address.ToList().Count;
            _repository.Add(person);

            var addedPerson = _repository.GetById(personCountBeforeAdd+1);

            //Assert
            Assert.AreEqual(person.FirstName, addedPerson.FirstName);
            Assert.AreEqual(person.LastName, addedPerson.LastName);
            Assert.AreEqual(_dbContext.People.ToList().Count, personCountBeforeAdd + 1);
            Assert.AreEqual(_dbContext.Address.ToList().Count, addressCountBeforeAdd + 1);

            //Clean Up
            _repository.Delete(personCountBeforeAdd + 1);

            Assert.AreEqual(_dbContext.People.ToList().Count, personCountBeforeAdd);
            Assert.AreEqual(_dbContext.Address.ToList().Count, addressCountBeforeAdd);
        }

        [TestMethod]
        public void RepositoryShouldSearchCaseInsensitiveWithNameContainsInFirstOrLastName()
        {
            //Arrage
            _repository = new PeopleRepository(_dbContext);
            var person = Data.GetPeople().First<Person>();

            //Execute
            var searchNameContainsInFirstName = _repository.Search(new PeopleSearchOptions() { Name="kURt" }).ToList();
            var searchNameContainsInLastName = _repository.Search(new PeopleSearchOptions() { Name = "LEACH" }).ToList();
            var searchNameContainsInBoth = _repository.Search(new PeopleSearchOptions() { Name = "D" }).ToList();
            var searchNameContainsWithNoRecord = _repository.Search(new PeopleSearchOptions() { Name = "ZZZ" }).ToList();


            //Assert
            Assert.AreEqual(searchNameContainsInFirstName.First<Person>().FirstName, "Kurt");
            Assert.AreEqual(searchNameContainsInLastName.First<Person>().LastName, "Leach");
            Assert.AreEqual(searchNameContainsInBoth.Count, 3);
            Assert.AreEqual(searchNameContainsWithNoRecord.Count, 0);
        }
    }
}
