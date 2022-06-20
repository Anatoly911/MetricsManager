using MetricsManager.Models;
using MetricsManager.Models.Requests;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;

namespace MetricsManager.Services.Impl
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MetricsAgentClient> _logger;
        private readonly AgentPool _agentPool;
        public MetricsAgentClient(HttpClient httpClient, AgentPool agentPool, ILogger<MetricsAgentClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _agentPool = agentPool;
        }
        public CpuMetricsResponse GetCpuMetrics(CpuMetricsRequest cpuMetricsRequest)
        {
            try
            {
                AgentInfo agentInfo = _agentPool.Get().FirstOrDefault(agent => agent.AgentId == cpuMetricsRequest.AgentId);
                if (agentInfo == null)
                    throw new Exception($"AgentId #{cpuMetricsRequest.AgentId} is not found");
                string requestQuery = $"{agentInfo.AgentAddress}api/metrics/cpu/from/{cpuMetricsRequest.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{cpuMetricsRequest.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestQuery);
                httpRequestMessage.Headers.Add("Accept", "application/json");
                HttpResponseMessage response = _httpClient.SendAsync(httpRequestMessage).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseStr = response.Content.ReadAsStringAsync().Result;
                    CpuMetricsResponse cpuMetricsResponse = (CpuMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(CpuMetricsResponse));
                    cpuMetricsResponse.AgentId = cpuMetricsRequest.AgentId;
                    return cpuMetricsResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
        public DotNetMetricsResponse GetDotNetMetrics(DotNetMetricsRequest dotNetMetricsRequest)
        {
            try
            {
                AgentInfo agentInfo = _agentPool.Get().FirstOrDefault(agent => agent.AgentId == dotNetMetricsRequest.AgentId);
                if (agentInfo == null)
                    throw new Exception($"AgentId #{dotNetMetricsRequest.AgentId} is not found");
                string requestQuery = $"{agentInfo.AgentAddress}api/metrics/dotnet/from/{dotNetMetricsRequest.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{dotNetMetricsRequest.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestQuery);
                httpRequestMessage.Headers.Add("Accept", "application/json");
                HttpResponseMessage response = _httpClient.SendAsync(httpRequestMessage).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseStr = response.Content.ReadAsStringAsync().Result;
                    DotNetMetricsResponse dotnetMetricsResponse = (DotNetMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(DotNetMetricsResponse));
                    dotnetMetricsResponse.AgentId = dotNetMetricsRequest.AgentId;
                    return dotnetMetricsResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
        public HddMetricsResponse GetHddMetrics(HddMetricsRequest hddMetricsRequest)
        {
            try
            {
                AgentInfo agentInfo = _agentPool.Get().FirstOrDefault(agent => agent.AgentId == hddMetricsRequest.AgentId);
                if (agentInfo == null)
                    throw new Exception($"AgentId #{hddMetricsRequest.AgentId} is not found");
                string requestQuery = $"{agentInfo.AgentAddress}api/metrics/hdd/from/{hddMetricsRequest.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{hddMetricsRequest.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestQuery);
                httpRequestMessage.Headers.Add("Accept", "application/json");
                HttpResponseMessage response = _httpClient.SendAsync(httpRequestMessage).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseStr = response.Content.ReadAsStringAsync().Result;
                    HddMetricsResponse hddMetricsResponse = (HddMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(HddMetricsResponse));
                    hddMetricsResponse.AgentId = hddMetricsRequest.AgentId;
                    return hddMetricsResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public NetworkMetricsResponse GetNetworkMetrics(NetworkMetricsRequest networkMetricsRequest)
        {
            try
            {
                AgentInfo agentInfo = _agentPool.Get().FirstOrDefault(agent => agent.AgentId == networkMetricsRequest.AgentId);
                if (agentInfo == null)
                    throw new Exception($"AgentId #{networkMetricsRequest.AgentId} is not found");
                string requestQuery = $"{agentInfo.AgentAddress}api/metrics/network/from/{networkMetricsRequest.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{networkMetricsRequest.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestQuery);
                httpRequestMessage.Headers.Add("Accept", "application/json");
                HttpResponseMessage response = _httpClient.SendAsync(httpRequestMessage).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseStr = response.Content.ReadAsStringAsync().Result;
                    NetworkMetricsResponse networkMetricsResponse = (NetworkMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(NetworkMetricsResponse));
                    networkMetricsResponse.AgentId = networkMetricsRequest.AgentId;
                    return networkMetricsResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public RamMetricsResponse GetRamMetrics(RamMetricsRequest ramMetricsRequest)
        {
            try
            {
                AgentInfo agentInfo = _agentPool.Get().FirstOrDefault(agent => agent.AgentId == ramMetricsRequest.AgentId);
                if (agentInfo == null)
                    throw new Exception($"AgentId #{ramMetricsRequest.AgentId} is not found");
                string requestQuery = $"{agentInfo.AgentAddress}api/metrics/ram/from/{ramMetricsRequest.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{ramMetricsRequest.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestQuery);
                httpRequestMessage.Headers.Add("Accept", "application/json");
                HttpResponseMessage response = _httpClient.SendAsync(httpRequestMessage).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseStr = response.Content.ReadAsStringAsync().Result;
                    RamMetricsResponse ramMetricsResponse = (RamMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(RamMetricsResponse));
                    ramMetricsResponse.AgentId = ramMetricsRequest.AgentId;
                    return ramMetricsResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
