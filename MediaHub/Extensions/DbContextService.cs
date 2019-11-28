using MediaHub.Common;
using MediaHub.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaHub.Extensions
{
    public static class DbContextService
    {
        public static void AddDbContextService(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddDbContext<MyContext>(options =>
            {
                var sqlServerString = Appsettings.GetJsonString(new string[] { "ConnectionStrings", "MySqlConnection" });
                options.UseMySQL(sqlServerString, b => b.MigrationsAssembly("MediaHub.Data"));
            });
        }
    }
}
