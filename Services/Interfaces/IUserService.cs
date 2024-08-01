using Web_App.ModelDTOs;
using Web_App.Models;

namespace Web_App.Services.Interfaces;

public interface IUserService
{
    Task<User?> RegisterLogin(SignUpDTO model);
}
