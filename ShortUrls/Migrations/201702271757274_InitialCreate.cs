namespace ShortUrls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Urls",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LongUrl = c.String(nullable: false),
                        ShortUrl = c.String(nullable: false),
                        DateGenerated = c.DateTime(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Urls");
        }
    }
}
