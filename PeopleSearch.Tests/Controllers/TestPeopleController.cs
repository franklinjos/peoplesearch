using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PeopleSearch.Controllers;
using PeopleSearch.Models;
using PeopleSearch.Repository;
using PeopleSearch.Tests.TestData;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;

namespace PeopleSearch.Tests.Controllers
{
    /// <summary>
    /// Unit test for PeopleController
    /// </summary>
    [TestClass]
    public class TestPeopleController
    {
        private PeopleController _controller;
        private readonly Mock<IPeopleRepository> _repository;

        public TestPeopleController()
        {
            _repository = new Mock<IPeopleRepository>();
        }

        [TestMethod]
        public void ShouldReturnPeople()
        {
            // Arrange
            _controller = new PeopleController(_repository.Object);
            var people = Data.GetPeople();
            _repository.Setup(r => r.GetAll()).Returns(people);

            //Act;
            var result = _controller.GetPeople().ToList();

            //Assert
            Assert.AreEqual(result.Count, people.Count);
        }

        [TestMethod]
        public void ShouldAddPerson()
        {
            // Arrange
            _controller = new PeopleController(_repository.Object);
            var person = new Person() { FirstName="TestF", LastName="Testl", Address= new Address() {AddressLine1="123 street" } };

            //Act;
            IHttpActionResult actionResult  = _controller.PostPerson(person);
            
            //Assert
            Assert.AreEqual(((CreatedAtRouteNegotiatedContentResult<Person>)actionResult).Content, person);

        }

        [TestMethod]
        public void ShouldReturnPersonById()
        {
            // Arrange
            _controller = new PeopleController(_repository.Object);
            var people = Data.GetPeople();
            _repository.Setup(r => r.GetById(1)).Returns(people.First);

            //Act;
            var actionResult = _controller.GetPerson(1);
            
            //Assert
            Assert.AreEqual(((OkNegotiatedContentResult<Person>)actionResult).Content, people.First<Person>());
        }

        [TestMethod]
        public void ShouldReturnNotFoundIfPersonWithIdNotExist()
        {
            // Arrange
            _controller = new PeopleController(_repository.Object);
            Person person = null;
            _repository.Setup(r => r.GetById(1)).Returns(person);

            //Act;
            var actionResult = _controller.GetPerson(1);

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void ShouldReturnSearchResult()
        {
            // Arrange
            _controller = new PeopleController(_repository.Object);
            var people = Data.GetPeople();
            _repository.Setup(r => r.Search(new PeopleSearchOptions() { Name = "jo"})).Returns(people);

            //Act;
            var result = _controller.Search("jo");

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<IEnumerable<Person>>));
        }

        [TestMethod]
        public void ShouldDeletePersonById()
        {
            // Arrange
            _controller = new PeopleController(_repository.Object);
            var people = Data.GetPeople();
            _repository.Setup(r => r.Delete(1)).Returns(people.First<Person>);

            //Act;
            var result = _controller.DeletePerson(1);

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Person>));
        }

        [TestMethod]
        public void ShouldReturnNotFoundIfPersonWithIdNotExistToDelete()
        {
            // Arrange
            _controller = new PeopleController(_repository.Object);
            var people = Data.GetPeople();
            _repository.Setup(r => r.Delete(23)).Throws(new RecordNotFoundException(22));

            //Act;
            var result = _controller.DeletePerson(23);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
