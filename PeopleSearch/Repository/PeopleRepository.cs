using PeopleSearch.EntityFramework;
using PeopleSearch.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace PeopleSearch.Repository
{
    public class PeopleRepository : IPeopleRepository
    {
        const string ADDFAILED_MSG = "Add Person '{0}' with Id '{1}' failed";
        const string UPDATEFAILED_MSG = "Update Person '{0}' with Id '{1}' failed";

        private IPeopleContext dataContext;
        private readonly IDbSet<Person> personDbSet;
        private readonly IDbSet<Address> addressDbSet;

        public PeopleRepository(IPeopleContext peopleContext)
        {
            dataContext = peopleContext;
            personDbSet = dataContext.People;
            addressDbSet = dataContext.Address;

        }

        /// <summary>
        /// Add person to db
        /// </summary>
        /// <param name="person">Object of person to add</param>
        public void Add(Person person)
        {
            try
            {
                personDbSet.Add(person);
                addressDbSet.Add(person.Address);
                dataContext.MarkAsAdded(person);
                int iresult = dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format(ADDFAILED_MSG, person.LastName + " " + person.FirstName, person.PersonId), ex);
            }
        }

        /// <summary>
        /// Udpate person to db
        /// </summary>
        /// <param name="person">Object of person to udpate</param>
        public void Update(Person person)
        {
            dataContext.MarkAsModified(person);

            try
            {
                dataContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!PersonExists(person.PersonId))
                {
                    throw new RecordNotFoundException(person.PersonId);
                }

                throw new ApplicationException(String.Format(UPDATEFAILED_MSG, person.LastName + " " + person.FirstName, person.PersonId), ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format(UPDATEFAILED_MSG, person.LastName + " " + person.FirstName, person.PersonId), ex);
            }
        }

        /// <summary>
        /// Get Person by PersonId
        /// </summary>
        /// <param name="id">Id of the person</param>
        /// <returns></returns>
        public Person GetById(int id)
        {
            var person = personDbSet.Find(id);
            if (person != null)
            {
                person.Address = addressDbSet.Find(person.PersonId);
            }
            return person;
        }

        /// <summary>
        /// Get all person
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Person> GetAll()
        {
            var people = personDbSet.AsNoTracking().ToList();
            foreach (var person in people)
            {
                person.Address = addressDbSet.Find(person.PersonId);
            }

            return people;
        }

        /// <summary>
        /// Get all matching person
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IEnumerable<Person> Search(PeopleSearchOptions searchOptions)
        {
            IEnumerable<Person> people = null;

            // If Name is empty return all people
            if (string.IsNullOrEmpty(searchOptions.Name))
            {
                people = GetAll();
            }
            else
            {
                // Expression to do case insensitive search for SearchOptions.Name in Person.FirstName and Person.LastName
                Func<Person, bool> containsName = p => p.FirstName.ToLower().Contains(searchOptions.Name.ToLower()) || p.LastName.ToLower().Contains(searchOptions.Name.ToLower());
                people = personDbSet.Where(containsName).ToList();
            }

            foreach (var person in people)
            {
                person.Address = addressDbSet.Find(person.PersonId);
            }

            return people;
        }

        /// <summary>
        /// Delete person using PersonId
        /// </summary>
        /// <param name="id">Id of the person</param>
        /// <returns></returns>
        public Person Delete(int id)
        {
            Person person = personDbSet.Find(id);
            if (person == null)
            {
                throw new RecordNotFoundException(person.PersonId);
            }

            personDbSet.Remove(person);
            dataContext.SaveChanges();
            return person;
        }

       public void Dispose()
        {
            if (dataContext != null) dataContext.Dispose();
        }

        private bool PersonExists(int id)
        {
            return personDbSet.Count(e => e.PersonId == id) > 0;
        }
    }
}