using System.Data.Entity.Migrations;

namespace Repo.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Context.EntityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Context.EntityContext context)
        {
            
        }
    }
}
