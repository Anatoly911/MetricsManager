using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Data.SQLite;

namespace MetricsManager.Models
{
    /// <summary>
    /// Агент
    /// </summary>
    public class AgentInfo
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int AgentId { get; set; }
        /// <summary>
        /// Url адресс
        /// </summary>
        public Uri AgentAddress { get; set; }
        /// <summary>
        /// Активность
        /// </summary>
        public bool Enable { get; set; }

    }
}
