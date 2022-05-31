using MetricsManager.Models;
using MetricsManager.Services;
using Quartz;
using System;
using System.Threading.Tasks;

namespace MetricsManager.Jobs
{
    public class NetworkMetricJob : IJob
    {
        private INetworkMetricsRepository _networkMetricsRepository;
        private IMetricsAgentClient _metricsAgentClient;
        private AgentPool _agentPool;
        public NetworkMetricJob(INetworkMetricsRepository networkMetricsRepository, IMetricsAgentClient metricsAgentClient, AgentPool agentPool)
        {
            _networkMetricsRepository = networkMetricsRepository;
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
