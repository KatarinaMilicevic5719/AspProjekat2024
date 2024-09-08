using Asp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp.DataAccess.Configuration
{
    internal class ErrorLogConfiguration : IEntityTypeConfiguration<ErrorLog>
    {
        public void Configure(EntityTypeBuilder<ErrorLog> builder)
        {
            builder.Property(x => x.Message).IsRequired().HasMaxLength(500);
            builder.Property(x => x.StrackTrace).IsRequired().HasMaxLength(2000);
            builder.HasKey(x => x.ErrorId);
        }
    }
}
