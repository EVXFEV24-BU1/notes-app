using System.Security.Cryptography;
using System.Text;

public interface IUserService
{
    User? GetLoggedInUser();

    User Register(string username, string password, DateTime birthDate);
    User Login(string username, string password);
    void Logout();
}

public class DefaultUserService : IUserService
{
    private IUserRepository userRepository;

    private Guid? loggedInUserId = null;

    public DefaultUserService(DependencyProvider dependencyProvider)
    {
        this.userRepository = dependencyProvider.Get<IUserRepository>();
    }

    public User? GetLoggedInUser()
    {
        if (loggedInUserId != null)
        {
            return userRepository.GetById(
                loggedInUserId ?? throw new Exception("User is not logged in.")
            );
        }

        return null;
    }

    public User Login(string username, string password)
    {
        User? user = userRepository.GetByName(username);
        if (user == null)
        {
            throw new KeyNotFoundException("Wrong username or password.");
        }

        string[] passwordParts = user.Password.Split(":");
        string passwordHash = passwordParts[0];
        string salt = passwordParts[1];

        string userProvidedPasswordHash = HashPassword(password, salt);

        if (!passwordHash.Equals(userProvidedPasswordHash))
        {
            throw new ArgumentException("Wrong username or password.");
        }

        // Markera användare som inloggad
        loggedInUserId = user.Id;
        return user;
    }

    public void Logout()
    {
        loggedInUserId = null;
    }

    public User Register(string username, string password, DateTime birthDate)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Username may not be null or empty.");
        }

        if (username.Length < 3)
        {
            throw new ArgumentException("Username must be at least 3 characters.");
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Passowrd may not be null or empty.");
        }

        if (password.Length < 5)
        {
            throw new ArgumentException("Password must be at least 5 characters.");
        }

        string salt = GenerateSalt();
        password = HashPassword(password, salt);
        password += ":" + salt;

        User user = new User(username, password, birthDate);
        userRepository.Save(user);
        return user;
    }

    private string HashPassword(string password, string salt)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] fullBytes = passwordBytes.Concat(Encoding.UTF8.GetBytes(salt)).ToArray();

        StringBuilder sb = new StringBuilder();
        using (HashAlgorithm algorithm = SHA256.Create())
        {
            byte[] hash = algorithm.ComputeHash(fullBytes);
            foreach (byte b in hash)
                sb.Append(b.ToString("X2"));
        }

        return sb.ToString();
    }

    private string GenerateSalt()
    {
        StringBuilder sb = new StringBuilder();
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        foreach (byte b in salt)
            sb.Append(b.ToString("X2"));

        return sb.ToString();
    }
}
