using Microsoft.AspNetCore;
using Mockaccino.Sample;

IConfiguration Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddEnvironmentVariables()
            .Build();


BuildWebHost(args).Run();

IWebHost BuildWebHost(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .UseConfiguration(Configuration)
        .Build();


public partial class Program { }