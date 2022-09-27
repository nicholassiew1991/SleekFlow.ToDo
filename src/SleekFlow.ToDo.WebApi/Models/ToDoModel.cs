namespace SleekFlow.ToDo.WebApi.Models;

public class ToDoModel
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public DateTimeOffset DueDate { get; set; }
    
    public string Status { get; set; }
}