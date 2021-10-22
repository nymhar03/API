namespace Project.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gdgdhg : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CONTACT", newName: "CONTACTS");
            RenameTable(name: "dbo.SUBSCRIBER", newName: "SUBSCRIBERS");
            RenameTable(name: "dbo.INCIDENT", newName: "INCIDENTS");
            RenameTable(name: "dbo.REQUEST", newName: "REQUESTS");
            RenameTable(name: "dbo.USER_MSTR", newName: "USERS");
            RenameTable(name: "dbo.TEXTMESSAGE", newName: "MESSAGES");
            AddColumn("dbo.USERS", "PHONE_NO", c => c.String(maxLength: 100));
            AddColumn("dbo.USERS", "ISVERIFIED", c => c.Boolean(nullable: false));
            AddColumn("dbo.USERS", "ATTEMPS", c => c.Int(nullable: false));
            AddColumn("dbo.USERS", "VERIFICATION_CODE", c => c.String());
            DropColumn("dbo.USERS", "SEX");
            DropColumn("dbo.USERS", "CONTACT_NO");
        }
        
        public override void Down()
        {
            AddColumn("dbo.USERS", "CONTACT_NO", c => c.String(maxLength: 100));
            AddColumn("dbo.USERS", "SEX", c => c.String(maxLength: 10));
            DropColumn("dbo.USERS", "VERIFICATION_CODE");
            DropColumn("dbo.USERS", "ATTEMPS");
            DropColumn("dbo.USERS", "ISVERIFIED");
            DropColumn("dbo.USERS", "PHONE_NO");
            RenameTable(name: "dbo.MESSAGES", newName: "TEXTMESSAGE");
            RenameTable(name: "dbo.USERS", newName: "USER_MSTR");
            RenameTable(name: "dbo.REQUESTS", newName: "REQUEST");
            RenameTable(name: "dbo.INCIDENTS", newName: "INCIDENT");
            RenameTable(name: "dbo.SUBSCRIBERS", newName: "SUBSCRIBER");
            RenameTable(name: "dbo.CONTACTS", newName: "CONTACT");
        }
    }
}
