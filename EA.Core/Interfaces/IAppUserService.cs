public interface IAppUserService
{
    Task CreateAsync(CreateAppUserDto dto);
    Task UpdateRefreshToken(int userId,string RefreshToken,DateTime expiration);
}