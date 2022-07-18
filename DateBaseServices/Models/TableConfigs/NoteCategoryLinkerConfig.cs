namespace DateBaseServices.Models.TableConfigs 
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class NoteCategoryLinkerConfig : IEntityTypeConfiguration<NoteCategoryLinker>
    {
        public void Configure(EntityTypeBuilder<NoteCategoryLinker> builder)
        {
            builder.ToTable("NoteCategoryLinker");
            builder.HasKey(p => p.LinkerId);
            
            builder.HasOne(p => p.Category)
                .WithMany(t => t.NoteCategoryLinkers)
                .HasForeignKey(p => p.CategoryId)
                .HasPrincipalKey(t => t.CategoryId);
            
            builder.HasOne(p => p.Note)
                .WithMany(t => t.NoteCategoryLinkers)
                .HasForeignKey(p => p.NoteId)
                .HasPrincipalKey(t => t.NoteId);
        }
    }
}
