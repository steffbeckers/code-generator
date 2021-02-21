using CodeGen.Models;
using System;

namespace CodeGen.API.ViewModels
{
    public class ProjectVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CodeGenConfig Config { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }

    public class ProjectListVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ProjectCreateVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public CodeGenConfig Config { get; set; }
    }

    public class ProjectUpdateVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CodeGenConfig Config { get; set; }
    }
}
