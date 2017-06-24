namespace IPNuty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Singerzy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SheetMusics",
                c => new
                    {
                        SheetMusicId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Author = c.String(),
                        Type = c.Int(nullable: false),
                        Singer_SingerId = c.Int(),
                    })
                .PrimaryKey(t => t.SheetMusicId)
                .ForeignKey("dbo.Singers", t => t.Singer_SingerId)
                .Index(t => t.Singer_SingerId);
            
            CreateTable(
                "dbo.Singers",
                c => new
                    {
                        SingerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastName = c.String(),
                        Activicity = c.Boolean(nullable: false),
                        JoiningDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SingerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SheetMusics", "Singer_SingerId", "dbo.Singers");
            DropIndex("dbo.SheetMusics", new[] { "Singer_SingerId" });
            DropTable("dbo.Singers");
            DropTable("dbo.SheetMusics");
        }
    }
}
