using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SimpleGrader;

var builder = Host.CreateDefaultBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();

builder.ConfigureServices(services => services.AddSimpleGraderServices(configuration));

var app = builder.Build();

app.Run();