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
        private PerformanceCounterCategory _networkCounter;
        public NetworkMetricJob(INetworkMetricsRepository networkMetricsRepository)
        {
            _networkMetricsRepository = networkMetricsRepository;
            _networkCounter = new PerformanceCounterCategory("Network Interface");
        }
        public Task Execute(IJobExecutionContext context)
        {
            /* PerformanceCounterCategory _networkCounter = new PerformanceCounterCategory("Network Interface");*/
            String[] instanceName = _networkCounter.GetInstanceNames();
            foreach (string ns in instanceName)
            {
                PerformanceCounter netSentCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", ns);
                PerformanceCounter netRecCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", ns);
            }
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            _networkMetricsRepository.Create(new NetworkMetric
            {
                Time = time.TotalSeconds,
                Value = int.Parse(instanceName.Length.ToString())
            });
            return Task.CompletedTask;
        }
    }
}
