using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyBGList.DTO;
using MyBGList.Models;
using MyBGList.servs.Iservices;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace MyBGList.servs
{
    public class AccountService(UserManager<APiUser> _userManager, ILogger<APiUser> _logger, ApplicationDbContext context, IConfiguration _config) :IAccountService
    {
     

        public async Task<object> Login(LoginDto loginDto)
        {
                var user = await _userManager.FindByNameAsync(loginDto.UserName);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid username or password."); 

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!isPasswordValid)
               
                     throw new Exception("Invalid username or password."); 

            var claims = new List<Claim>
{
    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!),
    new Claim(JwtRegisteredClaimNames.Email, user.Email!),
    
};
            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

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
            return new 
            {
                Token = jwt,
                Expiration = token.ValidTo,
                UserName = user.UserName,
                Email = user.Email
            };



        }

        public async Task<object> RegisterAsync(RegisterDto userData)
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
            return (StatusCodes.Status201Created);
        }
    }
}
