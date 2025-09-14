namespace WebApi.Models;

public class User : TrackableEntity
{
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Phone { get; private set; } = null!;

    protected User() { }

    public User(string name, string email, string phone)
    {
        Name = name;
        Email = email;
        Phone = phone;
    }

    public void Update(User userData)
    {
        Name = userData.Name ?? Name;
        Email = userData.Email ?? Email;
        Phone = userData.Phone ?? Phone;
        SetUpdateAt(DateTime.UtcNow);
    }
}
