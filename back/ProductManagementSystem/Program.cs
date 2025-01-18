
using ProductManagement.WebAPI.Configuration;





var builder = WebApplication.CreateBuilder(args);

ServiceConfiguration.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

MiddlewareConfiguration.ConfigureMiddleware(app);


app.Run();
