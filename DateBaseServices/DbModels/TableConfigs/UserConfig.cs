using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DateBaseServices.DbModels.TableConfigs 
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(p => p.UserId);
            
            builder.HasMany(p => p.UserCategoryLinkers)
                .WithOne(t => t.User)
                .HasForeignKey(p => p.UserId)
                .HasPrincipalKey(t => t.UserId);
        }
    }
}
