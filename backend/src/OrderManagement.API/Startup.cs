using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderManagement.Domain.Repositories;
using OrderManagement.Infra.Repository;
using OrderManagement.Infra.DBDataContext;
using OrderManagement.Shared.Commands;
using OrderManagement.Shared.Interface;
using OrderManagement.Application.UseCase;
using Microsoft.AspNetCore.Cors.Infrastructure;
using OrderManagement.API.Hubs;
using OrderManagement.API.Services;

namespace OrderManagement.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly string CORSPolicy = "CorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSignalR();
            
            services.AddCors(options =>
            {
                options.AddPolicy(CORSPolicy, policy =>
                    policy.WithOrigins("http://localhost:5173")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());
            });

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICommandResult, CommandResult>();
            services.AddScoped<DeleteOrderUseCase, DeleteOrderUseCase>();
            services.AddScoped<GetAllOrderUseCase, GetAllOrderUseCase>();
            services.AddScoped<GetOrderByIdUseCase, GetOrderByIdUseCase>();
            services.AddScoped<InsertOrderUseCase, InsertOrderUseCase>();
            services.AddScoped<UpdateOrderUseCase, UpdateOrderUseCase>();
            services.AddScoped<IOrderNotifier, SignalROrderNotifier>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();  
            }

            app.UseCors(CORSPolicy);

            

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<OrderHub>("/orderhub");
            });
        }
    }
}