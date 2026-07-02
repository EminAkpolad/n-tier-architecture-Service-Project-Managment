using EA.Core.Commons;
using EA.Core.Commons.Enums;

namespace EA.Core.Entities;

public class Commit : BaseEntity
{
    public required string Message{get; set;}
    public int RepoSitoryId{get; set;}
    public RepoSitory? RepoSitory{get; set;}=null;
}