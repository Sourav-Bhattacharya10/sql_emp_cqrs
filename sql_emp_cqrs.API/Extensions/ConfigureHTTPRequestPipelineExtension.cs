using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace sql_emp_cqrs.API
{
    public static class ConfigureHTTPRequestPipelineExtension
    {
        public static IApplicationBuilder AddApplicationMiddlwares(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            return app;
        }
    }
}