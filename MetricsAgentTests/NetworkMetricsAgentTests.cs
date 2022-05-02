using MetricsAgent;
using MetricsAgent.Controllers;
using MetricsAgent.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class NetworkMetricsAgentTests
    {
        private NetworkMetricsController _networkMetricsController;
        private Mock<INetworkMetricsRepository> mock;
        public NetworkMetricsAgentTests()
        {
            mock = new Mock<INetworkMetricsRepository>();
            _networkMetricsController = new NetworkMetricsController(mock.Object);
        }
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            mock.Setup(repository => repository.Create(It.IsAny<NetworkMetric>())).Verifiable();
            var result = _networkMetricsController.Create(new MetricsAgent.Models.Requests.NetworkMetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });
            mock.Verify(repository => repository.Create(It.IsAny<NetworkMetric>()), Times.AtMostOnce());
        }
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            var agentId = 1;
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            IActionResult result = _networkMetricsController.GetMetrics(agentId, fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
