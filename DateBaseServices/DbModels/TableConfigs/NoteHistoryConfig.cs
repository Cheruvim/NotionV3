namespace DateBaseServices.Models.TableConfigs 
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class NoteHistoryConfig : IEntityTypeConfiguration<NoteHistory>
    {
        public void Configure(EntityTypeBuilder<NoteHistory> builder)
        {
            builder.ToTable("NoteHistory");
            builder.HasKey(p => p.HistoryId);
            
            builder.HasOne(p => p.UserCategoryLinker)
                .WithMany(t => t.NoteHistories)
                .HasForeignKey(p => p.UserCategoryLinkerId)
                .HasPrincipalKey(t => t.LinkerId);
            
            builder.HasOne(p => p.Note)
                .WithMany(t => t.NoteHistories)
                .HasForeignKey(p => p.NoteId)
                .HasPrincipalKey(t => t.NoteId);
        }
    }
}
