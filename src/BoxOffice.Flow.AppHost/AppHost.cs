var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.BoxOffice_Flow>("boxoffice-flow");

await builder.Build().RunAsync();
