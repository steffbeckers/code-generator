using System;

namespace CodeGenOutput.ViewModels
{
    public class ContactVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
    }

    public class ContactListVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
    }

    public class ContactCreateVM
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
    }
}
