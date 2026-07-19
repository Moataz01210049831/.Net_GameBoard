using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyBGList.DTO;
using MyBGList.Models;
using MyBGList.servs;
using MyBGList.servs.Iservices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyBGList.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController(IAccountService _accountService, ILogger<APiUser> _logger, ApplicationDbContext context,IConfiguration _config,UserManager<APiUser> _userManager,SignInManager<APiUser> _signInManager) : ControllerBase
    {
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterDto userData)
        {

            var result = await _accountService.RegisterAsync(userData);
           
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginDto loginData)
        {
            var result = await _accountService.Login(loginData);

            


            return Ok(result);
        }
        

       
    }
      
    }

