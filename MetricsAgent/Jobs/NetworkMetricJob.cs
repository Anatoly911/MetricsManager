using MetricsAgent.Models;
using MetricsAgent.Services;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class NetworkMetricJob : IJob
    {
        private INetworkMetricsRepository _networkMetricsRepository;
        private PerformanceCounter _networkCounter;
        public NetworkMetricJob(INetworkMetricsRepository networkMetricsRepository)
        {
            _networkMetricsRepository = networkMetricsRepository;
            _networkCounter = new PerformanceCounter("ASP.NET Applications(__Total__)", "Requests/Sec");

        }
        public Task Execute(IJobExecutionContext context)
        {
            float networkUsageInPercents = _networkCounter.NextValue();
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            _networkMetricsRepository.Create(new NetworkMetric
            {
                Time = time.TotalSeconds,
                Value = (int)networkUsageInPercents
            });
            return Task.CompletedTask;
        }
    }
}
