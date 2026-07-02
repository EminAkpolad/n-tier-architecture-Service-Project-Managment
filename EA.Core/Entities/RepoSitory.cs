using EA.Core.Commons;
using EA.Core.Commons.Enums;

namespace EA.Core.Entities;

public class RepoSitory : BaseEntity
{
    public required string Name{get;set;}
    public string? Description{get;set;}
    public bool IsPublic{get; set;}=true;
    public int AppUserId{get;set;}
    public AppUser? AppUser {get; set;}=null;
}