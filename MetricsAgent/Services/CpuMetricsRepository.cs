using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Dapper;
using System.Linq;
using Microsoft.Extensions.Options;

namespace MetricsAgent.Services
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;
        public CpuMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }
        public void Create(CpuMetric item)
        {
            DatabaseOptions databaseOptions = _databaseOptions.Value;
            using var connection = new SQLiteConnection(databaseOptions.ConnectionString);
            connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
            new
            {
                value = item.Value,
                time = item.Time
            });
        }
        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Execute("DELETE FROM cpumetrics WHERE id=@id", new { id });
        }
        public void Update(CpuMetric item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Execute("UPDATE cpumetrics SET value = @value, time = @time WHERE id = @id",
            new
            {
                value = item.Value,
                time = item.Time,
                id = item.Id
            });
        }
        public IList<CpuMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            List<CpuMetric> metrics = connection.Query<CpuMetric>("SELECT * FROM cpumetrics").ToList();
            return metrics;
        }
        public CpuMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            CpuMetric metric = connection.QuerySingle<CpuMetric>("SELECT Id, Time, Value FROM cpumetrics WHERE id = @id",
            new { id });
            return metric;
        }
        public IList<CpuMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            string table = "cpumetrics";
            List<CpuMetric> metrics = connection.Query<CpuMetric>($"SELECT * FROM {table} where time >= @timeFrom and time <= @timeTo",
                new { timeFrom = timeFrom.TotalSeconds, timeTo = timeTo.TotalSeconds }).ToList();
            return metrics;
        }
    }
}