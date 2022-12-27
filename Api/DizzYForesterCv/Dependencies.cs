using Model.Database;

namespace WebAPI
{
    public class Dependencies
    {
        public Dependencies()
        {
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUoW, UoW>();
        }
    }
}
