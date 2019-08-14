using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PeopleSearch.EntityFramework;
using PeopleSearch.Models;
using PeopleSearch.Repository;

namespace PeopleSearch.Controllers
{
    

    public class PeopleController : ApiController
    {
        private IPeopleRepository _repository;

        public PeopleController(IPeopleRepository peopleRepo ) : base()
        {
            _repository = peopleRepo;
        }


        // GET: api/People
        [HttpGet]
        public IEnumerable<Person> GetPeople()
        {
            return _repository.GetAll();
        }

        // GET: api/People/
        [HttpGet]
        [ActionName("Search")]
        [ResponseType(typeof(IEnumerable<Person>))]
        public IHttpActionResult Search(string name)
        {
            return Ok(_repository.Search(new PeopleSearchOptions() { Name = name }));
        }

        // GET: api/People/5
        [HttpGet]
        [ResponseType(typeof(Person))]
        public IHttpActionResult GetPerson(int id)
        {
            Person person = _repository.GetById(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // POST: api/People
        [HttpPost]
        [ResponseType(typeof(Person))]
        public IHttpActionResult PostPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(person);

            return CreatedAtRoute("DefaultApi", new { id = person.PersonId }, person);
        }

        // DELETE: api/People/5
        [HttpDelete]
        [ResponseType(typeof(Person))]
        public IHttpActionResult DeletePerson(int id)
        {
            Person person;
            try
            {
                person = _repository.Delete(id);
            }
            catch (RecordNotFoundException)
            {
                return NotFound();
            }

            return Ok(person);
        }

      

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}