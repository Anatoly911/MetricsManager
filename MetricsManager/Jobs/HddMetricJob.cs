using MetricsManager.Models;
using MetricsManager.Services;
using Quartz;
using System;
using System.Threading.Tasks;

namespace MetricsManager.Jobs
{
    public class HddMetricJob : IJob
    {
        private IHddMetricsRepository _hddMetricsRepository;
        private IMetricsAgentClient _metricsAgentClient;
        private AgentPool _agentPool;
        public HddMetricJob(IHddMetricsRepository hddMetricsRepository, IMetricsAgentClient metricsAgentClient, AgentPool agentPool)
        {
            _hddMetricsRepository = hddMetricsRepository;
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
