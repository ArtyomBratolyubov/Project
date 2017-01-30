namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Comments", newName: "CommentSongs");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.CommentSongs", newName: "Comments");
        }
    }
}
