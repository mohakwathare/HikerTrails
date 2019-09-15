namespace HikerTrails.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class participants : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hikes", "MaximumParticipants", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hikes", "MaximumParticipants");
        }
    }
}
