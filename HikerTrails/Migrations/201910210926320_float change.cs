namespace HikerTrails.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class floatchange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Hikes", "StartLongitude", c => c.Single(nullable: false));
            AlterColumn("dbo.Hikes", "StartLatitude", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Hikes", "StartLatitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Hikes", "StartLongitude", c => c.Double(nullable: false));
        }
    }
}
