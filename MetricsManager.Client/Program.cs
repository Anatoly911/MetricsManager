using System;
using System.Net.Http;

namespace MetricsManager.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MetricsManagerClient metricsManagerClient = new MetricsManagerClient("https://localhost:5001", new HttpClient());
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
                            TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                            TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);
                            CpuMetricsRequest cpuMetricsRequest = new CpuMetricsRequest()
                            {
                                AgentId = 1,
                                FromTime = fromTime.ToString("dd\\.hh\\:mm\\:ss"),
                                ToTime = toTime.ToString("dd\\.hh\\:mm\\:ss"),
                            };
                            try
                            {
                                CpuMetricsResponse cpuMetricsResponse = metricsManagerClient.GetCpuMetricsFromAgentAsync(cpuMetricsRequest).Result;
                                foreach (CpuMetric cpuMetric in cpuMetricsResponse.Metrics)
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
                            TimeSpan toTime2 = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                            TimeSpan fromTime2 = toTime2 - TimeSpan.FromSeconds(60);
                            DotNetMetricsRequest dotNetMetricsRequest = new DotNetMetricsRequest()
                            {
                                AgentId = 2,
                                FromTime = fromTime2.ToString("dd\\.hh\\:mm\\:ss"),
                                ToTime = toTime2.ToString("dd\\.hh\\:mm\\:ss"),
                            };
                            try
                            {
                                DotNetMetricsResponse dotNetMetricsResponse = metricsManagerClient.GetDotNetMetricsFromAgentAsync(dotNetMetricsRequest).Result;
                                foreach (DotNetMetric dotNetMetric in dotNetMetricsResponse.Metrics)
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
                            TimeSpan toTime3 = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                            TimeSpan fromTime3 = toTime3 - TimeSpan.FromSeconds(60);
                            HddMetricsRequest hddMetricsRequest = new HddMetricsRequest()
                            {
                                AgentId = 3,
                                FromTime = fromTime3.ToString("dd\\.hh\\:mm\\:ss"),
                                ToTime = toTime3.ToString("dd\\.hh\\:mm\\:ss"),
                            };
                            try
                            {
                                HddMetricsResponse hddMetricsResponse = metricsManagerClient.GetHddMetricsFromAgentAsync(hddMetricsRequest).Result;
                                foreach (HddMetric hddMetric in hddMetricsResponse.Metrics)
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
                            TimeSpan toTime4 = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                            TimeSpan fromTime4 = toTime4 - TimeSpan.FromSeconds(60);
                            NetworkMetricsRequest networkMetricsRequest = new NetworkMetricsRequest()
                            {
                                AgentId = 4,
                                FromTime = fromTime4.ToString("dd\\.hh\\:mm\\:ss"),
                                ToTime = toTime4.ToString("dd\\.hh\\:mm\\:ss"),
                            };
                            try
                            {
                                NetworkMetricsResponse networkMetricsResponse = metricsManagerClient.GetNetworkMetricsFromAgentAsync(networkMetricsRequest).Result;
                                foreach (NetworkMetric networkMetric in networkMetricsResponse.Metrics)
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
                            TimeSpan toTime5 = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                            TimeSpan fromTime5 = toTime5 - TimeSpan.FromSeconds(60);
                            RamMetricsRequest ramMetricsRequest = new RamMetricsRequest()
                            {
                                AgentId = 5,
                                FromTime = fromTime5.ToString("dd\\.hh\\:mm\\:ss"),
                                ToTime = toTime5.ToString("dd\\.hh\\:mm\\:ss"),
                            };
                            try
                            {
                                RamMetricsResponse ramMetricsResponse = metricsManagerClient.GetRamMetricsFromAgentAsync(ramMetricsRequest).Result;
                                foreach (RamMetric ramMetric in ramMetricsResponse.Metrics)
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
