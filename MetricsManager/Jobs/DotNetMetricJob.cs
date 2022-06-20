using MetricsManager.Models;
using MetricsManager.Services;
using Quartz;
using System;
using System.Threading.Tasks;

namespace MetricsManager.Jobs
{
    public class DotNetMetricJob : IJob
    {
        private IDotNetMetricsRepository _dotnetMetricsRepository;
        private IMetricsAgentClient _metricsAgentClient;
        private AgentPool _agentPool;
        public DotNetMetricJob(IDotNetMetricsRepository dotnetMetricsRepository, IMetricsAgentClient metricsAgentClient, AgentPool agentPool)
        {
            _dotnetMetricsRepository = dotnetMetricsRepository;
            _metricsAgentClient = metricsAgentClient;
            _agentPool = agentPool;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            return Task.CompletedTask;
        }
    }
}
