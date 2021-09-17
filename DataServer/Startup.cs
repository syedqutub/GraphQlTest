using DataServer.Data;
using DataServer.Data.Queries;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace DataServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=conferences.db"));

            services.AddGraphQLServer()
                .AddQueryType(d => d
                        .Name(OperationTypeNames.Query)
                        .Field("version")
                        .Resolve(typeof(IResolverContext).Assembly.GetName().Version?.ToString()))
                    .AddTypeExtension<SpeakerQueries>()
                .AddProjections()
                .AddFiltering()
                .AddSorting()
                //.EnableRelaySupport()
                // ×¢²áÊý¾Ý¼ÓÔØÆ÷
                .InitializeOnStartup();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.Use(static async (context, next) =>
                {
                    try
                    {
                        await next();
                    }
                    catch(Exception ex)
                    {
                        throw;
                    }
                });
            }

            app.UseHttpsRedirection();

            app.UseWebSockets();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGraphQL();
            });
        }
    }
}
