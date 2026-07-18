using EA.Core.Commons;

public class UserClaim:BaseEntity
{
    public int UserId{get; set;}
    public string ClaimType { get; set; }=string.Empty;
    public string ClaimValue { get; set; }=string.Empty;
}