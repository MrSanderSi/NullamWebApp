var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("db").WithPgAdmin();

var nullamdb = db.AddDatabase("nullamdb");

var apiService = builder.AddProject<Projects.NullamWebApp_ApiService>("apiservice")
    .WithReference(nullamdb);

builder.AddProject<Projects.NullamWebApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
