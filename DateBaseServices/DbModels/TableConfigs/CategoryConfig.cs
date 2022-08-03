using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DateBaseServices.DbModels.TableConfigs 
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(p => p.CategoryId);
            
            builder.HasMany(p => p.UserCategoryLinkers)
                .WithOne(t => t.Category)
                .HasForeignKey(p => p.CategoryId)
                .HasPrincipalKey(t => t.CategoryId);
            
            builder.HasMany(p => p.NoteCategoryLinkers)
                .WithOne(t => t.Category)
                .HasForeignKey(p => p.CategoryId)
                .HasPrincipalKey(t => t.CategoryId);
        }
    }
}
