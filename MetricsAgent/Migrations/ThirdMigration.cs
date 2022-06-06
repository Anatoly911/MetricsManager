using FluentMigrator;

namespace MetricsAgent.Migrations
{
    [Migration(3)]
    public class ThirdMigration : Migration
    {
        public override void Up()
        {
            Create.Table("hddmetrics")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Value").AsInt32()
            .WithColumn("Time").AsInt64();
        }
        public override void Down()
        {
            Delete.Table("hddmetrics");
        }
    }
}
