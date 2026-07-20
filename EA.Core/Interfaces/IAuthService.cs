public interface IAuthService
{
    
    Task<TokenDto?> LoginAsync(LoginDto loginDto);
    Task<TokenDto?> RegisterAsync(RegisterDto registerDto);
    Task<TokenDto?> RefreshTokenAsync(string refreshToken);
    Task<bool> LogoutAsync(int userId); 
}