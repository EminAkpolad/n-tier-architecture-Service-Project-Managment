using EA.Core.Entities;
using EA.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

public class AuthService
{
    private readonly IGenericRepository<AppUser> _userRepo;
    private readonly IUnitOfWork _uow;
    private readonly ITokenService _tokenService;
    private readonly IAppUserService _userService;
    public AuthService(IGenericRepository<AppUser> userRepo,IUnitOfWork uow,ITokenService tokenService,IAppUserService userService)
    {
        _uow=uow;
        _userRepo=userRepo;
        _tokenService=tokenService;
        _userService=userService;
    }
     public async Task<TokenDto?> LoginAsync(LoginDto loginDto)
    {
        var user=await _userRepo.Where(x=>x.Email==loginDto.Email).FirstOrDefaultAsync();
        if (user != null)
        {
            var tokenDto=_tokenService.CreateToken(user);
            await _userService.UpdateRefreshToken(user.Id,tokenDto.RefreshToken,DateTime.Now.AddDays(7));
            return tokenDto;
        }
        return null;
    }
}