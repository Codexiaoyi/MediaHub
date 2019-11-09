using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MediaHub.Extensions
{
    public static class SwaggerService
    {
        public static void AddSwaggerService(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var basePath = AppContext.BaseDirectory;
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

        }
    }
}
