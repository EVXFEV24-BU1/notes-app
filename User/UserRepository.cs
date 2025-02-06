// Basklassen för alla user repositories. Fördelen med detta är att vi enkelt
// kan bygga ut med andra repositories, t.ex för databaser som vi kommer att göra vecka 2.
public interface IUserRepository
{
    void Save(User user);
    User? GetById(Guid id);
    User? GetByName(string name);
}

// Vår nuvarande UserRepository sparar users i en enkel lista.
public class ListUserRepository : IUserRepository
{
    // Hårdkoda en användare så att vi slipper registrera varje gång - detta är temporärt
    private List<User> users = [new User("Ironman", "tonystark", DateTime.Now)];

    // Hämta ut hela objektet utifrån ett id
    public User? GetById(Guid id)
    {
        return users.Find(user => user.Id.Equals(id));
    }

    // Hämta ut hela objektet utifrån ett namn
    public User? GetByName(string name)
    {
        return users.Find(user => user.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public void Save(User user)
    {
        int index = users.FindIndex(existing => existing.Id.Equals(user.Id));
        if (index == -1)
        {
            users.Add(user);
        }
        else
        {
            users[index] = user;
        }
    }
}
