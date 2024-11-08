using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateApiDb.Domain.Entities;
using TemplateApiDb.Domain.ValueObjects;

namespace TemplateApiDb.Data.Configurations
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            ArgumentNullException.ThrowIfNull(builder);
            _ = builder.HasKey(x => x.Id);

            // Value objects persist as owned entity
            _ = builder.OwnsOne(x => x.Username);
            _ = builder.OwnsOne(x => x.FirstName);
            _ = builder.OwnsOne(x => x.LastName);
            _ = builder.OwnsOne(x => x.PhoneNumber);
            _ = builder.OwnsOne(x => x.Email);
            // builder.Property(p => p.Email).HasDefaultValue("");

            //_ = builder.Property(x => x.Username).HasMaxLength(50).IsRequired();
            //_ = builder.Property(x => x.FirstName).HasMaxLength(50);
            //_ = builder.Property(x => x.LastName).HasMaxLength(50);
            //_ = builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
            //_ = builder.Property(x => x.PhoneNumber).HasMaxLength(50);

            base.Configure(builder);
        }
    }
}
