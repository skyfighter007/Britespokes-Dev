namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class AddMailingListFieldsToTour : DbMigration
  {
    public override void Up()
    {
      AddColumn("dbo.Tours", "MailingListUrl", c => c.String());
      AddColumn("dbo.Tours", "MailingListEmailName", c => c.String());
    }

    public override void Down()
    {
      DropColumn("dbo.Tours", "MailingListEmailName");
      DropColumn("dbo.Tours", "MailingListUrl");
    }
  }
}