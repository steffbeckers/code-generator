using System;

namespace CodeGenOutput.API.ViewModels
{
    public class AccountContactVM
    {
        public Guid Id { get; set; }
        public bool IsPrimary { get; set; }
        public int? SortOrder { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public Guid AccountId { get; set; }
        public AccountVM Account { get; set; }
        public Guid ContactId { get; set; }
        public ContactVM Contact { get; set; }

    }

    public class AccountContactListVM
    {
        public Guid Id { get; set; }
        public bool IsPrimary { get; set; }
        public int? SortOrder { get; set; }

        public Guid AccountId { get; set; }
        public AccountListVM Account { get; set; }
        public Guid ContactId { get; set; }
        public ContactListVM Contact { get; set; }

    }

    public class AccountContactCreateVM
    {
        public bool IsPrimary { get; set; }
        public int? SortOrder { get; set; }

        public Guid AccountId { get; set; }
        public AccountCreateVM Account { get; set; }
        public Guid ContactId { get; set; }
        public ContactCreateVM Contact { get; set; }

    }

    public class AccountContactUpdateVM
    {
        public Guid Id { get; set; }
        public bool IsPrimary { get; set; }
        public int? SortOrder { get; set; }
    }
}
