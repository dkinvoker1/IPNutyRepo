namespace IPNuty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSingerIDtoSheetMusic : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SheetMusics", name: "Singer_SingerId", newName: "SingerID_SingerId");
            RenameIndex(table: "dbo.SheetMusics", name: "IX_Singer_SingerId", newName: "IX_SingerID_SingerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.SheetMusics", name: "IX_SingerID_SingerId", newName: "IX_Singer_SingerId");
            RenameColumn(table: "dbo.SheetMusics", name: "SingerID_SingerId", newName: "Singer_SingerId");
        }
    }
}
