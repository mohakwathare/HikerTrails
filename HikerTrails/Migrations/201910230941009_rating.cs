namespace HikerTrails.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HikerTripDetails", "UserRating", c => c.Single(nullable: false));
            AddColumn("dbo.Hikes", "IsHikeCompleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.HikerTripDetails", "IsHikeCompleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HikerTripDetails", "IsHikeCompleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.Hikes", "IsHikeCompleted");
            DropColumn("dbo.HikerTripDetails", "UserRating");
        }
    }
}
