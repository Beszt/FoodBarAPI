namespace FoodBarAPI.Tests.Tokens;

public class AdminToken : Token
{
    public AdminToken() : base("{\"login\": \"admin\",\"password\": \"admin\"}") { }
}
