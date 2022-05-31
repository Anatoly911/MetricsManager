﻿using MetricsAgent.Models;
using MetricsAgent.Services;
using Quartz;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class HddMetricJob : IJob
    {
        private IHddMetricsRepository _hddMetricsRepository;
        private DriveInfo _hddCounter;
        public HddMetricJob(IHddMetricsRepository hddMetricsRepository)
        {
            _hddMetricsRepository = hddMetricsRepository;
            _hddCounter = new DriveInfo("D");
        }
        public Task Execute(IJobExecutionContext context)
        {
            float hddUsageInPercents = _hddCounter.AvailableFreeSpace;
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            _hddMetricsRepository.Create(new HddMetric
            {
                Time = time.TotalSeconds,
                Value = (int)hddUsageInPercents
            });

            return Task.CompletedTask;
        }
    }
}