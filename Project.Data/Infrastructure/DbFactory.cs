using System.Configuration;
namespace Project.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private ProjectEntities dbContext;

        public ProjectEntities Init()
        {
            return dbContext ?? (dbContext = new ProjectEntities());
        }

        public void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}