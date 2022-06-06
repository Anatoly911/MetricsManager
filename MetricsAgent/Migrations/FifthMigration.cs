using FluentMigrator;

namespace MetricsAgent.Migrations
{
    [Migration(5)]
    public class FifthMigration : Migration
    {
        public override void Up()
        {
            Create.Table("rammetrics")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Value").AsInt32()
            .WithColumn("Time").AsInt64();
        }
        public override void Down()
        {
            Delete.Table("rammetrics");
        }
    }
}
