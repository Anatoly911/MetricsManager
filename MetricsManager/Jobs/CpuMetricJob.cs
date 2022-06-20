using MetricsManager.Models;
using MetricsManager.Services;
using Quartz;
using System;
using System.Threading.Tasks;

namespace MetricsManager.Jobs
{
    public class CpuMetricJob : IJob
    {
        private ICpuMetricsRepository _cpuMetricsRepository;
        private IMetricsAgentClient _metricsAgentClient;
        private AgentPool _agentPool;
        public CpuMetricJob(ICpuMetricsRepository cpuMetricsRepository, IMetricsAgentClient metricsAgentClient, AgentPool agentPool)
        {
            _cpuMetricsRepository = cpuMetricsRepository;
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
