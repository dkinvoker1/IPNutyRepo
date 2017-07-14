namespace IPNuty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dodane_id_usuniete_imie_rejestracja : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SingerId", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            DropColumn("dbo.AspNetUsers", "SingerId");
        }
    }
}
