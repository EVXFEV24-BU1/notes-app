public class Note
{
    public Guid Id { get; init; }
    public string Title { get; set; }
    public string Content { get; set; }
    public List<DateTime> UpdateHistoryDates { get; set; }
    public User User { get; set; }

    public Note(string title, string content)
    {
        this.Id = Guid.NewGuid();
        this.Title = title;
        this.Content = content;
        this.UpdateHistoryDates = [DateTime.UtcNow];
        this.User = null!;
    }

    public Note() { }
}
