using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeDoVerde.Domain.Post;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedeDoVerde.Repository.Mapping
{
    public class PostMap : IEntityTypeConfiguration<Domain.Post.Post>
    {
        public void Configure(EntityTypeBuilder<Domain.Post.Post> builder)
        {
            builder.ToTable("Post");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Content).IsRequired().HasMaxLength(250);
            builder.Property(x => x.ImagePost).HasMaxLength(250);

            builder.HasMany(x => x.Comments).WithOne(c => c.Post);
            builder.HasOne(x => x.Account).WithMany(x => x.Posts);
        }
    }
}
