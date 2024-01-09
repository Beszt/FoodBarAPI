namespace FoodBarAPI.Tests.Tokens;

public class UserToken : Token
{
    public UserToken()
    {
        _jsonCredentials = "{\"login\": \"user\",\"password\": \"12345\"}";
    }
}
