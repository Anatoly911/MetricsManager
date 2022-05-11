using MetricsAgent.Models;
using MetricsAgent.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private ICpuMetricsRepository _cpuMetricsRepository;
        private readonly ILogger<CpuMetricsController> _logger;
        public CpuMetricsController(ICpuMetricsRepository _cpuMetricsRepository)
        {
            this._cpuMetricsRepository = _cpuMetricsRepository;
        }
        public CpuMetricsController(ILogger<CpuMetricsController> logger)
        {
            _logger = logger;
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            CpuMetric cpuMetric = new CpuMetric
            {
                Time = request.Time,
                Value = request.Value
            };
            _cpuMetricsRepository.Create(cpuMetric);
            if (_logger != null)
                _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
            return Ok();
        }
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _cpuMetricsRepository.GetAll();
            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new CpuMetricDto
                {
                    Time = metric.Time,
                    Value = metric.Value,
                    Id = metric.Id
                });
            }
            return Ok(response);
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Привет! Это наше первое сообщение в лог");
            return Ok();
        }
    }
}
