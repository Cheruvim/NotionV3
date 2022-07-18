namespace DateBaseServices.Models.TableConfigs 
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CategoryHistoryConfig : IEntityTypeConfiguration<CategoryHistory>
    {
        public void Configure(EntityTypeBuilder<CategoryHistory> builder)
        {
            builder.ToTable("CategoryHistory");
            builder.HasKey(p => p.HistoryId);
            
            builder.HasOne(p => p.UserCategoryLinker)
                .WithMany(t => t.CategoryHistories)
                .HasForeignKey(p => p.UserCategoryLinkerId)
                .HasPrincipalKey(t => t.LinkerId);
        }
    }
}
