using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class CpuMetricsManagerTests
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private CpuMetricsController _cpuMetricsController;
        public CpuMetricsManagerTests()
        {
            /*_cpuMetricsController = new CpuMetricsController(null);*/
        }
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            var agentId = 1;
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            IActionResult result = _cpuMetricsController.GetMetricsFromAgent(agentId, fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
