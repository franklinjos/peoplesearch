using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace PeopleSearch.Models
{
    [DataContract]
    public class Address
    {
        [DataMember]
        [Key]
        [ForeignKey("Person")]
        public int PersonAddressId { get; set; }

        [DataMember]
        [Required]
        public string AddressLine1 { get; set; }

        [DataMember]
        public string AddressLine2 { get; set; }

        [DataMember]
        [Required]
        public string City { get; set; }

        [DataMember]
        [Required]
        public string State { get; set; }

        [DataMember]
        [Required]
        public string ZipCode { get; set; }

        [DataMember]
        [Required]
        public string Country { get; set; }

        public virtual Person Person {get; set;}
    }
}