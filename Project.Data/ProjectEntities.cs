using Microsoft.AspNet.Identity.EntityFramework;
using Project.Data.Configuration;
using Project.Model;
using System.Data.Entity;
using System.Configuration;

namespace Project.Data
{
    public class ProjectEntities : DbContext
    {
        public ProjectEntities()
            : base()
        {
            Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["WebApiCon"].ToString();
            Configuration.LazyLoadingEnabled = false;
        }

        static ProjectEntities()
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new AccountConfiguration());
            modelBuilder.Configurations.Add(new PaymentConfiguration());
            modelBuilder.Configurations.Add(new UserTokenConfiguration());
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<USER> USERS { get; set; }
        public DbSet<USER_TOKEN> USER_TOKENS { get; set; }
        public DbSet<ACCOUNT> ACCOUNTS { get; set; }
        public DbSet<PAYMENT> PAYMENTS { get; set; }
        public virtual void Commit()
        {
            base.SaveChanges();
        }

        public static ProjectEntities Create()
        {
            return new ProjectEntities();
        }
    }
}