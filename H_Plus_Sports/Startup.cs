﻿using H_Plus_Sports.Contracts;
using H_Plus_Sports.Models;
using H_Plus_Sports.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace H_Plus_Sports
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
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISalespersonRepository, SalespersonRepository>();

            /* cache from azure
            services.AddDistributedRedisCache( options =>
            {
                options.Configuration = Configuration.GetConnectionString("RedisConnection");
                options.InstanceName = "master";
            });
            */

            services.AddMvc();


            var connection = "Server=MAIN-PC\\SQLEXPRESS;Initial Catalog=H_Plus_Sports;Persist Security Info=False;User ID=Ninja;Password=258Ye707!;";
            services.AddDbContext<H_Plus_SportsContext>(options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<StackifyMiddleware.RequestTracerMiddleware>();

            app.UseMvc();
        }
    }
}
