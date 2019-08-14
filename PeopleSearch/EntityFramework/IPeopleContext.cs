using PeopleSearch.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PeopleSearch.EntityFramework
{
    public interface IPeopleContext : IDisposable
    {
        DbSet<Person> People { get; }
        DbSet<Address> Address { get; }
        int SaveChanges();
        void MarkAsModified(Person item);
        void MarkAsAdded(Person item);
    }
}