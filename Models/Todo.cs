public class Todo
{
    public string id { get; init; }
    public string list_id { get; init; }
    public string description { get; set; }
    public string created_at { get; init; }
    public string created_by { get; init; }
    public bool completed { get; set; }
    public string? completed_at { get; set; }
    public string? completed_by { get; set; }
}
