var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.BoxOffice_Flow>("boxoffice-flow");

builder.Build().Run();
