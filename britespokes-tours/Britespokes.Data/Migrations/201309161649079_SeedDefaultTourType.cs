namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class SeedDefaultTourType : DbMigration
  {
    public override void Up()
    {
      Sql(@"IF NOT EXISTS (SELECT * FROM dbo.TourTypes WHERE name = 'Default')" +
          "BEGIN; " +
          "INSERT INTO dbo.TourTypes (Name, DisplayName, slug) VALUES ('Default', 'Default', '');" +
          "END");
      Sql(@"UPDATE Tours SET TourTypeId = (SELECT Id FROM TourTypes WHERE name = 'Default')");
    }

    public override void Down()
    {
      Sql(@"UPDATE Tours SET TourTypeId = NULL");
      Sql(@"DELETE FROM dbo.TourTypes WHERE name = 'Default'");
    }
  }
}