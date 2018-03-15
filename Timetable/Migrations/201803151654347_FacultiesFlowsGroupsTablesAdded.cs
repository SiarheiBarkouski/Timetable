namespace Timetable.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FacultiesFlowsGroupsTablesAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Faculties",
                c => new
                    {
                        IdFaculty = c.Int(nullable: false, identity: true),
                        NameFaculty = c.String(),
                    })
                .PrimaryKey(t => t.IdFaculty);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        IdGroup = c.Int(nullable: false, identity: true),
                        NameGroup = c.String(),
                        IdFlow = c.Int(),
                        IdFaculty = c.Int(nullable: false),
                        EduLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdGroup)
                .ForeignKey("dbo.Faculties", t => t.IdFaculty, cascadeDelete: true)
                .ForeignKey("dbo.Flows", t => t.IdFlow)
                .Index(t => t.IdFlow)
                .Index(t => t.IdFaculty);
            
            CreateTable(
                "dbo.Flows",
                c => new
                    {
                        IdFlow = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.IdFlow);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Groups", "IdFlow", "dbo.Flows");
            DropForeignKey("dbo.Groups", "IdFaculty", "dbo.Faculties");
            DropIndex("dbo.Groups", new[] { "IdFaculty" });
            DropIndex("dbo.Groups", new[] { "IdFlow" });
            DropTable("dbo.Flows");
            DropTable("dbo.Groups");
            DropTable("dbo.Faculties");
        }
    }
}
