using System.Text.RegularExpressions;

public class User
{

    public User(string userName, string userEmail, string password)
    {

        if (string.IsNullOrEmpty(userName)) throw new ArgumentException("UserName cannot be null or empty.");

        if (string.IsNullOrEmpty(userEmail) || !Regex.IsMatch(userEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))

            throw new ArgumentException("Invalid email format.");

        if (string.IsNullOrEmpty(password)) throw new ArgumentException("Password cannot be null or empty.");

        UserId = Guid.NewGuid();
        UserName = userName;
        UserEmail = userEmail;
        Password = password;

    }

    public Guid UserId { get; private set; }

    public string UserName { get; set; }

    public string UserEmail { get; set; }

    public string Password { get; set; }


}