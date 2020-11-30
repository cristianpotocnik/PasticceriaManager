using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pasticceria.Data;
using Pasticceria.Repository;
using Pasticceria.Services;

namespace Pasticceria
{
    public class StartupServices
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // Add transient db context
            services.AddTransient<DbContext, ApplicationDbContext>();

            // Add transient repository
            services.AddTransient<IDolciRepository, DolciRepository>();
            services.AddTransient<IIngredientiRepository, IngredintiRepository>();
            services.AddTransient<IDolceIngredienteRepository, DolceIngredienteRepository>();
            services.AddTransient<IArticoloService, ArticoloService>();
        }
    }
}
