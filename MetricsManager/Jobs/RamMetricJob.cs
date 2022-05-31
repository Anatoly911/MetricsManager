using MetricsManager.Models;
using MetricsManager.Services;
using Quartz;
using System;
using System.Threading.Tasks;

namespace MetricsManager.Jobs
{
    public class RamMetricJob : IJob
    {
        private IRamMetricsRepository _ramMetricsRepository;
        private IMetricsAgentClient _metricsAgentClient;
        private AgentPool _agentPool;
        public RamMetricJob(IRamMetricsRepository ramMetricsRepository, IMetricsAgentClient metricsAgentClient, AgentPool agentPool)
        {
            _ramMetricsRepository = ramMetricsRepository;
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
