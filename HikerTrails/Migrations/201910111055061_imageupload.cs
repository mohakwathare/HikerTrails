namespace HikerTrails.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imageupload : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hikes", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hikes", "ImagePath");
        }
    }
}
