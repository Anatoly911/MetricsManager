using MetricsManager.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Предоставляет работу с агентами")]
    public class AgentsController : ControllerBase
    {
        private AgentPool _agentPool;
        public AgentsController(AgentPool agentPool)
        {
            _agentPool = agentPool;
        }
        [HttpPost("register")]
        [SwaggerOperation(description: "Регистрация нового агента в системе мониторинга")]
        [SwaggerResponse(200, "Успешная операция")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            if (agentInfo != null)
            {
                _agentPool.Add(agentInfo);
            }
            return Ok();
        }
        [HttpPut("enable/{agentId}")]
        [SwaggerOperation(description: "Изменить статус агента при необходимости")]
        [SwaggerResponse(200, "Успешная операция")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            if (_agentPool.Values.ContainsKey(agentId))
                _agentPool.Values[agentId].Enable = true;
            return Ok();
        }
        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            if (_agentPool.Values.ContainsKey(agentId))
                _agentPool.Values[agentId].Enable = false;
            return Ok();
        }
        [HttpGet("get")]
        public IActionResult GetAllAgents()
        {
            return Ok(_agentPool.Get());
        }
    }
}

