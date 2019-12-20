using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaHub.AuthorizeHelper.Jwt;
using MediaHub.Data.SeedData;
using MediaHub.IRepository;
using MediaHub.Model;
using MediaHub.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaHub.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("login")]
        public async Task<ActionResult> GetToken(string userName, string userPassword)
        {
            string tokenStr = string.Empty;
            bool suc = false;
            //这里就是用户登陆以后，通过数据库去调取数据，分配权限的操作
            //var user = TemporaryData.GetUser(userName);
            var user = await _userRepository.QuaryUserByName(userName);
            if (user != null && user.Password.Equals(userPassword))
            {
                tokenStr = JwtHelper.GetToken(user);
                suc = true;
            }
            else
            {
                tokenStr = "身份验证失败!";
            }

            return Ok(new
            {
                success = suc,
                token = tokenStr
            });
        }

        /// <summary>
        /// 注册接口
        /// </summary>
        /// <param name="mediaHubUserViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] MediaHubUserViewModel mediaHubUserViewModel)
        {
            bool suc = false;

            if (ModelState.IsValid)
            {
                var newUser = new MediaHubUser
                {
                    UserName = mediaHubUserViewModel.UserName,
                    Password = mediaHubUserViewModel.Password,
                    Email = mediaHubUserViewModel.Email
                };

                var result = await _userRepository.AddAsync(newUser);
                if (result > 0)
                {
                    suc = true;//注册成功
                }
            }

            return Ok(new
            {
                success = suc,
            });
        }
    }
}