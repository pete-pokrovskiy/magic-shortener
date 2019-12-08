using AutoMapper;
using FluentValidation.AspNetCore;
using MagicShortener.API.Infrastructure;
using MagicShortener.Common.Configuration;
using MagicShortener.DataAccess;
using MagicShortener.DataAccess.Mongo;
using MagicShortener.DataAccess.Repositories;
using MagicShortener.Logic.Commands;
using MagicShortener.Logic.Commands.Links.CreateLink;
using MagicShortener.Logic.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MagicShortener.API
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
            services.AddAutoMapper(typeof(Startup));

            services.AddMvc()
                .AddJsonOptions(opt =>
                    {
                        opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                        opt.SerializerSettings.DateParseHandling = Newtonsoft.Json.DateParseHandling.DateTime;
                        opt.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
                    })
                .AddFluentValidation(c => c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
;
            //конфигурация
            services.AddScoped<IMongoDbConfig, MongoDbConfig>();
            services.AddTransient<IMagicShortenerContext, MagicShortenerContext>();

            //репозитории
            services.AddTransient<ILinksRepository, LinksRepository>();
            services.AddTransient<ICountersRepository, CountersRepository>();

            //команды
            services.AddTransient<ICommandHandler<CreateLinkCommand>, CreateLinkCommandHandler>();


            //запросы


            //сервисы
            services.AddTransient<IUrlShorteningService, Base62ShorteningService>();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //считаем, что на API - публичный. В противном случае надо выставить соответствующие опции
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
            });

            //для необработанных исключений регистрируем глобальный обработчик в пайплайне
            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
