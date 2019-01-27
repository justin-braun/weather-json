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

            LastRun = DateTime.UtcNow;
            //DEBUG LastRun = new DateTime(2019, 1, 26, 14, 00, 00);
            
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
            weatherjson.AlertList alerts = wa.GetWeatherAlerts();

            Console.WriteLine($"Checking for new alerts since {LastRun.ToString()}...");


            if (alerts.features.Count() > 0)
            {
                foreach (var alert in alerts.features)
                {
                    //Console.WriteLine($"Checking if {alert.properties.sent.Value.ToString()} > {LastRun.ToString()}...");

                    if (alert.properties.sent.Value > LastRun)
                    {
                        Console.WriteLine($"NEW ALERT: {alert.properties.Event} for {alert.properties.areaDesc} effective {alert.properties.effective.Value.ToString()}{Environment.NewLine}");
                    }
                    //else
                    //{
                    //    Console.WriteLine("Alert occurred before last run.");
                    //}
                }
            }
            else
            {
                Console.WriteLine("There are no new alerts since the last check.");
            }

            Console.WriteLine($"There are {alerts.features.Count().ToString()} active alerts.");

            LastRun = DateTime.Now;

            Console.Read();
        }
    }
}
