﻿using SampleApp.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace SampleApp.NotificationService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Notification";

            var bus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host, Constants.NotificationServiceQueue, e =>
                {
                    e.Consumer<UserRegisteredConsumer>();
                });
            });

            bus.Start();
            Console.WriteLine("Listening for user registered events.. Press enter to exit");
            Console.ReadLine();

            bus.Stop();
        }
    }
}
