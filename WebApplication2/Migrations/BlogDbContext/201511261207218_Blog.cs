namespace WebApplication2.Migrations.BlogDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Blog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        UrlSeo = c.String(nullable: false),
                        Description = c.String(nullable: false, maxLength: 20),
                        Checked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.PostCategory",
                c => new
                    {
                        PostId = c.String(nullable: false, maxLength: 128),
                        CategoryId = c.String(nullable: false, maxLength: 128),
                        Checked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostId, t.CategoryId })
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Post", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Title = c.String(nullable: false, maxLength: 50),
                        ShortDescription = c.String(nullable: false, maxLength: 250),
                        Body = c.String(nullable: false),
                        Meta = c.String(nullable: false, maxLength: 25),
                        UrlSeo = c.String(nullable: false),
                        Published = c.Boolean(nullable: false),
                        NetLikeCount = c.Int(nullable: false),
                        PostedOn = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PostTag",
                c => new
                    {
                        PostID = c.String(nullable: false, maxLength: 128),
                        TagId = c.String(nullable: false, maxLength: 128),
                        Checked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostID, t.TagId })
                .ForeignKey("dbo.Post", t => t.PostID, cascadeDelete: true)
                .ForeignKey("dbo.Tag", t => t.TagId, cascadeDelete: true)
                .Index(t => t.PostID)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        UrlSeo = c.String(nullable: false, maxLength: 20),
                        Checked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            

            //?allaghhhhh?
            CreateTable(
                "dbo.PostVideo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VideoUrl = c.String(nullable: false),
                        VideoThumbnail = c.String(),
                        PostId = c.String(maxLength: 128),
                        VideoSiteName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Post", t => t.PostId)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostVideo", "PostId", "dbo.Post");
            DropForeignKey("dbo.PostTag", "TagId", "dbo.Tag");
            DropForeignKey("dbo.PostTag", "PostID", "dbo.Post");
            DropForeignKey("dbo.PostCategory", "PostId", "dbo.Post");
            DropForeignKey("dbo.PostCategory", "CategoryId", "dbo.Category");
            DropIndex("dbo.PostVideo", new[] { "PostId" });
            DropIndex("dbo.PostTag", new[] { "TagId" });
            DropIndex("dbo.PostTag", new[] { "PostID" });
            DropIndex("dbo.PostCategory", new[] { "CategoryId" });
            DropIndex("dbo.PostCategory", new[] { "PostId" });
            DropTable("dbo.PostVideo");
            DropTable("dbo.Tag");
            DropTable("dbo.PostTag");
            DropTable("dbo.Post");
            DropTable("dbo.PostCategory");
            DropTable("dbo.Category");
        }
    }
}
