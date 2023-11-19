namespace Assignment.Domain.Entities;

public class User
{
    public User()
    {
        
    }

    public User(Guid id, string username, string password)
    {
        Id = id;
        Username = username;
        Password = password;
    }

    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
