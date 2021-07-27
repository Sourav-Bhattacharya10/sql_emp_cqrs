using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sql_emp_cqrs.Persistence;

namespace sql_emp_cqrs.API
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(opt => 
            {
                var builder = new SqlConnectionStringBuilder(config.GetConnectionString("DefaultConnection"));
                builder.Password = config["DbPassword"]; // dotnet user-secrets set "DbPassword" "***value***"
                opt.UseSqlServer(builder.ConnectionString);
            });
            
            return services;
        }
    }
}