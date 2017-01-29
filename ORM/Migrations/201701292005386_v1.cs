namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32),
                        DateOut = c.DateTime(precision: 7, storeType: "datetime2"),
                        Description = c.String(maxLength: 500),
                        GenreId = c.Int(nullable: false),
                        AuthorId = c.Int(),
                        ImageId = c.Int(),
                        SingerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .ForeignKey("dbo.Files", t => t.ImageId)
                .ForeignKey("dbo.Singers", t => t.SingerId, cascadeDelete: true)
                .Index(t => t.GenreId)
                .Index(t => t.AuthorId)
                .Index(t => t.ImageId)
                .Index(t => t.SingerId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 16),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 12),
                        Description = c.String(maxLength: 500),
                        AuthorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.Binary(nullable: false),
                        MimeType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Singers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32),
                        Description = c.String(maxLength: 500),
                        AuthorId = c.Int(),
                        ImageId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId)
                .ForeignKey("dbo.Files", t => t.ImageId)
                .Index(t => t.AuthorId)
                .Index(t => t.ImageId);
            
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 500),
                        MusicId = c.Int(),
                        AlbumId = c.Int(nullable: false),
                        AuthorId = c.Int(),
                        DateAdded = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.AuthorId)
                .ForeignKey("dbo.Files", t => t.MusicId)
                .Index(t => t.MusicId)
                .Index(t => t.AlbumId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 200),
                        UserId = c.Int(),
                        SongId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Songs", t => t.SongId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.SongId);
            
            CreateTable(
                "dbo.RateAlbums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        AlbumId = c.Int(nullable: false),
                        Mark = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.AlbumId);
            
            CreateTable(
                "dbo.RateSongs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        SongId = c.Int(nullable: false),
                        Mark = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Songs", t => t.SongId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.SongId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RateSongs", "UserId", "dbo.Users");
            DropForeignKey("dbo.RateSongs", "SongId", "dbo.Songs");
            DropForeignKey("dbo.RateAlbums", "UserId", "dbo.Users");
            DropForeignKey("dbo.RateAlbums", "AlbumId", "dbo.Albums");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "SongId", "dbo.Songs");
            DropForeignKey("dbo.Songs", "MusicId", "dbo.Files");
            DropForeignKey("dbo.Songs", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.Songs", "AlbumId", "dbo.Albums");
            DropForeignKey("dbo.Albums", "SingerId", "dbo.Singers");
            DropForeignKey("dbo.Singers", "ImageId", "dbo.Files");
            DropForeignKey("dbo.Singers", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.Albums", "ImageId", "dbo.Files");
            DropForeignKey("dbo.Albums", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Genres", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.Albums", "AuthorId", "dbo.Users");
            DropIndex("dbo.RateSongs", new[] { "SongId" });
            DropIndex("dbo.RateSongs", new[] { "UserId" });
            DropIndex("dbo.RateAlbums", new[] { "AlbumId" });
            DropIndex("dbo.RateAlbums", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "SongId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Songs", new[] { "AuthorId" });
            DropIndex("dbo.Songs", new[] { "AlbumId" });
            DropIndex("dbo.Songs", new[] { "MusicId" });
            DropIndex("dbo.Singers", new[] { "ImageId" });
            DropIndex("dbo.Singers", new[] { "AuthorId" });
            DropIndex("dbo.Genres", new[] { "AuthorId" });
            DropIndex("dbo.Albums", new[] { "SingerId" });
            DropIndex("dbo.Albums", new[] { "ImageId" });
            DropIndex("dbo.Albums", new[] { "AuthorId" });
            DropIndex("dbo.Albums", new[] { "GenreId" });
            DropTable("dbo.RateSongs");
            DropTable("dbo.RateAlbums");
            DropTable("dbo.Comments");
            DropTable("dbo.Songs");
            DropTable("dbo.Singers");
            DropTable("dbo.Files");
            DropTable("dbo.Genres");
            DropTable("dbo.Users");
            DropTable("dbo.Albums");
        }
    }
}
