using DateBaseServices.DbModels;
using DateBaseServices.DbModels.TableConfigs;
using DateBaseServices.Services;

namespace DateBaseServices
{
    using Microsoft.EntityFrameworkCore;

    public class DataContext : DbContext
    {
        internal DbSet<User> DbUsers { get; set; }
        internal DbSet<Category> DbCategories { get; set; }
        internal DbSet<Note> DbNotes { get; set; }
        internal DbSet<UserCategoryLinker> UserCategoryLinkers { get; set; }
        internal DbSet<NoteCategoryLinker> NoteCategoryLinkers { get; set; }
        internal DbSet<NoteHistory> NoteHistories { get; set; }
        internal DbSet<CategoryHistory> CategoryHistories { get; set; }

        public UserService Users { get; }
        public CategoryService Categories { get; }
        public NoteService Notes { get; }

        public DataContext()
        {
            Database.EnsureCreated();

            Users = new UserService(this);
            Categories = new CategoryService(this);
            Notes = new NoteService(this);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new CategoryConfig());
            modelBuilder.ApplyConfiguration(new NoteConfig());
            modelBuilder.ApplyConfiguration(new UserCategoryLinkerConfig());
            modelBuilder.ApplyConfiguration(new NoteCategoryLinkerConfig());
            modelBuilder.ApplyConfiguration(new NoteHistoryConfig());
            modelBuilder.ApplyConfiguration(new CategoryHistoryConfig());

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=NotionV3;User Id=root;Password=root; TrustServerCertificate=True;");
        }
    }

}