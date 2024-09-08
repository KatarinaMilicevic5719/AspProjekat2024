using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asp.DataAccess.Configuration
{
    public class MessageConfiguration : EntityConfiguration<Message>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Message> builder)
        {
            builder.Property(x => x.Context).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Seen).HasDefaultValue(false);

            builder.HasOne(x => x.FromUser)
                   .WithMany(x => x.SentMessages)
                   .HasForeignKey(x => x.FromUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ToUser)
                   .WithMany(x => x.RecivedMessages)
                   .HasForeignKey(x => x.ToUserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
