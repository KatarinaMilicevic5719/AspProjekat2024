using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asp.DataAccess.Configuration
{
    public class ImageConfiguration : EntityConfiguration<Image>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Image> builder)
        {
            builder.Property(x => x.Path).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Path).HasMaxLength(50).IsRequired();
        }
    }
}
