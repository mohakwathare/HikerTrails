namespace HikerTrails.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TripsDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HikerTripDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        IsHikeCompleted = c.Boolean(nullable: false),
                        Hike_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hikes", t => t.Hike_Id)
                .Index(t => t.Hike_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HikerTripDetails", "Hike_Id", "dbo.Hikes");
            DropIndex("dbo.HikerTripDetails", new[] { "Hike_Id" });
            DropTable("dbo.HikerTripDetails");
        }
    }
}
