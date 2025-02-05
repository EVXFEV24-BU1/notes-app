// Interfacet för alla menu services. Här har vi bara en implementation: DefaultMenuService
public interface IMenuService
{
    // Hämta den nuvarande/aktuella menyn
    Menu GetCurrentMenu();

    // Byt meny
    void SetMenu(Menu menu);
}

public class DefaultMenuService : IMenuService
{
    private Menu menu;

    public Menu GetCurrentMenu()
    {
        return menu;
    }

    public void SetMenu(Menu menu)
    {
        this.menu = menu;
        this.menu.Display();
    }
}
