using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MaSuperAgence.Api
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();

    //.UseKestrel(l => {
    //  l.Limits.MaxRequestBodySize = 524288;
    //});
  }
}
