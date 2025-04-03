namespace PowersyncDotnetTodoList.Models;

public class TodoItem
{
    public string Id { get; set; }
    public string ListId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }
    public string DueDate { get; set; }
    public string CreatedAt { get; set; }
}