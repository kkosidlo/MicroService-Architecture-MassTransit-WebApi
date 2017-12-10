using SampleApp.Messaging;
using SampleApp.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SampleApp.Web.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpGet]
        public async Task TestMethod()
        {
            UserViewModel model =
                new UserViewModel
                {
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Address = "Street 23",
                    Age = 23,
                    IdNumber = 232323
                };

            var bus = BusConfigurator.ConfigureBus();

            var sendToUri = new Uri($"{Constants.RabbitMqUri}" + $"{Constants.RegisterUserServiceQueue}");
            var endPoint = await bus.GetSendEndpoint(sendToUri);


            // no need to have a class implementing interface ( as we needed one during the implementation for rabbitmq
            await endPoint.Send<IRegisterUserCommand>(
                new
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    Age = model.Age,
                    IdNumber = model.IdNumber
                });

        }
    }
}
