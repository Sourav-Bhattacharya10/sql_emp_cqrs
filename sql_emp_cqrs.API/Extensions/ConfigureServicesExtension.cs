using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sql_emp_cqrs.Application.Core;
using sql_emp_cqrs.Application.Employees;
using sql_emp_cqrs.Persistence;
using System;

namespace sql_emp_cqrs.API
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(opt => 
            {
                var connectionString = config.GetValue<string>("SqlServer2017ConnectionString");
                var userID = config.GetValue<string>("SqlServer2017UserID");
                var dbPassword = config.GetValue<string>("SqlServer2017Password");
                
                var builder = new SqlConnectionStringBuilder(connectionString);
                builder.UserID = userID;
                builder.Password = dbPassword; // dotnet user-secrets set "DbPassword" "***value***"
                opt.UseSqlServer(builder.ConnectionString);
            });

            services.AddMediatR(typeof(GetAll.Handler).Assembly);

            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            
            return services;
        }
    }
}