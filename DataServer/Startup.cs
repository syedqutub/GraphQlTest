using DataServer.Data;
using DataServer.Data.DataLoaders;
using DataServer.Data.Mutations;
using DataServer.Data.Queries;
using DataServer.Data.Subscriptions;
using DataServer.Models.Types;
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

            services.AddPooledDbContextFactory<ApplicationDbContext>(options => options.UseSqlite("Data Source=conferences.db"));

            services.AddScoped(provider => provider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext());

            services.AddGraphQLServer()
                .AddQueryType(d => d
                        .Name(OperationTypeNames.Query)
                        .Field("version")
                        .Resolve(typeof(IResolverContext).Assembly.GetName().Version?.ToString()))
                    .AddTypeExtension<SpeakerQueries>()
                    .AddTypeExtension<SessionQueries>()
                    .AddTypeExtension<TrackQueries>()
                    .AddTypeExtension<AttendeeQueries>()
                .AddMutationType(d => d.Name(OperationTypeNames.Mutation))
                    .AddTypeExtension<SpeakerMutations>()
                    .AddTypeExtension<SessionMutations>()
                    .AddTypeExtension<TrackMutations>()
                    .AddTypeExtension<AttendeeMutations>()
                .AddSubscriptionType(d => d.Name(OperationTypeNames.Subscription))
                    .AddTypeExtension<SessionSubscriptions>()
                    .AddTypeExtension<AttendeeSubscriptions>()
                .AddType<SpeakerType>()
                .AddType<SessionType>()
                .AddType<AttendeeType>()
                .AddType<TrackType>()
                .AddProjections()
                .AddFiltering()
                .AddSorting()
                //.EnableRelaySupport()
                .AddGlobalObjectIdentification()
                // ×¢²áÊý¾Ý¼ÓÔØÆ÷
                .AddDataLoader<SpeakerByIdDataLoader>()
                .AddDataLoader<SessionByIdDataLoader>()
                .AddDataLoader<AttendeeByIdDataLoader>()
                .AddDataLoader<TrackByIdDataLoader>()
                .AddInMemorySubscriptions();
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
