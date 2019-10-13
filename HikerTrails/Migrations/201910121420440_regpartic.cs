namespace HikerTrails.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class regpartic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hikes", "RegisteredParticipants", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hikes", "RegisteredParticipants");
        }
    }
}
