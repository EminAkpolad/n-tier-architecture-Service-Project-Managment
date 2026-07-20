using EA.Core.Entities;

public interface ITokenService
{
    TokenDto CreateToken(AppUser user);
}