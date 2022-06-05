namespace Domain.Models.Response.Authentication;

public record LoginResponse
{
    public LoginResponse(string accessToken, int expiresIn, string tokenType)
    {
        AccessToken = accessToken;
        ExpiresIn = expiresIn;
        TokenType = tokenType;
    }
    public string AccessToken { get; set; }
    public int ExpiresIn { get; set; }
    public string TokenType { get; set; }
}
