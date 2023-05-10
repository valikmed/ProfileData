using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http.Features;
using Domain.Abstractions;
using Domain.Abstractions.Services;
using Domain.Mapping;
using Infrastructure;
using ProfileData.Domain.Abstractions.Services;
using Application;
using ProfileData;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using Infrastructure.Authorization;

namespace ProfileData
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DatabaseConection");
            services.AddDbContext<ProfileDataContext>(e => e.UseSqlServer(connection));


            services.AddControllers();


            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                });
            });

            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ControllerAccessPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser()
                          .RequireClaim("role", "C-Level");
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

                // Тут добавляється конфігурація з Авторизацією
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT", // Формат нашого токена
                    In = ParameterLocation.Header, // Кажемо що наш токен буде весь час в Хедері
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                // Тут створюється схема - як виглядає токен зсередини
                var securityScheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };

                // Тут добавляється створена вище схема до свагера через requirements об'єкт
                // - Викликається c.AddSecurityRequrement(І тут наш об'єкт requirements)
                var requirements = new OpenApiSecurityRequirement();
                requirements.Add(securityScheme, new List<string>());
                c.AddSecurityRequirement(requirements);
            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });


            // Добавити авторизацію для ендпоінтів
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => // Кажемо що добавлямо `Bearier` токен (В хедерах запитів)
                {
                    // Чи відправляти дані лише по HTTPS (Бажано поставити true)
                    // false - зазвичай для тестування ставлять
                    options.RequireHttpsMetadata = false;

                    // Задаємо параметри валідації.
                    // Там ми будемо читати деякі параметри з констант классу `AuthOptions`
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true, // Чи буде перевірятись дата придатності
                        ValidateIssuerSigningKey = true,

                        // Настроюємо Автора токенів та користувача. Дані беремо з `AuthOptions` класу
                        ValidIssuer = AuthOptions.ISSUER,
                        ValidAudience = AuthOptions.AUDIENCE,

                        // Ключ безпеки. Беремо симетричну верісію з нашого `AuthOptions` класу
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()

                    };

                });



            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);



            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IAvatarService, AvatarService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();


            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



        }
    }
}
