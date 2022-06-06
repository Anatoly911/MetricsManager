using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly IRamMetricsRepository _ramMetricsRepository;
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IMapper _mapper;
        public RamMetricsController(IMapper mapper, ILogger<RamMetricsController> logger, IRamMetricsRepository ramMetricsRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _ramMetricsRepository = ramMetricsRepository;
        }
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _ramMetricsRepository.GetAll();
            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };
            foreach (var metric in metrics)
                response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
            return Ok(response);
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            var metrics = _ramMetricsRepository.GetByTimePeriod(fromTime, toTime);
            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new RamMetricDto
                {
                    Time = TimeSpan.FromSeconds(metric.Time),
                    Value = metric.Value,
                    Id = metric.Id
                });
            }
            return Ok(response);
        }
    }
}
