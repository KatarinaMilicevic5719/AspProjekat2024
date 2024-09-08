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
    public class UserConfiguration : EntityConfiguration<User>
    {
        protected override void ConfigureRules(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(30).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(35).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(255).IsRequired();

            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.FirstName);
            builder.HasIndex(x => x.LastName);

            
            builder.HasMany(x => x.UserPosts)
                   .WithOne(x => x.User)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.UserLikes)
                   .WithOne(x => x.User)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.AcceptedFriends)
                   .WithOne(x => x.AcceptUser)
                   .HasForeignKey(x => x.AcceptUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.RequestedFriends)
                   .WithOne(x => x.RequestUser)
                   .HasForeignKey(x => x.RequestUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.UseCases)
                   .WithOne(x => x.User)
                   .HasForeignKey(x => x.UserId);
        }
    }
}
