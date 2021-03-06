using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
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
        private readonly IHddMetricsRepository _hddMetricsRepository;
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IMapper _mapper;
        public HddMetricsController(IMapper mapper, ILogger<HddMetricsController> logger, IHddMetricsRepository hddMetricsRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _hddMetricsRepository = hddMetricsRepository;
        }
        /*[HttpGet("getHddMetrics")]
        [ProducesResponseType(typeof(AllHddMetricsResponse), StatusCodes.Status200OK)]*/
       /* public IActionResult GetMetricsV2([FromBody] HddMetricCreateRequest request)
        {
            AllHddMetricsResponse response = _metricsAgentClient.GetHddMetrics(new HddMetricCreateRequest()
            {
                Time = request.Time,
                Value = request.Value,
            });
            return Ok(response);
        }*/
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _hddMetricsRepository.GetAll();
            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };
            foreach (var metric in metrics)
                response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));

            return Ok(response);
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            var metrics = _hddMetricsRepository.GetByTimePeriod(fromTime, toTime);
            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new HddMetricDto
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
