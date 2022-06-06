using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly INetworkMetricsRepository _networkMetricsRepository;
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly IMapper _mapper;
        public NetworkMetricsController(IMapper mapper, ILogger<NetworkMetricsController> logger, INetworkMetricsRepository networkMetricsRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _networkMetricsRepository = networkMetricsRepository;
        }
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _networkMetricsRepository.GetAll();
            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };
            foreach (var metric in metrics)
                response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
            return Ok(response);
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            var metrics = _networkMetricsRepository.GetByTimePeriod(fromTime, toTime);
            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new NetworkMetricDto
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
