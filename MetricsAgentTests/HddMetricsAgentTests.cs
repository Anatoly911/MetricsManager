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
            mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
                        .Returns(new List<HddMetric>());
            _hddMetricsController.GetMetrics(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(100));
            mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()), Times.AtMostOnce());
        }
    }
}
