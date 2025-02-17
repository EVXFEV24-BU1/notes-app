public class User
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public string Password { get; set; }
    public DateTime BirthDate { get; set; }
    public List<Note> Notes { get; set; }

    public User(string name, string password, DateTime birthDate)
    {
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.Password = password;
        this.BirthDate = birthDate;
        this.Notes = new List<Note>();
    }

    public User() { }
}
