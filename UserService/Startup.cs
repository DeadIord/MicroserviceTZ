using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using Serilog;
using System;
using System.Text;
using UserService.Data;
using UserService.Models;
using UserService.Rabbit;
using static MassTransit.Logging.OperationName;

namespace UserService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserService", Version = "v1" });
            });
            services.AddDbContext<AddDbContext>(options =>
            options.UseSqlServer(
            Configuration.GetConnectionString("UserConnection"),
            sqlOptions => sqlOptions.CommandTimeout(60)));
            services.AddScoped<UserServices>();
            services.AddMassTransit(config =>
            {
                config.AddConsumer<SearchRequestHandler>().Endpoint(configure: x => x.Name = "searchconsumer");
                config.AddRequestClient<SearchRequest>(destinationAddress: new Uri("exchange:searchconsumer"));
                config.UsingRabbitMq((context, cfg) =>
                {
                    var rabbitMqConfig = Configuration.GetSection("RabbitMQ");
                    cfg.Host(new Uri(rabbitMqConfig["Hostname"]), h =>
                    {
                        h.Username(rabbitMqConfig["Username"]);
                        h.Password(rabbitMqConfig["Password"]);
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });
            services.AddScoped<IPublishEndpoint>(provider =>
            {
                var busControl = provider.GetRequiredService<IBusControl>();
                return busControl;
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Jwt:SecretKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserService v1"));
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
        }
    }
}