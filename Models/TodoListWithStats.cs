namespace PowersyncDotnetTodoList.Models;

public class TodoListWithStats : TodoList
{
    public int pending_tasks { get; set; }
    public int completed_tasks { get; set; }
}
