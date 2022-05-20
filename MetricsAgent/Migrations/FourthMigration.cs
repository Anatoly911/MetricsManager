using FluentMigrator;

namespace MetricsAgent.Migrations
{
    [Migration(4)]
    public class FourthMigration : Migration
    {
        public override void Up()
        {
            Create.Table("networkmetrics")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Value").AsInt32()
            .WithColumn("Time").AsInt64();
        }
        public override void Down()
        {
            Delete.Table("networkmetrics");
        }
    }
}
