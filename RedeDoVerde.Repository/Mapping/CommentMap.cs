using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeDoVerde.Domain.Comment;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedeDoVerde.Repository.Mapping
{
    class CommentMap : IEntityTypeConfiguration<Comments>
    {
        public void Configure(EntityTypeBuilder<Comments> builder)
        {
            builder.ToTable("Comment");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Content).IsRequired().HasMaxLength(250);

            builder.HasOne(x => x.Account).WithMany(x => x.Comments);
            builder.HasOne(x => x.Post).WithMany(x => x.Comments);
        }
    }
}
