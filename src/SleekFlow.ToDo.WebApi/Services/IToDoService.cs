using SleekFlow.ToDo.WebApi.Models;

namespace SleekFlow.ToDo.WebApi.Services;

public interface IToDoService
{
    Task<ToDoModel> Get(int id);
    
    Task<IEnumerable<ToDoModel>> GetAll();

    Task<ToDoModel> Create(ToDoModel model);

    Task<ToDoModel> Update(int id, ToDoModel model);

    Task<int> Delete(int id);
}