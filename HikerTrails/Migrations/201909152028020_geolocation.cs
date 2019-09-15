namespace HikerTrails.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class geolocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hikes", "StartLongitude", c => c.Double(nullable: false));
            AddColumn("dbo.Hikes", "StartLatitude", c => c.Double(nullable: false));
            AddColumn("dbo.Hikes", "HikeRating", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hikes", "HikeRating");
            DropColumn("dbo.Hikes", "StartLatitude");
            DropColumn("dbo.Hikes", "StartLongitude");
        }
    }
}
