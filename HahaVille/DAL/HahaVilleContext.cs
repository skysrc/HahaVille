using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using HahaVille.Models;

namespace HahaVille.DAL
{
    public class HahaVilleContext : DbContext
    {
        public HahaVilleContext()
            : base("HahaVilleContext")
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Category> Category { get; set; }
        //public DbSet<Language> Languages { get; set; }
        public DbSet<LocaleStringResource> LocaleStringResources { get; set; }
        public DbSet<LocalizedProperty> LocalizedProperties { get; set; }
        public DbSet<User> Users { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.Entity<User>().ToTable("Users"); 
        }
    }
}