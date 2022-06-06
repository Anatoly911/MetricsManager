using MetricsAgent.Models;
using MetricsAgent.Services;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob : IJob
    {
        private IRamMetricsRepository _ramMetricsRepository;
        private PerformanceCounter _ramCounter;
        public RamMetricJob(IRamMetricsRepository ramMetricsRepository)
        {
            _ramMetricsRepository = ramMetricsRepository;
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }
        public Task Execute(IJobExecutionContext context)
        {
            float ramUsageInPercents = _ramCounter.NextValue();
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            _ramMetricsRepository.Create(new RamMetric
            {
                Time = time.TotalSeconds,
                Value = (int)ramUsageInPercents
            });
            return Task.CompletedTask;
        }
    }
}
