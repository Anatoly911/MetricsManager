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

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ICpuMetricsRepository _cpuMetricsRepository;
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly IMapper _mapper;
        public CpuMetricsController(IMapper mapper, ILogger<CpuMetricsController> logger, ICpuMetricsRepository cpuMetricsRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _cpuMetricsRepository = cpuMetricsRepository;
        }
       /* [HttpGet("getCpuMetrics")]
        [ProducesResponseType(typeof(AllCpuMetricsResponse), StatusCodes.Status200OK)]
        public IActionResult GetMetricsV2([FromBody] CpuMetricCreateRequest request)
        {
            AllCpuMetricsResponse response = _cpuMetricsRepository.GetByTimePeriod(fromTime, toTime);
            {
                Time = request.Time,
                Value = request.Value,
            });
            return Ok(response);
        }*/
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _cpuMetricsRepository.GetAll();
            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };

            foreach (var metric in metrics)
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));

            return Ok(response);
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            var metrics = _cpuMetricsRepository.GetByTimePeriod(fromTime, toTime);
            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new CpuMetricDto
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