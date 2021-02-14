using System;
using System.ComponentModel.DataAnnotations;

namespace CodeGenOutput.API.Models
{
    public class Address : Auditable
    {
        public Address()
        {
        }

        [Key]
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string BoxNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }


    }
}
