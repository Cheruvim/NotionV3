using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DateBaseServices.DbModels.TableConfigs 
{
    public class NoteConfig : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.ToTable("Notes");
            builder.HasKey(p => p.NoteId);
            
            builder.HasMany(p => p.NoteCategoryLinkers)
                .WithOne(t => t.Note)
                .HasForeignKey(p => p.NoteId)
                .HasPrincipalKey(t => t.NoteId);
            
            builder.HasMany(p => p.NoteHistories)
                .WithOne(t => t.Note)
                .HasForeignKey(p => p.NoteId)
                .HasPrincipalKey(t => t.NoteId);
        }
    }
}
