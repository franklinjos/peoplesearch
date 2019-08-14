using PeopleSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace PeopleSearch.Repository
{
    public interface IPeopleRepository : IDisposable
    {
        void Add(Person person);
        void Update(Person person);
        Person Delete(int Id);
        Person GetById(int Id);
        IEnumerable<Person> GetAll();
        IEnumerable<Person> Search(PeopleSearchOptions searchOptions);
    }
}