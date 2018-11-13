namespace NiceNet.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adduserpassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.XYFUsers", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.XYFUsers", "Password");
        }
    }
}
