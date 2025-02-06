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

        if (!user.Password.Equals(password))
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

        User user = new User(username, password, birthDate);
        userRepository.Save(user);
        return user;
    }
}
