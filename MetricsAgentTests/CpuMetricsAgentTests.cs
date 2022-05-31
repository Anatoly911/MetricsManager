using MetricsAgent;
using MetricsAgent.Controllers;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuMetricsAgentTests
    {
        private CpuMetricsController _cpuMetricsController;
        private Mock<ICpuMetricsRepository> mock;
        public CpuMetricsAgentTests()
        {
            mock = new Mock<ICpuMetricsRepository>();
            _cpuMetricsController = new CpuMetricsController(null, null, mock.Object);
        }
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
           /* mock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();
            var result = _cpuMetricsController.Create(new MetricsAgent.Models.Requests.CpuMetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });
            mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());*/
        }
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            mock.Setup(repository =>
                    repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
                    .Returns(new List<CpuMetric>());

            _cpuMetricsController.GetMetrics(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(100));

            mock.Verify(repository =>
                    repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()), Times.AtMostOnce());
        }
    }
}
