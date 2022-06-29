using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Northwind.Services;
using System.Threading.Tasks;

namespace Example
{
    public class Program
    {
        public static Task Main(string[] args) {
            var runtimeHost = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder => 
                    builder.UseStartup<Startup>()
                    //builder.UseStartup<StartupStarWars>()
                )
                .Build();
                
            ServiceResolver.Setup(runtimeHost.Services);

            return runtimeHost.RunAsync();
        }
    }
}
