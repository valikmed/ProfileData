using System;
using Application;
using Domain.Abstractions.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProfileData.Domain.Abstractions.Services;

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
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddControllers();


        //    services.AddCors(options =>
        //    {
        //        options.AddPolicy(MyAllowSpecificOrigins,
        //        builder =>
        //        {
        //            builder.WithOrigins("http://localhost:4200",
        //                                "http://www.contoso.com")
        //                        .AllowAnyHeader()
        //                        .AllowAnyMethod();
        //        });
        //    });

        //    services.Configure<FormOptions>(o => {
        //        o.ValueLengthLimit = int.MaxValue;
        //        o.MultipartBodyLengthLimit = int.MaxValue;
        //        o.MemoryBufferThreshold = int.MaxValue;
        //    });
        //    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        //    services.AddSwaggerGen(c =>
        //    {
        //        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        //    });

        //    var mappingConfig = new MapperConfiguration(mc =>
        //    {
        //        mc.AddProfile(new MappingProfile());
        //    });


        //    IMapper mapper = mappingConfig.CreateMapper();
        //    services.AddSingleton(mapper);

        //    var connection = Configuration.GetConnectionString("DatabaseConection");
        //    services.AddDbContext<ProfileDataContext>(e => e.UseSqlServer(connection));

        //    services.AddScoped<IUserService, UserService>();

        //    services.AddScoped<IRoleService, RoleService>();

        //    services.AddScoped<IAvatarService, AvatarService>();

        //    services.AddScoped<IUnitOfWork, UnitOfWork>();
        //}

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


