namespace FoodBarAPI.Tests.Tokens;

public class AdminToken : Token
{
    public AdminToken()
    {
        _jsonCredentials = "{\"login\": \"admin\",\"password\": \"admin\"}";
    }
}
