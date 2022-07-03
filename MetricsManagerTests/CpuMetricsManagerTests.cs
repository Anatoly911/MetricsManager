using MetricsManager.Controllers;
using MetricsManager.Models;
using MetricsManager.Models.Requests;
using MetricsManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class CpuMetricsManagerTests
    {
        private readonly AgentPool _agentPool;
        private readonly ILogger<CpuMetricsController> _logger;
        private CpuMetricsController _cpuMetricsController;
        private readonly IMetricsAgentClient _metricsAgentClient;
        public CpuMetricsManagerTests()
        {
            
             _agentPool = new AgentPool();
            _cpuMetricsController = new CpuMetricsController(_metricsAgentClient, _agentPool);
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
