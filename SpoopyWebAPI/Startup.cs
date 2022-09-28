using Microsoft.AspNetCore.Builder;
using SpoopyWebAPI.Models;

namespace SpoopyWebAPI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpLogging();

            app.UseHttpsRedirection();           

            app.UseSession();

            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSession();
            services.AddHttpLogging((options) => {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.Request;
            });
        }
    }
}
