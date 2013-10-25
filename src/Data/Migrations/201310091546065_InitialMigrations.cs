namespace Simple.Web.Windsor.Owin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigrations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicants", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applicants","Email",null);
        }
    }
}
