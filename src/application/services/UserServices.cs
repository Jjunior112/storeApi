using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using storeApp.domain.dtos;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllUsers() => await _context.Users.ToListAsync();

    public async Task<User?> GetUserById(Guid id) => await _context.Users.FindAsync(id);

    public async Task AddUser(RegisterDto request)

    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.password);
        
        var user = new User(request.username, request.email, hashedPassword);
        
        _context.Users.Add(user);

        await _context.SaveChangesAsync();
    }

    public async Task<AuthResponseDto> UserLogin(string email, string password, IConfiguration config)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == email);

        if (user == null)
        {
            return new AuthResponseDto { Success = false, Message = "Usuário inválido" };
        }

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);

        if (!isPasswordValid)
        {
            return new AuthResponseDto { Success = false, Message = "Senha inválida" };
        }

        var token = JwtHelper.GenerateToken(user, config);

        return new AuthResponseDto { Success = true, Token = token };
    }

    public async Task<UserDto?> UpdateUserName(Guid id, string email)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null) return null;

        user.UserEmail = email;

        await _context.SaveChangesAsync();

        return new UserDto(user.UserName, user.UserEmail);
    }

    public async Task<bool> DeleteUser(Guid id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null) return false;

        _context.Users.Remove(user);

        await _context.SaveChangesAsync();

        return true;
    }
}