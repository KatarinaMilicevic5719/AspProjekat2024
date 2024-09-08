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
    public class CommentConfiguration : EntityConfiguration<Comment>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Context).HasMaxLength(150).IsRequired();

            builder.HasOne(x => x.ParentComment)
                   .WithMany(x => x.ChildComments)
                   .HasForeignKey(x => x.ParentId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Post)
                   .WithMany(x => x.PostComments)
                   .HasForeignKey(x => x.PostId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                   .WithMany()
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
