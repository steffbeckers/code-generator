using System;
using System.ComponentModel.DataAnnotations;

namespace CodeGenOutput.API.Models
{
    public class Contact
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
    }
}
