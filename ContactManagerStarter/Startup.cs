using ElmahCore.Mvc;

namespace ContactManagerStarter
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddElmah();
        }

        public void configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseElmah();
        }
    }
}
