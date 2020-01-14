using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public LoginController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [Route("login")]
        public async Task<ActionResult> GetToken(string userAccount, string userPassword)
        {
            string tokenStr = string.Empty;
            bool suc = false;
            //这里就是用户登陆以后，通过数据库去调取数据，分配权限的操作
            //var user = TemporaryData.GetUser(userName);
            var user = await _userRepository.QueryUserByAccount(userAccount);
            if (user != null && user.Password.Equals(userPassword))
            {
                tokenStr = JwtHelper.GetToken(user);
                suc = true;
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
                //automapper映射
                var newUser = _mapper.Map<MediaHubUser>(mediaHubUserViewModel);

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