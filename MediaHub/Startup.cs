using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediaHub.Common;
using MediaHub.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace MediaHub
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("mh1", new Info
                {
                    Version = "v1.0.0",
                    Title = "MediaHub API",
                    Description = "Api说明文档",
                    TermsOfService = "None",
                });

                //这里配置可以将注释显示再swagger中
                var xmlPath = Path.Combine(basePath, "MediaHub.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改
                var xmlModelPath = Path.Combine(basePath, "MediaHubModel.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlModelPath);//默认的第二个参数是false，这个是controller的注释，记得修改

                //添加header验证信息
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    { "MediaHub", new string[] { } }
                };
                c.AddSecurityRequirement(security);
                //方案名称“Blog.Core”可自定义，上下一致即可
                c.AddSecurityDefinition("MediaHub", new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });
            });

            #endregion

            #region Authorization
            //读取配置文件
            var audienceConfig = Configuration.GetSection("Audience");
            var symmetricKeyAsBase64 = audienceConfig["PrivateKey"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            // 令牌验证参数
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Issuer"],//发行人
                ValidateAudience = true,
                ValidAudience = audienceConfig["Audience"],//订阅人
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30),//总的有效时间是 JwtRegisteredClaimNames.Exp + ClockSkew，这里是30s+150s
                RequireExpirationTime = true,
            };

            //2.1【认证】、core自带官方JWT认证
            // 开启Bearer认证
            services.AddAuthentication("Bearer")
             // 添加JwtBearer服务
             .AddJwtBearer(o =>
             {
                 o.TokenValidationParameters = tokenValidationParameters;
                 o.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         // 如果过期，则把<是否过期>添加到，返回头信息中
                         if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                         {
                             context.Response.Headers.Add("Token-Expired", "true");
                         }
                         return Task.CompletedTask;
                     }
                 };
             });
            #endregion

            services.AddSingleton(new Appsettings(Env.ContentRootPath));
            services.AddDbContext<MyContext>(options =>
            {
                var sqlServerString = Appsettings.GetJsonString(new string[] { "ConnectionStrings", "SqlServer" });
                options.UseSqlServer(sqlServerString, b => b.MigrationsAssembly("MediaHub.Data"));
            });

            #region Autofac
            //让Autofac接管ConfigureServices
            //实例化 Autofac 容器
            var builder = new ContainerBuilder();

            //注册要反射的组件
            //builder.RegisterType<BaseRepository>().As<IBaseRepository>();

            //反射集注入
            var repositoryDllFile = Path.Combine(basePath, "MediaHub.Repository.dll");
            var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces();

            //将services填充到Autofac容器生成器中
            builder.Populate(services);

            //使用已进行的组件登记创建新容器
            var ApplicationContainer = builder.Build();
            #endregion

            return new AutofacServiceProvider(ApplicationContainer);//Autofac接管 core的内置DI容器
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/mh1/swagger.json", "ApiHelp v1.0.0");
                c.RoutePrefix = "";
            });
            #endregion

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
