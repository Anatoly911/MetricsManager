using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent.Services
{
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;
        public RamMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }
        public void Create(RamMetric item)
        {
            DatabaseOptions databaseOptions = _databaseOptions.Value;
            using var connection = new SQLiteConnection(databaseOptions.ConnectionString);
            connection.Execute("INSERT INTO rammetrics(value, time) VALUES(@value, @time)",
            new
            {
                value = item.Value,
                time = item.Time
            });
        }
        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Execute("DELETE FROM rammetrics WHERE id=@id",
            new
            {
                id
            });
        }
        public void Update(RamMetric item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Execute("UPDATE rammetrics SET value = @value, time = @time WHERE id=@id",
            new
            {
                value = item.Value,
                time = item.Time,
                id = item.Id
            });
        }
        public IList<RamMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            List<RamMetric> metrics = connection.Query<RamMetric>("SELECT * FROM rammetrics").ToList();
            return metrics;
        }
        public RamMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            RamMetric metric = connection.QuerySingle<RamMetric>("SELECT Id, Time, Value FROM rammetrics WHERE id = @id",
            new { id });
            return metric;
        }
        public IList<RamMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            string table = "rammetrics";
            List<RamMetric> metrics = connection.Query<RamMetric>($"SELECT * FROM {table} where time >= @timeFrom and time <= @timeTo",
                new { timeFrom = timeFrom.TotalSeconds, timeTo = timeTo.TotalSeconds }).ToList();
            return metrics;
        }
    }
}
