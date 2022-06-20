using Dapper;
using MetricsManager.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsManager.Services.Impl
{
    public class AgentPoolRepository : IAgentPool
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;
        public AgentPoolRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }
        public void Create(AgentInfo item)
        {
            DatabaseOptions databaseOptions = _databaseOptions.Value;
            using var connection = new SQLiteConnection(databaseOptions.ConnectionString);
            connection.Execute("INSERT INTO agents(AgentId , AgentAddress) VALUES(@AgentId, @AgentAddress)",
            new
            {
                AgentId = item.AgentId,
                AgentAddress = item.AgentAddress
            });
        }
        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Execute("DELETE FROM agents WHERE AgentId=@AgentId", new { id });
        }
        public void Update(AgentInfo item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Execute("UPDATE agents SET AgentId = @AgentId, AgentAddress = @AgentAddress WHERE Enable = @Enable",
            new
            {
                AgentId = item.AgentId,
                AgentAddress = item.AgentAddress,
                Enable = item.Enable,
            });
        }
        public IList<AgentInfo> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            List<AgentInfo> metrics = connection.Query<AgentInfo>("SELECT * FROM agents").ToList();
            return metrics;
        }
        public AgentInfo GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            AgentInfo metric = connection.QuerySingle<AgentInfo>("SELECT AgentId, AgentAddress, Enable FROM agents WHERE AgentId = @AgentId",
            new { id });
            return metric;
        }
        public IList<AgentInfo> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            string table = "agents";
            List<AgentInfo> metrics = connection.Query<AgentInfo>($"SELECT * FROM {table} where time >= @timeFrom and time <= @timeTo",
                new { timeFrom = timeFrom.TotalSeconds, timeTo = timeTo.TotalSeconds }).ToList();
            return metrics;
        }
    }
}
