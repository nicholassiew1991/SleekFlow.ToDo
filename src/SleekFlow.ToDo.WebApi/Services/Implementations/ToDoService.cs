using AutoMapper;
using SleekFlow.ToDo.WebApi.DataAccess;
using SleekFlow.ToDo.WebApi.DataAccess.Entities;
using SleekFlow.ToDo.WebApi.Models;

namespace SleekFlow.ToDo.WebApi.Services.Implementations;

public class ToDoService : IToDoService
{
    private readonly IToDoRepository _toDoRepository;

    private readonly IMapper _mapper;

    public ToDoService(IToDoRepository toDoRepository,
                       IMapper mapper)
    {
        this._toDoRepository = toDoRepository;
        this._mapper = mapper;
    }
    
    public async Task<ToDoModel> Get(int id)
    {
        ToDoEntity toDoEntity = await this._toDoRepository.Get(id);
        return this._mapper.Map<ToDoModel>(toDoEntity);
    }

    public async Task<IEnumerable<ToDoModel>> GetAll(FilterSortingModel filterSorting)
    {
        IEnumerable<ToDoEntity> results = await this._toDoRepository.GetAll(filterSorting);
        return this._mapper.Map<IEnumerable<ToDoModel>>(results);
    }

    public async Task<ToDoModel> Create(ToDoModel model)
    {
        ToDoEntity entity = this._mapper.Map<ToDoEntity>(model);
        entity.Status = "New";

        entity = await this._toDoRepository.Create(entity);

        return this._mapper.Map<ToDoModel>(entity);
    }

    public async Task<ToDoModel> Update(int id, ToDoModel model)
    {
        ToDoEntity entity = this._mapper.Map<ToDoEntity>(model);
        
        entity = await this._toDoRepository.Update(id, entity);

        return this._mapper.Map<ToDoModel>(entity);
    }

    public async Task<int> Delete(int id)
    {
        return await this._toDoRepository.Delete(id);
    }
}