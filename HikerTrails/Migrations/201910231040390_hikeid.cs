namespace HikerTrails.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hikeid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HikerTripDetails", "Hike_Id", "dbo.Hikes");
            DropIndex("dbo.HikerTripDetails", new[] { "Hike_Id" });
            AddColumn("dbo.HikerTripDetails", "HikeId", c => c.Int(nullable: false));
            DropColumn("dbo.HikerTripDetails", "Hike_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HikerTripDetails", "Hike_Id", c => c.Int());
            DropColumn("dbo.HikerTripDetails", "HikeId");
            CreateIndex("dbo.HikerTripDetails", "Hike_Id");
            AddForeignKey("dbo.HikerTripDetails", "Hike_Id", "dbo.Hikes", "Id");
        }
    }
}
