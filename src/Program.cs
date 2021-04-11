using System;
using src.DependencyInjection;
using src.Services.GetGuidService;
using src.Services.NameService;
using src.Services.RandomGuidGeneratorService;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello DependencyInjection");
            var container = new DependencyInjectionContainer();
            var resolver = new DependencyResolver(container);
            //Add dependency    
            container.AddSingleton<Name>(new Name());
            container.AddSingleton<IRandomGuidGenerator>(new RandomGuidGenerator(resolver.GetService<Name>())); 
            container.AddTransient<IGetGuid>(new GetGuid(resolver.GetService<IRandomGuidGenerator>()));
            //Resolve dependency 
            Console.WriteLine("Singleton Example");
            resolver.GetService<IGetGuid>().GetGuidFromService();
            resolver.GetService<IGetGuid>().GetGuidFromService();
        }
    }
}
