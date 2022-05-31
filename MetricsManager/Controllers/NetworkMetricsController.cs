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
    [Route("api/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly AgentPool _agentPoll;
        private readonly IMetricsAgentClient _metricsAgentClient;
        public NetworkMetricsController(IMetricsAgentClient metricsAgentClient, AgentPool agentPoll)
        {
            _agentPoll = agentPoll;
            _metricsAgentClient = metricsAgentClient;
        }
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            NetworkMetricsResponse response = _metricsAgentClient.GetNetworkMetrics(new NetworkMetricsRequest()
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
