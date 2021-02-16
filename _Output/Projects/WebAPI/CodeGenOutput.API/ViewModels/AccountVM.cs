using System;
using System.Collections.Generic;

namespace CodeGenOutput.API.ViewModels
{
    public class AccountVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string VAT { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public Guid? AddressId { get; set; }
        public AddressVM Address { get; set; }

        public List<AccountContactVM> Contacts { get; set; }
    }

    public class AccountListVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string VAT { get; set; }

        public Guid? AddressId { get; set; }
        public AddressListVM Address { get; set; }

        public List<AccountContactListVM> Contacts { get; set; }
    }

    public class AccountCreateVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string VAT { get; set; }

        public Guid? AddressId { get; set; }
        public AddressCreateVM Address { get; set; }

        public List<AccountContactCreateVM> Contacts { get; set; }
    }

    public class AccountUpdateVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string VAT { get; set; }
    }
}
