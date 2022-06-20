using Dapper;
using MetricsManager.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsManager.Services
{
    public interface IAgentPool : IRepository<AgentInfo>
    {
    }
}
