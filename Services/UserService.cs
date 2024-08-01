using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web_App.Data;
using Web_App.ModelDTOs;
using Web_App.Models;
using Web_App.Services.Interfaces;

namespace Web_App.Services;

public class UserService : IUserService
{
    private readonly WebAppContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(WebAppContext context, IPasswordHasher<User> passwordHasher) 
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }
    public async Task<User?> RegisterLogin(SignUpDTO model)
    {
        var user = await _context.Users.Where(u => u.UserName == model.User.UserName).FirstOrDefaultAsync();

        if(user == null)
        {
            user = await Register(model.User);
        }
        else
        {
            user = Login(user, model.User);
        }
        return user;
    }

    private async Task<User> Register(UserDTO model)
    {
        User user = new User()
        {
            UserName = model.UserName,
        };
        
        await _context.Users.AddAsync(user);

        user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
        await _context.SaveChangesAsync();
        return user;
    }

    private User? Login(User user, UserDTO model)
    {
        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash ?? "", model.Password);
        if(result == PasswordVerificationResult.Success)
        {
            return user;
        }
        else
        {
            return null;
        }
    }
}
