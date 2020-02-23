using MediaHub.Common;
using MediaHub.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MediaHub.AuthorizeHelper.Jwt
{
    public class JwtHelper
    {

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetToken(User user)
        {
            string issuer = Appsettings.GetJsonString(new string[] { "Audience", "Issuer" });//获取发布人
            string audience = Appsettings.GetJsonString(new string[] { "Audience", "Audience" });//获取作者
            string privateKey = Appsettings.GetJsonString(new string[] { "Audience", "PrivateKey" });//获取私钥

            //创建声明
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Jti, user.UserAccount),
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                //这个就是过期时间，目前是过期1000秒，可自定义，注意JWT有自己的缓冲过期时间
                new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(180)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Iss,issuer),
                new Claim(JwtRegisteredClaimNames.Aud,audience)
            };

            //将一个用户的多个角色都加入到声明中
            //claims.AddRange(tokenModel.Role.Split(",").Select(x => new Claim(ClaimTypes.Role, x)));

            //加载密钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateKey));
            //密钥加入数字签名
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: issuer,
                claims: claims,
                signingCredentials: creds);

            var jwtHandler = new JwtSecurityTokenHandler();
            var token = jwtHandler.WriteToken(jwt);

            return token;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static TokenModel SerializeToken(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(token);
            object role;
            try
            {
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var tm = new TokenModel
            {
                Uid = (jwtToken.Id).ObjToInt(),
                Role = role != null ? role.ObjToString() : "",
            };
            return tm;
        }
    }

    /// <summary>
    /// 令牌格式
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Uid { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }
    }
}
