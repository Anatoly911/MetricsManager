using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
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
        private readonly ICpuMetricsRepository _cpuMetricsRepository;
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly IMapper _mapper;
        public CpuMetricsController(IMapper mapper, ILogger<CpuMetricsController> logger, ICpuMetricsRepository cpuMetricsRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _cpuMetricsRepository = cpuMetricsRepository;
        }
        [HttpGet("sql-test")]
        public IActionResult TryToSqlLite()
        {
            string cs = "Data Source=:memory:";
            string stm = "SELECT SQLITE_VERSION()";
            using (var con = new SQLiteConnection(cs))
            {
                con.Open();
                using var cmd = new SQLiteCommand(stm, con);
                string version = cmd.ExecuteScalar().ToString();
                return Ok(version);
            }
        }
        [HttpGet("sql-read-write-test")]
        public IActionResult TryToInsertAndRead()
        {
            string connectionString = "Data Source=:memory:";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
                    command.ExecuteNonQuery();
                    command.CommandText = @"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY, value INT, time INT)";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(10, 1)";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(50, 2)";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(75, 4)";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(90, 5)";
                    command.ExecuteNonQuery();
                    string readQuery = "SELECT * FROM cpumetrics LIMIT 3";
                    var returnArray = new CpuMetric[3];
                    command.CommandText = readQuery;
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        var counter = 0;
                        while (reader.Read())
                        {
                            returnArray[counter] = new CpuMetric
                            {
                                Id = reader.GetInt32(0),
                                Value = reader.GetInt32(1),
                                Time = reader.GetInt64(2)
                            };
                            counter++;
                        }
                    }
                    return Ok(returnArray);
                }
            }
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            CpuMetric cpuMetric = new CpuMetric
            {
                Time = request.Time.TotalSeconds,
                Value = request.Value
            };

            _cpuMetricsRepository.Create(cpuMetric);

            if (_logger != null)
                _logger.LogDebug("Успешно добавили новую cpu метрику: {0}", cpuMetric);

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
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));

            return Ok(response);
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
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
        [HttpGet("all-old-v2")]
        public IActionResult GetAllOldV2()
        {
            MapperConfiguration config = new MapperConfiguration(
                cfg => cfg.CreateMap<CpuMetric, CpuMetricDto>().
                ForMember(x => x.Time, opt => opt.MapFrom(src => TimeSpan.FromSeconds(src.Time))));
            IMapper mapper = config.CreateMapper();
            var metrics = _cpuMetricsRepository.GetAll();
            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<CpuMetricDto>(metric));
            }
            return Ok(response);
        }
        [HttpGet("all-old-v1")]
        public IActionResult GetAllOldV1()
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
                    Time = TimeSpan.FromSeconds(metric.Time),
                    Value = metric.Value,
                    Id = metric.Id
                });
            }
            return Ok(response);
        }
        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetCpuMetricsByPercentile(
    [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime, [FromRoute] float percentile)
        {
            return Ok();
        }
    }
}