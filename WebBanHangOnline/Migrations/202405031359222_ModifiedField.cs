namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_ProductCategory", "Title", c => c.String(nullable: false, maxLength: 150));
            DropColumn("dbo.tb_ProductCategory", "Tile");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_ProductCategory", "Tile", c => c.String(nullable: false, maxLength: 150));
            DropColumn("dbo.tb_ProductCategory", "Title");
        }
    }
}
