using System;

namespace CodeGen.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CodeGenConfig Config { get; set; }
    }
}
