using Autofac;
using DIExampleServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjectionExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICitiesService _citiesService1;
        private readonly ICitiesService _citiesService2;
        private readonly ICitiesService _citiesService3;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILifetimeScope _lifetimeScope; // Demonstrating the use of Autofac

        //Constructor
        //Demonstrating Constructor injection
        public HomeController(
            ICitiesService citiesService1, 
            ICitiesService citiesService2, 
            ICitiesService citiesService3,
            IServiceScopeFactory serviceScopeFactory,
            ILifetimeScope lifetimeScope // Demonstrating the use of Autofac
        )
        {
            _citiesService1 = citiesService1;
            _citiesService2 = citiesService2;
            _citiesService3 = citiesService3;
            _serviceScopeFactory = serviceScopeFactory;
            _lifetimeScope = lifetimeScope;
        }


        [Route("/")]
        //Demonstrating Method Injection
        public IActionResult Index([FromServices] ICitiesService __citiesService)
        {
            List<string> cities = __citiesService.GetCities();

            ViewBag.InstanceId_CitiesService_1 = _citiesService1.ServiceInstanceId;
            ViewBag.InstanceId_CitiesService_2 = _citiesService2.ServiceInstanceId;
            ViewBag.InstanceId_CitiesService_3 = _citiesService3.ServiceInstanceId;
            ViewBag.InstanceId_CitiesService_Method = __citiesService.ServiceInstanceId;

            //Demonstrating the creation of child scope using Microsoft DI
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                //Logic to Inject CityService
                ICitiesService citiesService = scope.ServiceProvider.GetRequiredService<ICitiesService>();

                // TODO: Logic to do database work

                ViewBag.InstanceId_CitiesService_InScope = citiesService.ServiceInstanceId;

            } // End of scope, the Dispose method of CitiesService would get called automatically.

            //Demonstrating the creation of child scope using Autofac
            using(ILifetimeScope scope = _lifetimeScope.BeginLifetimeScope())
            {
                //Logic to Inject CityService
                ICitiesService citiesService = scope.Resolve<ICitiesService>();

                // TODO: Logic to do database work

                ViewBag.InstanceId_CitiesService_InAutofacScope = citiesService.ServiceInstanceId;
            }

            return View(cities);
        }
    }
}
