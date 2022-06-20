using MetricsAgent.Models.Requests;

namespace MetricsAgent.Services
{
    public interface IMetricsAgentClient
    {
        AllCpuMetricsResponse GetCpuMetrics(CpuMetricCreateRequest cpuMetricsRequest);
        AllDotNetMetricsResponse GetDotNetMetrics(DotNetMetricCreateRequest dotNetMetricsRequest);
        AllHddMetricsResponse GetHddMetrics(HddMetricCreateRequest hddMetricsRequest);
        AllNetworkMetricsResponse GetNetworkMetrics(NetworkMetricCreateRequest networkMetricsRequest);
        AllRamMetricsResponse GetRamMetrics(RamMetricCreateRequest ramMetricsRequest);
    }
}
