using PeopleSearch.Models;
using System.Data.Entity;

namespace PeopleSearch.EntityFramework
{
    public class PeopleContext: DbContext, IPeopleContext
    {
        public PeopleContext() : base("name=PeopleDBConnectionString")
        {
            Database.SetInitializer<PeopleContext>(new PeopleDbInitializer());
            this.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure a one-to-one relationship between Person and Address
            modelBuilder.Entity<Person>()
                .HasOptional(p => p.Address) // Mark Person.Address property optional (nullable).
                .WithRequired(ad => ad.Person).WillCascadeOnDelete(true); // Mark Address.Person property as required (NotNull).
                
            base.OnModelCreating(modelBuilder);
        }

        public void MarkAsModified(Person item)
        {
            Entry(item).State = EntityState.Modified;
        }

        public void MarkAsAdded(Person item)
        {
            Entry(item).State = EntityState.Added;
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Address> Address  { get; set; }
}
}