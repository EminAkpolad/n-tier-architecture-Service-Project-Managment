using EA.Core.Entities;
using EA.DataAccess.Repositories;

public class AppUserService : IAppUserService
{
    private readonly IGenericRepository<AppUser> _repo;
    private readonly IUnitOfWork _uow;

    public AppUserService(IGenericRepository<AppUser> repo,UnitOfWork uow)
    {
        _repo=repo;
        _uow=uow;
    }

    public async Task CreateAsync(CreateAppUserDto dto)
    {
        AppUser user=new AppUser
        {
            CreatedBy="",
            FirstName=dto.FirstName,
            LastName=dto.LastName,
            Email=dto.Email,
        };
        await _repo.AddAsync(user);
        await _uow.CompleteAsync();
    }

    public async Task UpdateRefreshToken(int userId,string RefreshToken,DateTime expiration)
    {
        var user=await _repo.GetById(userId);
        if (user != null)
        {
            user.RefreshToken=RefreshToken;
            user.RefreshTokenEndDate=expiration;
            await _uow.CompleteAsync();
        }
    }
}