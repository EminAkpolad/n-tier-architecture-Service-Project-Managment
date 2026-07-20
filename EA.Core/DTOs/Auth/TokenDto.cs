public record TokenDto(
    string AccessToken,
    string RefreshToken,
    DateTime Expiration
);
