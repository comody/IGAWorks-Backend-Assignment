using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dfinery.backend.assignment.Service.messenger;
using dfinery.backend.assignment.Service.store;
using dfinery.backend.assignment.store.IEventStoreService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace dfinery.backend.assignment
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
            var queueUrl = Configuration["AWS:SQS:Url"];
            var eventSQSMessenger = new EventSQSMessenger(queueUrl);

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var eventStoreService = new EventStoreService(connectionString);

            services.AddSingleton<IEventSQSMessenger>(eventSQSMessenger);
            services.AddSingleton<IEventStoreService>(eventStoreService);
            services.AddControllersWithViews().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
