using SampleApp.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace SampleApp.RegistrationService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Registration service";

            var bus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host,
                    Constants.RegisterUserServiceQueue, e =>
                    {
                        e.Consumer<RegisterUserCommandConsumer>();
                    });
            });

            bus.Start();

            Console.WriteLine("Listening for register user command.. ");

            Console.ReadLine();

            bus.Stop();
        }
    }
}
