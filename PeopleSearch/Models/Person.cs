using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace PeopleSearch.Models
{
    [DataContract]
    public class Person
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonId { get; set; }

        [DataMember]
        [Required]
        public string FirstName { get; set; }

        [DataMember]
        [Required]
        public string LastName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        [Required]
        public DateTime DateOfBirth { get; set; }

        [DataMember]
        public byte[] Picture { get; set; }

        [DataMember]
        public string Interests { get; set; }

        [DataMember]
        public virtual Address Address { get; set; }
    }
}