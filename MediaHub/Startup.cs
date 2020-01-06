using System;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using MediaHub.Common;
using MediaHub.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace MediaHub
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Env = env;
        }

        public IHostingEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //AutoMapper配置
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton(new Appsettings(Env.ContentRootPath));

            services.AddCorsService();
            services.AddSwaggerService();
            services.AddDbContextService();
            services.AddAuthorizationService();

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
                //app.UseExceptionHandler(appBuilder=> {
                //    appBuilder.Run(async context=>{
                //         context.Response.StatusCode = 500;
                //    });
                //});
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

            app.UseCors("LimitRequests");

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
