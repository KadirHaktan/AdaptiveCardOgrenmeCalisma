using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder.BotFramework;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WeatherAdaptiveCard.Bots;

namespace WeatherAdaptiveCard
{
    public class Startup
    {
        public string RootPath { get; set; }
        public IConfiguration configuration { get; set; }

        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            RootPath = env.ContentRootPath;
            this.configuration = configuration;
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(RootPath)
                .AddJsonFile("appsettings.json");


            var configuration = configurationBuilder.Build();



            services.AddBot<WeatherSimpleBot>((options) =>
            {
                options.CredentialProvider = new ConfigurationCredentialProvider(configuration);

            });

            services.AddMvc();

        }
    

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseMvc();
        app.UseBotFramework();
     }
   }
}