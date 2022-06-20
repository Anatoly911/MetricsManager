using System;
using System.Net.Http;

namespace MetricsAgent.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MetricsAgentClient metricsAgentClient = new MetricsAgentClient("https://localhost:5002", new HttpClient());
            while (true)
            {
                Console.WriteLine("Tasks");
                Console.WriteLine("=======================================================");
                Console.WriteLine("0 - Завершение работы приложения");
                Console.WriteLine("1 - Получить метрики за последнюю минуту (CPU)");
                Console.WriteLine("2 - Получить метрики за последнюю минуту (DOTNET)");
                Console.WriteLine("3 - Получить метрики за последнюю минуту (HDD)");
                Console.WriteLine("4 - Получить метрики за последнюю минуту (NETWORK)");
                Console.WriteLine("5 - Получить метрики за последнюю минуту (RAM)");
                Console.WriteLine("=======================================================");
                Console.Write("Введите номер задачи: ");
                if (int.TryParse(Console.ReadLine(), out int taskNumber))
                {
                    switch (taskNumber)
                    {
                        case 0:
                            Console.WriteLine("Завершение работы приложения");
                            return;
                        case 1:
                            TimeSpan time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                            CpuMetricCreateRequest cpuMetricsRequest = new CpuMetricCreateRequest()
                            {
                                Time = time.ToString("dd\\.hh\\:mm\\:ss"),
                            };
                            try
                            {
                                AllCpuMetricsResponse cpuMetricsResponse = metricsAgentClient.GetCpuMetricsAsync(cpuMetricsRequest).Result;
                                foreach (CpuMetricDto cpuMetric in cpuMetricsResponse.Metrics)
                                {
                                    Console.WriteLine($"{TimeSpan.Parse(cpuMetric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {cpuMetric.Value}");
                                }
                                Console.ReadKey();
                                Console.Clear();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Произошла ошибка при попытке получить CPU метрики. \n{ex.Message}");
                            }
                            break;
                        case 2:
                            TimeSpan time2 = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                            DotNetMetricCreateRequest dotNetMetricsRequest = new DotNetMetricCreateRequest()
                            {
                                Time = time2.ToString("dd\\.hh\\:mm\\:ss")
                            };
                            try
                            {
                                AllDotNetMetricsResponse dotNetMetricsResponse = metricsAgentClient.GetDotNetMetricsAsync(dotNetMetricsRequest).Result;
                                foreach (DotNetMetricDto dotNetMetric in dotNetMetricsResponse.Metrics)
                                {
                                    Console.WriteLine($"{TimeSpan.Parse(dotNetMetric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {dotNetMetric.Value}");
                                }
                                Console.ReadKey();
                                Console.Clear();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Произошла ошибка при попытке получить DOTNET метрики. \n{ex.Message}");
                            }
                            break;
                        case 3:
                            TimeSpan time3 = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                            HddMetricCreateRequest hddMetricsRequest = new HddMetricCreateRequest()
                            {
                                Time = time3.ToString("dd\\.hh\\:mm\\:ss")
                            };
                            try
                            {
                                AllHddMetricsResponse hddMetricsResponse = metricsAgentClient.GetHddMetricsAsync(hddMetricsRequest).Result;
                                foreach (HddMetricDto hddMetric in hddMetricsResponse.Metrics)
                                {
                                    Console.WriteLine($"{TimeSpan.Parse(hddMetric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {hddMetric.Value}");
                                }
                                Console.ReadKey();
                                Console.Clear();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Произошла ошибка при попытке получить HDD метрики. \n{ex.Message}");
                            }
                            break;
                        case 4:
                            TimeSpan time4 = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                            NetworkMetricCreateRequest networkMetricsRequest = new NetworkMetricCreateRequest()
                            {
                                Time = time4.ToString("dd\\.hh\\:mm\\:ss")
                            };
                            try
                            {
                                AllNetworkMetricsResponse networkMetricsResponse = metricsAgentClient.GetNetworkMetricsAsync(networkMetricsRequest).Result;
                                foreach (NetworkMetricDto networkMetric in networkMetricsResponse.Metrics)
                                {
                                    Console.WriteLine($"{TimeSpan.Parse(networkMetric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {networkMetric.Value}");
                                }
                                Console.ReadKey();
                                Console.Clear();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Произошла ошибка при попытке получить CPU метрики. \n{ex.Message}");
                            }
                            break;
                        case 5:
                            TimeSpan time5 = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                            RamMetricCreateRequest ramMetricsRequest = new RamMetricCreateRequest()
                            {
                                Time = time5.ToString("dd\\.hh\\:mm\\:ss")
                            };
                            try
                            {
                                AllRamMetricsResponse ramMetricsResponse = metricsAgentClient.GetRamMetricsAsync(ramMetricsRequest).Result;
                                foreach (RamMetricDto ramMetric in ramMetricsResponse.Metrics)
                                {
                                    Console.WriteLine($"{TimeSpan.Parse(ramMetric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {ramMetric.Value}");
                                }
                                Console.ReadKey();
                                Console.Clear();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Произошла ошибка при попытке получить CPU метрики. \n{ex.Message}");
                            }
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Номер задачи введен не корректно. \nПожалуйста, повторите ввод данных");
                }
            }
        }
    }
}
