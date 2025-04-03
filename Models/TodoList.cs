namespace PowersyncDotnetTodoList.Models;
// public class TodoList
// {
//     public string Id { get; set; }
//     public string Name { get; set; }
//     public string OwnerId { get; set; }
//     public string CreatedAt { get; set; }
// }
public record TodoList(string id, string name, string owner_id, string created_at);