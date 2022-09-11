using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Lib.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Database.Lib.Extensions
{
    public static class ServiceDatabaseExtensions
    {
        public static void AddIdentityDbService(this IServiceCollection services, string connectionString)
        {
            services.AddTransient(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>();
                optionsBuilder
                    .UseSqlServer(connectionString,
                        options =>
                        {
                            options.MigrationsHistoryTable("__EFMigrationsHistory", IdentityDbContext.Schema);
                        });
                //.UseQueryHints();
                return new IdentityDbContext(optionsBuilder.Options);
            });
        }
    }
}