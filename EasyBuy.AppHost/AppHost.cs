var builder = DistributedApplication.CreateBuilder(args);

var productApi = builder.AddProject<Projects.EasyBuy_ProductService>("ProductApi");
var orderApi = builder.AddProject<Projects.EasyBuy_OrderService>("OrderApi");

builder.AddProject<Projects.EasyBuy_Web>("WebApp")
    .WithReference(productApi)
    .WithReference(orderApi);

builder.Build().Run();