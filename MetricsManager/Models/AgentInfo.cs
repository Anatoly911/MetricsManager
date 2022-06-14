using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Data.SQLite;

namespace MetricsManager.Models
{
    public class AgentInfo
    {
        public int AgentId { get; set; }
        public Uri AgentAddress { get; set; }
        public bool Enable { get; set; }

    }
}
