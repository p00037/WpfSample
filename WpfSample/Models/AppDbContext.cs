using Microsoft.EntityFrameworkCore;
using WpfSample.Models.DBEntity;
using System.Configuration;

namespace WpfSample.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<M_AccountEntity> M_AccountEntitys { get; set; }

        public DbSet<M_事業所Entity> M_事業所Entitys { get; set; }

        public DbSet<M_事業所明細Entity> M_事業所明細Entitys { get; set; }

        public DbSet<M_サービス種類Entity> M_サービス種類Entitys { get; set; }

        public DbSet<V_状態区分Entity> V_状態区分Entitys { get; set; }

        public DbSet<V_障害者種別Entity> V_障害者種別Entitys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<M_AccountEntity>().ToTable("M_Account");
            modelBuilder.Entity<M_事業所Entity>().ToTable("M_事業所").HasKey(c => new { c.事業所番号 });
            modelBuilder.Entity<M_事業所明細Entity>().ToTable("M_事業所明細").HasKey(c => new { c.事業所番号, c.連番 });
            modelBuilder.Entity<V_状態区分Entity>().ToTable("V_状態区分");
            modelBuilder.Entity<V_障害者種別Entity>().ToTable("V_障害者種別");
            modelBuilder.Entity<M_サービス種類Entity>().ToTable("M_サービス種類").HasKey(c => new { c.障害者種別, c.サービス種類CD });
        }
    }
}
