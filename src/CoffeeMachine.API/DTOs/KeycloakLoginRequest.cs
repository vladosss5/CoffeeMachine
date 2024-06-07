namespace CoffeeMachine.API.DTOs;

public class KeycloakLoginRequest
{
    public string GrantType { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Scope { get; set; }
}
