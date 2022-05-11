using MetricsAgent;
using MetricsAgent.Controllers;
using MetricsAgent.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class DotNetMetricsAgentTests
    {
        private DotNetMetricsController _dotNetMetricsController;
        private Mock<IDotNetMetricsRepository> mock;
        public DotNetMetricsAgentTests()
        {
            mock = new Mock<IDotNetMetricsRepository>();
            _dotNetMetricsController = new DotNetMetricsController(mock.Object);
        }
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            mock.Setup(repository => repository.Create(It.IsAny<DotNetMetric>())).Verifiable();
            var result = _dotNetMetricsController.Create(new MetricsAgent.Models.Requests.DotNetMetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });
            mock.Verify(repository => repository.Create(It.IsAny<DotNetMetric>()), Times.AtMostOnce());
        }
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            var agentId = 1;
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            IActionResult result = _dotNetMetricsController.GetMetrics(agentId, fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

    }
}
