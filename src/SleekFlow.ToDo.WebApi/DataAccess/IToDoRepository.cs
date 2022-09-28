using SleekFlow.ToDo.WebApi.DataAccess.Entities;
using SleekFlow.ToDo.WebApi.Models;

namespace SleekFlow.ToDo.WebApi.DataAccess;

public interface IToDoRepository
{
    Task<IEnumerable<ToDoEntity>> GetAll(FilterSortingModel filterSorting);

    Task<ToDoEntity> Get(int id);

    Task<ToDoEntity> Create(ToDoEntity entity);

    Task<ToDoEntity> Update(int id, ToDoEntity entity);

    Task<int> Delete(int id);
}