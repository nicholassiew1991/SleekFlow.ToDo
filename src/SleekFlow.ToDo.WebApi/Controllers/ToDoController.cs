using Microsoft.AspNetCore.Mvc;
using SleekFlow.ToDo.WebApi.Models;
using SleekFlow.ToDo.WebApi.Services;

namespace SleekFlow.ToDo.WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ToDoController : ControllerBase
{
    private readonly IToDoService _toDoService;

    public ToDoController(IToDoService toDoService)
    {
        this._toDoService = toDoService;
    }

    /// <summary>
    /// Get all the To-Do Tasks
    /// </summary>
    /// <param name="qs"></param>
    /// <returns>List of ToDoModel</returns>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FilterSortingModel qs)
    {
        IEnumerable<ToDoModel> todos = await this._toDoService.GetAll(qs);
        return this.Ok(todos);
    }

    /// <summary>
    /// Get To-Do Task
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns>ToDoModel</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        ToDoModel todo = await this._toDoService.Get(id);
        return (todo == null) ? this.NotFound() : this.Ok(todo);
    }

    /// <summary>
    /// Create To-Do Task
    /// </summary>
    /// <param name="model">ToDoModel</param>
    /// <returns>ToDoModel</returns>
    [HttpPost]
    public async Task<IActionResult> Create(ToDoModel model)
    {
        model = await this._toDoService.Create(model);
        return this.Ok(model);
    }

    /// <summary>
    /// Update To-Do Task
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="model">ToDoModel</param>
    /// <returns>ToDoModel</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ToDoModel model)
    {
        model = await this._toDoService.Update(id, model);
        return this.Ok(model);
    }

    /// <summary>
    /// Delete To-Do Task
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await this._toDoService.Delete(id);
        return this.Ok();
    }
}