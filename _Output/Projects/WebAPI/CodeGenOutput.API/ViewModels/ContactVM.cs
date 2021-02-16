using System;
using System.Collections.Generic;

namespace CodeGenOutput.API.ViewModels
{
    public class ContactVM
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public Guid? AddressId { get; set; }
        public AddressVM Address { get; set; }

        public List<AccountContactVM> Account { get; set; }
    }

    public class ContactListVM
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }

        public Guid? AddressId { get; set; }
        public AddressListVM Address { get; set; }

        public List<AccountContactListVM> Account { get; set; }
    }

    public class ContactCreateVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }

        public Guid? AddressId { get; set; }
        public AddressCreateVM Address { get; set; }

        public List<AccountContactCreateVM> Account { get; set; }
    }

    public class ContactUpdateVM
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
    }
}
