using ProductManagement.WebAPI.Endpoints;
using ProductManagement.WebAPI.Middleware;

namespace ProductManagement.WebAPI.Configuration
{
    public static class MiddlewareConfiguration
    {
        public static void ConfigureMiddleware(WebApplication app)
        {

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseStaticFiles();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowSpecificOrigin");

            app.UseHttpsRedirection();
            app.MapProductEndpoints();

        }
    }
}
