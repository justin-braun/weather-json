using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherjson;

namespace weather_json_console
{
    class Program
    {
        public static DateTime LastRun;

        static void Main(string[] args)
        {

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 300000; // 5 minutes
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Start();

            LastRun = DateTime.Now;
            LastRun = new DateTime(2019, 1, 28, 13, 00, 00);
            
            Console.WriteLine($"Timer started at {DateTime.Now.ToString()}.");

            Timer_Elapsed(null,null);

            Console.Read();

        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            

            Console.WriteLine($"Timer elapsed at {DateTime.Now.ToString()}.");

            CheckAlerts();

        }

        private static void CheckAlerts()
        {

            weatherjson.WeatherAlerts wa = new WeatherAlerts();
            weatherjson.AlertSourceList alerts;

            try
            {
                alerts = wa.GetWeatherAlerts(LastRun, WeatherAlerts.AlertFilterType.Alerts,WeatherAlerts.AlertSortOrder.Descending);

                Console.WriteLine($"Checking for new alerts since {LastRun.ToString()}...");


                if (alerts.AlertItems.Count() > 0)
                {
                    foreach (var alert in alerts.AlertItems)
                    {
                        //Console.WriteLine($"{alert.AlertInfo.MessageType.ToUpper()}: {alert.AlertInfo.EventName} for {Helpers.FormatCountyNames(alert.AlertInfo.AffectedCounties, alert.AlertInfo.Geocodes.UGC)} effective {alert.AlertInfo.EffectiveTime.Value.ToString()}{Environment.NewLine}");
                        Console.WriteLine($"{alert.AlertInfo.EventName} for {Helpers.FormatCountyNames(alert.AlertInfo.CountyDetails, true)} until {alert.AlertInfo.EndsTime.ToString()}");
                    }

                    Console.WriteLine($"There are {alerts.AlertItems.Count().ToString()} alerts since the last check.");

                    
                }
                else
                {
                    Console.WriteLine("There are no new alerts since the last check.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }

            LastRun = DateTime.Now;

            Console.Read();
        }
    }
}
