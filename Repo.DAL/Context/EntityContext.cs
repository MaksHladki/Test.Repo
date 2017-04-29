using System.Data.Entity;
using Repo.Model.Models;
using System.Data.Common;

namespace Repo.DAL.Context
{
    public class EntityContext : DbContext, IEntityContext
    {
        private const string ConnectionStingName = "Entities";

        #region Constructors

        public EntityContext() : base(ConnectionStingName)
        {

        }

        public EntityContext(DbConnection connection) : base(connection, true)
        {

        }

        #endregion

        #region Properties

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Message> Messages { get; set; }

        #endregion

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasRequired(p => p.UserTo)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasRequired(p => p.UserFrom)
                .WithMany()
                .WillCascadeOnDelete(false);
        }

        #endregion
    }
}
