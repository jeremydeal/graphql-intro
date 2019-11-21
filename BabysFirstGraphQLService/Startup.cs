using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabysFirstGraphQLService.Data.DAL;
using BabysFirstGraphQLService.Data.Models;
using BabysFirstGraphQLService.GraphTypes;
using BabysFirstGraphQLService.GraphTypes.Messaging;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Transports.AspNetCore;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Server.Ui.Playground;
using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace BabysFirstGraphQLService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddControllers();

            // config DAL
            services.AddSingleton<IMongoClient>(s =>
            {
                BsonClassMap.RegisterClassMap<WeatherForecast>();

                return new MongoClient("mongodb://mongo:27017/test?serverSelectionTimeoutMS=2000");
                //return new MongoClient();
            });
            services.AddScoped<IWeatherForecastData, WeatherForecastData>();
            services.AddScoped<ForecastMessageService>();

            // config GraphQL
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<WeatherSchema>();

            services.AddGraphQL(options =>
            {
                //options.EnableMetrics = true;
                options.ExposeExceptions = true;
            })
                .AddWebSockets() // Add required services for web socket support
                .AddGraphTypes(ServiceLifetime.Scoped);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(x => x.AllowAnyOrigin());

            app.UseWebSockets();

            // wire up GraphQL
            app.UseGraphQL<WeatherSchema>("/graphql");
            app.UseGraphQLWebSockets<WeatherSchema>("/graphql");

            // use graphiQL middleware at default url /graphiql
            //app.UseGraphiQLServer(new GraphiQLOptions());
            // use graphql-playground middleware at default url /ui/playground
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions
            {
                GraphQLEndPoint = "/graphql",
            });
            // use voyager middleware at default url /ui/voyager
            //app.UseGraphQLVoyager(new GraphQLVoyagerOptions());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
