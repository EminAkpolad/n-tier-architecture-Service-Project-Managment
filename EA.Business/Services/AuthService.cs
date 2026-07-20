using EA.Core.Entities;
using EA.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

public class AuthService : IAuthService
{
    private readonly IGenericRepository<AppUser> _userRepo;
    private readonly IUnitOfWork _uow;
    private readonly ITokenService _tokenService;
    private readonly IAppUserService _userService;
    public AuthService(IGenericRepository<AppUser> userRepo, IUnitOfWork uow, ITokenService tokenService, IAppUserService userService)
    {
        _uow = uow;
        _userRepo = userRepo;
        _tokenService = tokenService;
        _userService = userService;
    }
    public async Task<TokenDto?> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepo.Where(x => x.Email == loginDto.Email).FirstOrDefaultAsync();
        if (user != null)
        {
            var tokenDto = _tokenService.CreateToken(user);
            await _userService.UpdateRefreshToken(user.Id, tokenDto.RefreshToken, DateTime.Now.AddDays(7));
            return tokenDto;
        }
        return null;
    }

    public async Task<bool> LogoutAsync(int userId)
    {
        var user = await _userRepo.GetById(userId);
        if (user == null) return false;
        await _userService.UpdateRefreshToken(user.Id, null, DateTime.Now);
        await _uow.CompleteAsync();
        return true;
    }

    public async Task<TokenDto?> RefreshTokenAsync(string refreshToken)
    {
        var user = await _userRepo.Where(x => x.RefreshToken == refreshToken && x.RefreshTokenEndDate > DateTime.Now).FirstOrDefaultAsync();
        if (user == null) return null;

        var tokenDto = _tokenService.CreateToken(user);

        await _userService.UpdateRefreshToken(user.Id, tokenDto.RefreshToken, DateTime.Now.AddDays(7));

        await _uow.CompleteAsync();
        return tokenDto;
    }

    public async Task<TokenDto?> RegisterAsync(RegisterDto registerDto)
    {
        var user = await _userRepo.Where(x => x.Email == registerDto.Email).AnyAsync();
        if (user)
        {
            return null;
        }

        var newUser = new AppUser
        {
            Email = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            PassWord = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
            CreatedBy=""
        };

        await _userRepo.AddAsync(newUser);
        await _uow.CompleteAsync();


        var tokendto = _tokenService.CreateToken(newUser);
        await _userService.UpdateRefreshToken(newUser.Id, tokendto.RefreshToken, DateTime.Now.AddDays(7));
        await _uow.CompleteAsync();

        return tokendto;
    }
}