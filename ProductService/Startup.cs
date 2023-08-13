using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProductService.Data;
using ProductService.Models;
using ProductService.Rabbit;
using Serilog;
using System;

namespace ProductService
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductsService", Version = "v1" });
            });
            services.AddDbContext<AddProductDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ProductServices>();
            services.AddMassTransit(config =>
            {
                config.AddConsumer<OrderRequestHandler>();
                config.AddConsumer<SearchRequestHandler>();
                config.UsingRabbitMq((context, cfg) =>
                {
                    var rabbitMqConfig = Configuration.GetSection("RabbitMQ");

                    cfg.ReceiveEndpoint("SearchConsumerQueue", x =>
                    {
                        x.ConfigureConsumer<SearchRequestHandler>(context);
                        x.ConfigureConsumeTopology = false;
                        x.Bind("SearchConsumerExchange");
                    });
                    cfg.ReceiveEndpoint("ListProductsOrderConsumer", x =>
                    {
                        x.ConfigureConsumer<OrderRequestHandler>(context);
                        x.ConfigureConsumeTopology = false;
                        x.Bind("ListProductsOrderConsumerExchange");
                    });
                    cfg.Host(new Uri(rabbitMqConfig["Hostname"]), h =>
                    {
                        h.Username(rabbitMqConfig["Username"]);
                        h.Password(rabbitMqConfig["Password"]);
                    });
                });

            });
            Log.Logger = new LoggerConfiguration()
          .WriteTo.File("logses.txt", rollingInterval: RollingInterval.Day)
          .CreateLogger();
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductsService v1"));
            }

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
