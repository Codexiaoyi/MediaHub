﻿using MediaHub.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaHub.Extensions
{
    public static class AuthorizationService
    {
        public static void AddAuthorizationService(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            //读取配置文件
            var symmetricKeyAsBase64 = Appsettings.GetJsonString(new string[] { "Audience", "PrivateKey" });
            var issuer = Appsettings.GetJsonString(new string[] { "Audience", "Issuer" });
            var audience = Appsettings.GetJsonString(new string[] { "Audience", "Audience" });
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            // 令牌验证参数
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = issuer,//发行人
                ValidateAudience = true,
                ValidAudience = audience,//订阅人
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
        }
    }
}
