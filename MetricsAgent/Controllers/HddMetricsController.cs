using MetricsAgent.Models;
using MetricsAgent.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private IHddMetricsRepository _hddMetricsRepository;
        private readonly ILogger<HddMetricsController> _logger;
        public HddMetricsController(IHddMetricsRepository _hddMetricsRepository)
        {
            this._hddMetricsRepository = _hddMetricsRepository;
        }
        public HddMetricsController(ILogger<HddMetricsController> logger)
        {
            _logger = logger;
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] HddMetricCreateRequest request)
        {
            _hddMetricsRepository.Create(new HddMetric
            {
                Time = request.Time,
                Value = request.Value
            });
            if (_logger != null)
                _logger.LogDebug(1, "NLog встроен в HddMetricsController");
            return Ok();
        }
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _hddMetricsRepository.GetAll();
            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new HddMetricDto
                {
                    Time = metric.Time,
                    Value = metric.Value,
                    Id = metric.Id
                });
            }
            return Ok(response);
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Привет! Это наше первое сообщение в лог");
            return Ok(_hddMetricsRepository.GetByTimePeriod(fromTime, toTime));
        }
    }
}
