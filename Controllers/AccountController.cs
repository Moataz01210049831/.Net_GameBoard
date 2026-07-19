using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyBGList.DTO;
using MyBGList.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyBGList.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController(ILogger<APiUser> _logger, ApplicationDbContext context,IConfiguration _config,UserManager<APiUser> _userManager,SignInManager<APiUser> _signInManager) : ControllerBase
    {
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(RegisterDto userData)
        {
            var newUser = new APiUser
            {
                UserName = userData.UserName,
                Email = userData.Email
            };

            var result = await _userManager.CreateAsync(newUser, userData.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }

            _logger.LogInformation("User {UserName} has been created", newUser.UserName);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginDto loginData)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginData.UserName);

                if (user == null)
                    return Unauthorized("Invalid username or password.");

                var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginData.Password);

                if (!isPasswordValid)
                    return Unauthorized("Invalid username or password.");

                var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!)
        };

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_config["JWT:Key"]!));

                var credentials = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _config["JWT:Issuer"],
                    audience: _config["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: credentials);

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                _logger.LogInformation("User {UserName} logged in successfully.", user.UserName);

                return Ok(new
                {
                    Token = jwt,
                    Expiration = token.ValidTo,
                    userName = user.UserName,
                    Email = user.Email
                });
            }
            else
            {
                return BadRequest();
            }
        

       
    }
      
    }
}
