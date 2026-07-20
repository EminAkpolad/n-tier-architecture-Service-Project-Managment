public record TokenDto(
    string AccessToken,
    string RefreshToken,
    DateTime Expiration
)
{
    private string v;
    private DateTime validTo;

    public TokenDto(string v, DateTime validTo, string refreshToken)
    {
        this.v = v;
        this.validTo = validTo;
        RefreshToken = refreshToken;
    }
}