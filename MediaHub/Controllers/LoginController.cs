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
    [Route("api/Login")]
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
        public ActionResult GetToken(string userName, string password)
        {
            string tokenStr = string.Empty;
            bool suc = false;
            //这里就是用户登陆以后，通过数据库去调取数据，分配权限的操作
            var user = TemporaryData.GetUser(userName);
            if (user != null && user.Password.Equals(password))
            {
                tokenStr = JwtHelper.GetToken(user);
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