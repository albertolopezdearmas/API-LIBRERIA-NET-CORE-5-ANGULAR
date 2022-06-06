using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Data.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Author> entity)
        {
            entity.ToTable("Authors");
            entity.Property(e => e.Id)
                     .ValueGeneratedNever()
                     .HasColumnName("id");

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnName("firstName");

            entity.Property(e => e.IdBook).HasColumnName("idBook");

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnName("lastName");

            entity.HasOne(d => d.IdBookNavigation)
                   .WithMany(p => p.Authors)
                   .HasForeignKey(d => d.IdBook)
                   .HasConstraintName("FK_Authors_Books");
        }

    }
}
