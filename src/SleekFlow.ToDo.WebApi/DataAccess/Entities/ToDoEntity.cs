namespace SleekFlow.ToDo.WebApi.DataAccess.Entities;

public class ToDoEntity
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string DueDate { get; set; }
    
    public string Status { get; set; }
}