namespace IPNuty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Zmiana_Id_singera_w_rejestracji : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SingerId_SingerId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "SingerId_SingerId");
            AddForeignKey("dbo.AspNetUsers", "SingerId_SingerId", "dbo.Singers", "SingerId");
            DropColumn("dbo.AspNetUsers", "SingerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "SingerId", c => c.Int(nullable: false));
            DropForeignKey("dbo.AspNetUsers", "SingerId_SingerId", "dbo.Singers");
            DropIndex("dbo.AspNetUsers", new[] { "SingerId_SingerId" });
            DropColumn("dbo.AspNetUsers", "SingerId_SingerId");
        }
    }
}
