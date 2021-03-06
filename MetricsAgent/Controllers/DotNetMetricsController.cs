using AutoMapper;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly IDotNetMetricsRepository _dotNetMetricsRepository;
        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IMapper _mapper;
        public DotNetMetricsController(IMapper mapper, ILogger<DotNetMetricsController> logger, IDotNetMetricsRepository dotNetMetricsRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _dotNetMetricsRepository = dotNetMetricsRepository;
        }
        /*[HttpGet("getDotNetMetrics")]
        [ProducesResponseType(typeof(AllDotNetMetricsResponse), StatusCodes.Status200OK)]*/
        /*public IActionResult GetMetricsV2([FromBody] DotNetMetricCreateRequest request)
        {
            AllDotNetMetricsResponse response = _metricsAgentClient.GetDotNetMetrics(new DotNetMetricCreateRequest()
            {
                Time = request.Time,
                Value = request.Value,
            });
            return Ok(response);
        }*/
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _dotNetMetricsRepository.GetAll();
            var response = new AllDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };
            foreach (var metric in metrics)
                response.Metrics.Add(_mapper.Map<DotNetMetricDto>(metric));

            return Ok(response);
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            var metrics = _dotNetMetricsRepository.GetByTimePeriod(fromTime, toTime);
            var response = new AllDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new DotNetMetricDto
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
