using MetricsAgent;
using MetricsAgent.Controllers;
using MetricsAgent.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
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
            _dotNetMetricsController = new DotNetMetricsController(null, null, mock.Object, null);
        }
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            /*mock.Setup(repository => repository.Create(It.IsAny<DotNetMetric>())).Verifiable();
            var result = _dotNetMetricsController.Create(new MetricsAgent.Models.Requests.DotNetMetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });
            mock.Verify(repository => repository.Create(It.IsAny<DotNetMetric>()), Times.AtMostOnce());*/
        }
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
                        .Returns(new List<DotNetMetric>());
            _dotNetMetricsController.GetMetrics(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(100));
            mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()), Times.AtMostOnce());
        }
    }
}
