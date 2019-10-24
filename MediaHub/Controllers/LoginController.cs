using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaHub.AuthorizeHelper.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaHub.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet]
        [Route("token")]
        public ActionResult GetToken(string userName,string password)
        {
            string tokenStr = string.Empty;
            bool suc = false;
            //这里就是用户登陆以后，通过数据库去调取数据，分配权限的操作

            var user = "Admin";
            if (user != null)
            {
                TokenModel tokenModel = new TokenModel { Uid = 1, Role = user };
                tokenStr = JwtHelper.GetToken(tokenModel);
                suc = true;
            }
            else
            {
                tokenStr = "login fail!!!";
            }

            return Ok(new
            {
                success = suc,
                token = tokenStr
            });
        }
    }
}