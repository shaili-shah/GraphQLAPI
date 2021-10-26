using GraphiQl;
using GraphQL;
using GraphQL.Types;
using GraphQLAPI.GraphqlCore;
using GraphQLAPI.GraphqlCore.participant;
using GraphQLAPI.Infrastructure.DBContext;
using GraphQLAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace GraphQLAPI
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

            services.AddControllers()
                 .AddJsonOptions(options =>
                 {
                     options.JsonSerializerOptions.WriteIndented = true;
                     options.JsonSerializerOptions.Converters.Add(new CustomJsonConverterForType());
                 });

            services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GraphQLAPI", Version = "v1" });
            });

            services.AddDbContext<TechEventDBContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("GraphQLDBConnection")));

            services.AddTransient<ITechEventRepository, TechEventRepository>();
            services.AddTransient<IDocumentExecuter, DocumentExecuter>();

            services.AddTransient<ParticipantType>();
            services.AddTransient<TechEventInfoType>();
            services.AddTransient<TechEventQuery>();
            services.AddTransient<TechEventInputType>();
            services.AddTransient<TechEventMutation>();


            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new TechEventSchema(new FuncDependencyResolver(type => sp.GetService(type))));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GraphQLAPI v1"));
            }

            app.UseGraphiQl("/graphql");

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
