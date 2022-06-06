using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Data.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Book> entity)
        {
            entity.ToTable("Books");
            entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(450)
                .HasColumnName("description");

            entity.Property(e => e.PageCount).HasColumnName("pageCount");

            entity.Property(e => e.Excerpt)
                   .IsRequired()
                   .IsUnicode(false)
                   .HasColumnName("excerpt");

            entity.Property(e => e.PublishDate)
                .HasColumnType("datetime")
                .HasColumnName("publishDate");

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnName("title");
        }

    }
}
