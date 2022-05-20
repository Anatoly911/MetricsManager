using MetricsAgent.Models;
using MetricsAgent.Services;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class CpuMetricJob : IJob
    {
        private ICpuMetricsRepository _cpuMetricsRepository;
        private PerformanceCounter _cpuCounter;
        public CpuMetricJob(ICpuMetricsRepository cpuMetricsRepository)
        {
            _cpuMetricsRepository = cpuMetricsRepository;
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }
        public Task Execute(IJobExecutionContext context)
        {
            float cpuUsageInPercents = _cpuCounter.NextValue();
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            _cpuMetricsRepository.Create(new CpuMetric
            {
                Time = time.TotalSeconds,
                Value = (int)cpuUsageInPercents
            });
            return Task.CompletedTask;
        }
    }
}
