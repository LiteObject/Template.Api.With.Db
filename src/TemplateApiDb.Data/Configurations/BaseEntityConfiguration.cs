using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateApiDb.Domain.Entities;

namespace TemplateApiDb.Data.Configurations
{
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            string defaultBy = "System";

            // *.HasDefaultValueSql(Database.IsSqlite() ? "datetime('now', 'utc')" : "getutcdate()")

            _ = builder?.Property(p => p.Created).ValueGeneratedOnAdd().HasDefaultValueSql("datetime()");
            _ = builder?.Property(p => p.CreatedBy).ValueGeneratedOnAdd().HasDefaultValue(defaultBy);

            _ = builder?.Property(p => p.Updated).ValueGeneratedOnUpdate().HasDefaultValueSql("datetime()");
            _ = builder?.Property(p => p.UpdatedBy).ValueGeneratedOnUpdate().HasDefaultValue(defaultBy);
        }
    }
}
