using FluentMigrator;

namespace MetricsAgent.Migrations
{
    [Migration(2)]
    public class SecondMigration : Migration
    {
        public override void Up()
        {
            Create.Table("dotnetmetrics")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Value").AsInt32()
            .WithColumn("Time").AsInt64();
        }
        public override void Down()
        {
            Delete.Table("dotnetmetrics");
        }
    }
}
