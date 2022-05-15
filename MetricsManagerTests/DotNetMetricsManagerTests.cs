using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class DotNetMetricsManagerTests
    {
        private DotNetMetricsController _dotNetMetricsController;
        public DotNetMetricsManagerTests()
        {
            _dotNetMetricsController = new DotNetMetricsController(null);
        }
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            var agentId = 1;
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            IActionResult result = _dotNetMetricsController.GetMetricsFromAgent(agentId, fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
