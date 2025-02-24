// Basklassen för alla kommandon
// Den innehåller namnet på kommandot och
//  en beskrivning, samt dependencyProvider så att implementationerna (e.g. LoginCommand)
//  kan komma åt dependencies som services och repositories
public abstract class Command
{
    public string Name { get; init; }
    public string Description { get; init; }

    protected DependencyProvider dependencyProvider;

    public Command(string name, string description, DependencyProvider dependencyProvider)
    {
        this.Name = name;
        this.Description = description;
        this.dependencyProvider = dependencyProvider;
    }

    // Hjälpmetod för att slippa skriva 'dependencyProvider.Get<T>()' varje gång
    public T Get<T>()
    {
        return dependencyProvider.Get<T>();
    }

    // Alla kommandon gör olika saker och då används denna metod
    public abstract void Execute();
}

[AttributeUsage(AttributeTargets.Class)]
public class MenuCommandAttribute : Attribute
{
    public Type MenuType { get; }

    public MenuCommandAttribute(Type menuType)
    {
        this.MenuType = menuType;
    }
}
