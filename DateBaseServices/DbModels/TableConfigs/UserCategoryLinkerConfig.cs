namespace DateBaseServices.Models.TableConfigs 
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserCategoryLinkerConfig : IEntityTypeConfiguration<UserCategoryLinker>
    {
        public void Configure(EntityTypeBuilder<UserCategoryLinker> builder)
        {
            builder.ToTable("UserCategoryLinker");
            builder.HasKey(p => p.LinkerId);
            
            builder.HasOne(p => p.Category)
                .WithMany(t => t.UserCategoryLinkers)
                .HasForeignKey(p => p.CategoryId)
                .HasPrincipalKey(t => t.CategoryId);
            
            builder.HasOne(p => p.User)
                .WithMany(t => t.UserCategoryLinkers)
                .HasForeignKey(p => p.UserId)
                .HasPrincipalKey(t => t.UserId);
            
            
        }
    }
}
