using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;


namespace TaosPerformanceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var host = CreateWebHost(args);
            host.Run();
        }

        public static IWebHost CreateWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
