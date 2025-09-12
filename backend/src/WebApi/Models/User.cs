using System.Text.Json.Serialization;

namespace WebApi.Models;

public class User : TrackableEntity
{
    private string _name = null!;
    public string Name 
    { 
        get => _name; 
        init => _name = value; 
    }
    public string Email { get; init; } = null!;
    public string PasswordHash { get; init; } = null!;
}
