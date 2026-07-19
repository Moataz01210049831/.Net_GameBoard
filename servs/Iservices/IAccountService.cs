using Microsoft.AspNetCore.Identity;
using MyBGList.DTO;

namespace MyBGList.servs.Iservices
{
    public interface IAccountService
    {
        Task<object> RegisterAsync(RegisterDto userData);
        Task<object> Login(LoginDto loginDto);

    }
}
