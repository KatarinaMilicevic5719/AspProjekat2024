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
    public class PostConfiguration : EntityConfiguration<Post>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Post> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(200).IsRequired();

            builder.HasIndex(x => x.Title);

            builder.HasOne(x => x.User)
                   .WithMany(x => x.UserPosts)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Image)
                   .WithMany()
                   .HasForeignKey(x => x.ImageId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.PostCategories)
                   .WithOne(x => x.Post)
                   .HasForeignKey(x => x.PostId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.PostLikes)
                   .WithOne(x => x.Post)
                   .HasForeignKey(x => x.PostId)
                   .OnDelete(DeleteBehavior.Cascade);
           
        }
    }
}
