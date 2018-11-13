namespace NiceNet.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.XYFDepartments",
                c => new
                    {
                        XYFDepartmentId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.XYFDepartmentId);
            
            CreateTable(
                "dbo.XYFUsers",
                c => new
                    {
                        XYFUserId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.XYFUserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.XYFUsers");
            DropTable("dbo.XYFDepartments");
        }
    }
}
