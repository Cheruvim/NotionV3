namespace DateBaseServices 
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.TableConfigs;

    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<UserCategoryLinker> UserCategoryLinkers { get; set; }
        public DbSet<NoteCategoryLinker> NoteCategoryLinkers { get; set; }
        public DbSet<NoteHistory> NoteHistories { get; set; }
        public DbSet<CategoryHistory> CategoryHistories { get; set; }
        
        public DataContext()
        {
            Database.EnsureCreated(); // создаем базу данных при первом обращении
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
