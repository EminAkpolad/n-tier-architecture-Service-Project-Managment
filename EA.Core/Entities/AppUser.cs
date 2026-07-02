using EA.Core.Commons;
using EA.Core.Commons.Enums;

namespace EA.Core.Entities;

public class AppUser : BaseEntity
{
    
    public string FirstName{get; set;}=string.Empty;
    public string LastName{get; set;}=string.Empty;

    public string Email{get;set;}=string.Empty;
    public string PassWord{get; set;}=string.Empty;
    public string? RefreshToken {get; set;}
    public DateTime? RefreshTokenEndDate {get; set;}
    public required ICollection<RepoSitory> Repositories{get; set;}=new List<RepoSitory>();
}