using MetricsAgent;
using MetricsAgent.Controllers;
using MetricsAgent.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class HddMetricsAgentTests
    {
        private HddMetricsController _hddMetricsController;
        private Mock<IHddMetricsRepository> mock;
        public HddMetricsAgentTests()
        {
            mock = new Mock<IHddMetricsRepository>();
            _hddMetricsController = new HddMetricsController(mock.Object);
        }
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            mock.Setup(repository => repository.Create(It.IsAny<HddMetric>())).Verifiable();
            var result = _hddMetricsController.Create(new MetricsAgent.Models.Requests.HddMetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });
            mock.Verify(repository => repository.Create(It.IsAny<HddMetric>()), Times.AtMostOnce());
        }
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            var agentId = 1;
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            IActionResult result = _hddMetricsController.GetMetrics(agentId, fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
