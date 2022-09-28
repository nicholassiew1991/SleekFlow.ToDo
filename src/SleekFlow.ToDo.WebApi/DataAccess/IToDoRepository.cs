using SleekFlow.ToDo.WebApi.DataAccess.Entities;

namespace SleekFlow.ToDo.WebApi.DataAccess;

public interface IToDoRepository
{
    Task<IEnumerable<ToDoEntity>> GetAll();

    Task<ToDoEntity> Get(int id);

    Task<ToDoEntity> Create(ToDoEntity entity);

    Task<int> Update(int id, ToDoEntity entity);

    Task<int> Delete(int id);
}