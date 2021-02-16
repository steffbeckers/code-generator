using System.Collections.Generic;

namespace CodeGen.Models
{
    public class CodeGenModelRelations
    {
        public CodeGenModelRelations()
        {
            this.OneToMany = new List<CodeGenModelRelation>();
            this.ManyToOne = new List<CodeGenModelRelation>();
        }

        public List<CodeGenModelRelation> OneToMany { get; set; }
        public List<CodeGenModelRelation> ManyToOne { get; set; }
    }
}