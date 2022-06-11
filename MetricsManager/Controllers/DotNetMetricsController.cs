using MetricsManager.Models;
using MetricsManager.Models.Requests;
using MetricsManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;

namespace MetricsManager.Controllers
{
    [Route("api/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly AgentPool _agentPoll;
        private readonly IMetricsAgentClient _metricsAgentClient;
        public DotNetMetricsController(IMetricsAgentClient metricsAgentClient, AgentPool agentPoll)
        {
            _agentPoll = agentPoll;
            _metricsAgentClient = metricsAgentClient;
        }
        [HttpGet("getDotNetMetricsFromAgent")]
        [ProducesResponseType(typeof(DotNetMetricsResponse), StatusCodes.Status200OK)]
        public IActionResult GetMetricsFromAgentV2([FromBody] DotNetMetricsRequest request)
        {
            DotNetMetricsResponse response = _metricsAgentClient.GetDotNetMetrics(new DotNetMetricsRequest()
            {
                AgentId = request.AgentId,
                FromTime = request.FromTime,
                ToTime = request.ToTime
            });
            return Ok(response);
        }
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            DotNetMetricsResponse response = _metricsAgentClient.GetDotNetMetrics(new DotNetMetricsRequest()
            {
                AgentId = agentId,
                FromTime = fromTime,
                ToTime = toTime
            });
            return Ok(response);
        }
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}
