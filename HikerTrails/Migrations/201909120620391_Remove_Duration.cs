namespace HikerTrails.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_Duration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Hikes", "Duration");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Hikes", "Duration", c => c.DateTime(nullable: false));
        }
    }
}
