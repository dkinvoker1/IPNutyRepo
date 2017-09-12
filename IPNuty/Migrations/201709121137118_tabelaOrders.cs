namespace IPNuty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tabelaOrders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                {
                    OrderId = c.Int(nullable: false, identity: true),
                    OrderTime = c.DateTime(nullable: false),
                    Completed = c.Boolean(nullable: false),
                    SheetMusic_SheetMusicId = c.Int(),
                    Singer_SingerId = c.Int(),
                })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Singers", t => t.Singer_SingerId)
                .ForeignKey("dbo.SheetMusics", t => t.SheetMusic_SheetMusicId)
                .Index(t => t.Singer_SingerId)
                .Index(t => t.SheetMusic_SheetMusicId);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Singer_SingerId", "dbo.Singers");
            DropForeignKey("dbo.Orders", "SheetMusic_SheetMusicId", "dbo.SheetMusics");
            DropIndex("dbo.Orders", new[] { "Singer_SingerId" });
            DropIndex("dbo.Orders", new[] { "SheetMusic_SheetMusicId" });
            DropColumn("dbo.Orders", "Singer_SingerId");
            DropColumn("dbo.Orders", "SheetMusic_SheetMusicId");
            DropColumn("dbo.Orders", "Completed");
            DropColumn("dbo.Orders", "OrderTime");
        }
    }
}
