using AutoMapper;
using FluentValidation.AspNetCore;
using MagicShortener.API.Infrastructure;
using MagicShortener.API.Infrastructure.Authentication;
using MagicShortener.Common.Configuration;
using MagicShortener.DataAccess;
using MagicShortener.DataAccess.Mongo;
using MagicShortener.DataAccess.Repositories;
using MagicShortener.Logic.Commands;
using MagicShortener.Logic.Commands.Links.CreateLink;
using MagicShortener.Logic.Commands.Links.IncrementLinkRedirectsCount;
using MagicShortener.Logic.Queries;
using MagicShortener.Logic.Queries.Links.ConvertShortUrlToFull;
using MagicShortener.Logic.Queries.Links.GetAllLinks;
using MagicShortener.Logic.Queries.Links.GetLink;
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
        private const string AllowAllPolicyName = "AllowAllPolicy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));

            //считаем, что на API - публичный. В противном случае надо выставить соответствующие опции
            services.AddCors(options => {
                options.AddPolicy(AllowAllPolicyName,
                        builder => builder.AllowAnyOrigin()
                                          .AllowAnyMethod()
                                          .AllowAnyHeader()
                                          .AllowCredentials());
            });


            // TODO: здесь весь bootstrap код для конфигурации аутентификации/авторизации
            // AddAuthentication с типом JwtBearerDefaults.AuthenticationScheme
            // синглтон для создания фабрики генерации токенов
            // получение настроек из конфига, создание ключа для подписи
            // формирование перечня TokenValidationParameters - время жизни и т д
            

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
            services.AddTransient<IUsersRepository, UsersRepository>();

            //команды
            services.AddTransient<ICommandHandler<CreateLinkCommand>, CreateLinkCommandHandler>();
            services.AddTransient<ICommandHandler<IncrementLinkRedirectsCountCommand>, IncrementLinkRedirectsCountCommandHandler>();

            //запросы
            services.AddTransient<IQueryHandler<GetLinkQuery, GetLinkQueryResult>, GetLinkQueryHandler>();
            services.AddTransient<IQueryHandler<GetAllLinksQuery, GetAllLinksQueryResult>, GetAllLinksQueryHandler>();
            services.AddTransient<IQueryHandler<ConvertShortUrlToFullQuery, ConvertShortUrlToFullQueryResult>, ConvertShortUrlToFullQueryHandler>();

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

            //для необработанных исключений регистрируем глобальный обработчик в пайплайне
            app.ConfigureExceptionHandler();

            app.UseCors(AllowAllPolicyName);

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
