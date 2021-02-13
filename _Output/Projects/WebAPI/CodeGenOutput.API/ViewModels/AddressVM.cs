using System;
using System.Collections.Generic;

namespace CodeGenOutput.API.ViewModels
{
    public class AddressVM
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string BoxNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public Guid? AccountId { get; set; }
        public AccountVM Account { get; set; }
        public Guid? ContactId { get; set; }
        public ContactVM Contact { get; set; }

    }

    public class AddressListVM
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string BoxNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

        public Guid? AccountId { get; set; }
        public AccountListVM Account { get; set; }
        public Guid? ContactId { get; set; }
        public ContactListVM Contact { get; set; }

    }

    public class AddressCreateVM
    {
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string BoxNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

        public Guid? AccountId { get; set; }
        public AccountCreateVM Account { get; set; }
        public Guid? ContactId { get; set; }
        public ContactCreateVM Contact { get; set; }

    }

    public class AddressUpdateVM
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string BoxNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
    }
}
