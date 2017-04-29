namespace Repo.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Label = c.String(nullable: false, maxLength: 128),
                        Body = c.String(nullable: false, maxLength: 2048),
                        Status = c.Int(nullable: false),
                        DateSent = c.DateTime(),
                        DateReceived = c.DateTime(),
                        UserFromId = c.Guid(nullable: false),
                        UserToId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        ModifiedAt = c.DateTime(),
                        ModifiedBy = c.String(maxLength: 128),
                        User_Id = c.Guid(),
                        User_Id1 = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .ForeignKey("dbo.User", t => t.User_Id1)
                .ForeignKey("dbo.User", t => t.UserFromId)
                .ForeignKey("dbo.User", t => t.UserToId)
                .Index(t => t.UserFromId)
                .Index(t => t.UserToId)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 64),
                        FirstName = c.String(nullable: false, maxLength: 64),
                        LastName = c.String(nullable: false, maxLength: 64),
                        IsLocked = c.Boolean(),
                        Salt = c.String(nullable: false, maxLength: 128),
                        Hash = c.String(nullable: false, maxLength: 1024),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        ModifiedAt = c.DateTime(),
                        ModifiedBy = c.String(maxLength: 128),
                        Role_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.Role_Id)
                .Index(t => t.Login, unique: true, name: "Index_User_Login")
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        ModifiedAt = c.DateTime(),
                        ModifiedBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "Index_Role_Name");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Message", "UserToId", "dbo.User");
            DropForeignKey("dbo.Message", "UserFromId", "dbo.User");
            DropForeignKey("dbo.User", "Role_Id", "dbo.Role");
            DropForeignKey("dbo.Message", "User_Id1", "dbo.User");
            DropForeignKey("dbo.Message", "User_Id", "dbo.User");
            DropIndex("dbo.Role", "Index_Role_Name");
            DropIndex("dbo.User", new[] { "Role_Id" });
            DropIndex("dbo.User", "Index_User_Login");
            DropIndex("dbo.Message", new[] { "User_Id1" });
            DropIndex("dbo.Message", new[] { "User_Id" });
            DropIndex("dbo.Message", new[] { "UserToId" });
            DropIndex("dbo.Message", new[] { "UserFromId" });
            DropTable("dbo.Role");
            DropTable("dbo.User");
            DropTable("dbo.Message");
        }
    }
}
