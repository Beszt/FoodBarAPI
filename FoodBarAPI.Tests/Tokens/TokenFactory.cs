namespace FoodBarAPI.Tests.Tokens;

public static class TokenFactory
{
    public static Token CreateToken(TokenType type)
    {
        return type switch
        {
            TokenType.Admin => new AdminToken(),
            TokenType.User => new UserToken(),
            _ => throw new Exception($"Token type: {type} not exists")
        };
    }
}
