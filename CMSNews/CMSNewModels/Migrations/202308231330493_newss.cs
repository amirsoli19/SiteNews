namespace CMSNewModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newss : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.T_News", "NewsTitle", c => c.String());
            AlterColumn("dbo.T_News", "Description", c => c.String());
            AlterColumn("dbo.T_News", "ImageName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.T_News", "ImageName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.T_News", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.T_News", "NewsTitle", c => c.String(nullable: false, maxLength: 300));
        }
    }
}
