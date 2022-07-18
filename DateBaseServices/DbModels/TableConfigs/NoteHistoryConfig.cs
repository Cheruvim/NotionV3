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
            
            builder.HasOne(p => p.User)
                .WithMany(t => t.NoteHistories)
                .HasForeignKey(p => p.UserId)
                .HasPrincipalKey(t => t.UserId);
            
            builder.HasOne(p => p.Note)
                .WithMany(t => t.NoteHistories)
                .HasForeignKey(p => p.NoteId)
                .HasPrincipalKey(t => t.NoteId);
        }
    }
}
