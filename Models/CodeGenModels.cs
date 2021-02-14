using System.Collections.Generic;
using System.Linq;

namespace CodeGen.Models
{
    public class CodeGenModels
    {
        public CodeGenModels()
        {
            List = new List<CodeGenModel>();
        }

        public int? StringPropertyMaxLength { get; set; }
        public CodeGenModel Defaults { get; set; }
        public List<CodeGenModel> List { get; set; }

        // Helpers
        public CodeGenModelProperty DefaultKey(CodeGenModel model = null) {
            if (model != null) {
                CodeGenModelProperty modelKey = model.Properties.Where(x => x.Key).FirstOrDefault();
                if (modelKey != null) {
                    return modelKey;
                }
            }

            return Defaults.Properties.Where(x => x.Key).FirstOrDefault() ?? new CodeGenModelProperty() {
                Name = "Id",
                Type = "Guid",
                Key = true
            };
        }

        public List<CodeGenModelProperty> DefaultAuditProperties() {
            return Defaults.Properties.Where(x =>
                x.AuditCreatedBy ||
                x.AuditDateCreated ||
                x.AuditModifiedBy ||
                x.AuditDateModified ||
                x.AuditDeleted
            ).ToList();
        }

        public CodeGenModelProperty DefaultAuditDateCreatedProperty() {
            return Defaults.Properties.Where(x => x.AuditDateCreated).SingleOrDefault();
            // ?? new CodeGenModelProperty() {
            //     Name = "DateCreated",
            //     Type = "DateTime",
            //     AuditDateCreated = true
            // };
        }

        public CodeGenModelProperty DefaultAuditCreatedByProperty() {
            return Defaults.Properties.Where(x => x.AuditCreatedBy).SingleOrDefault();
            // ?? new CodeGenModelProperty() {
            //     Name = "CreatedBy",
            //     Type = "Guid",
            //     AuditCreatedBy = true
            // };
        }

        public CodeGenModelProperty DefaultAuditDateModifiedProperty() {
            return Defaults.Properties.Where(x => x.AuditDateModified).SingleOrDefault();
            // ?? new CodeGenModelProperty() {
            //     Name = "DateModified",
            //     Type = "DateTime?",
            //     AuditDateModified = true
            // };
        }

        public CodeGenModelProperty DefaultAuditModifiedByProperty() {
            return Defaults.Properties.Where(x => x.AuditModifiedBy).SingleOrDefault();
            // ?? new CodeGenModelProperty() {
            //     Name = "ModifiedBy",
            //     Type = "Guid?",
            //     AuditModifiedBy = true
            // };
        }

        public CodeGenModelProperty DefaultAuditDeletedProperty() {
            return Defaults.Properties.Where(x => x.AuditDeleted).SingleOrDefault();
            // ?? new CodeGenModelProperty() {
            //     Name = "Deleted",
            //     Type = "boolean",
            //     AuditDeleted = true
            // };
        }
    }
}
