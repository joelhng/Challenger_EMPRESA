using FteamLibrary.Interfaces;
using FteamLibrary.MongoSetting;
using FteamLibrary.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FteamApi
{
    public class Startup
    {
        readonly string GymCors = "GymCors";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<FteamLibrary.MongoSetting.GimnasioSettings>(Configuration.GetSection(nameof(GimnasioSettings)));
            services.AddSingleton<ISettingsMDB>(x => x.GetRequiredService<IOptions<GimnasioSettings>>().Value);
            services.AddSingleton<GimnasioService>();
            services.AddSingleton<UsuarioService>();
            services.AddSingleton<EjercicioService>();
            services.AddSingleton<RutinaService>();
            services.AddSingleton<HorariosService>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: GymCors,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                  });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = Configuration["Jwt:Issuer"],
                     ValidAudience = Configuration["Jwt:Issuer"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))

                 };
             });


            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FteamApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FteamApi v1"));
            }

            //TODO: Descomentar cuando el servidor sea https
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(GymCors);

            app.UseAuthentication();
            app.UseAuthorization();


            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), @"Resources"));
            }


            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
