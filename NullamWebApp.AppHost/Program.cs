var builder = DistributedApplication.CreateBuilder(args);


var apiService = builder.AddProject<Projects.NullamWebApp_ApiService>("apiservice");

builder.AddProject<Projects.NullamWebApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
