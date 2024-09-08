using Asp.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp.DataAccess.Configuration
{
    internal class UseCaseLogsConfiguration : IEntityTypeConfiguration<UseCaseLog>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UseCaseLog> builder)
        {
            builder.Property(x => x.UseCaseName).HasMaxLength(500);
            builder.Property(x => x.User).HasMaxLength(200);
            builder.Property(x => x.Data).HasMaxLength(2000);
            builder.HasIndex(x => x.UseCaseName);

        }
    }
}
