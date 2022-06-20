using MetricsAgent.Models;
using MetricsAgent.Services;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class DotNetMetricJob : IJob
    {
        private IDotNetMetricsRepository _dotnetMetricsRepository;
        private PerformanceCounter _dotnetCounter;
        public DotNetMetricJob(IDotNetMetricsRepository dotnetMetricsRepository)
        {
            _dotnetMetricsRepository = dotnetMetricsRepository;
            _dotnetCounter = new PerformanceCounter(".NET CLR Exceptions", "# of Exceps Thrown / sec", "_Global_");
        }
        public Task Execute(IJobExecutionContext context)
        {
            float dotnetUsageInPercents = _dotnetCounter.NextValue();
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            _dotnetMetricsRepository.Create(new DotNetMetric
            {
                Time = time.TotalSeconds,
                Value = (int)dotnetUsageInPercents
            });
            return Task.CompletedTask;
        }
    }
}
