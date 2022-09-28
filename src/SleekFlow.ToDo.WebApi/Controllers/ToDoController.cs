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

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FilterSortingModel qs)
    {
        IEnumerable<ToDoModel> todos = await this._toDoService.GetAll(qs);
        return this.Ok(todos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        ToDoModel todo = await this._toDoService.Get(id);
        return (todo == null) ? this.NotFound() : this.Ok(todo);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ToDoModel model)
    {
        model = await this._toDoService.Create(model);
        return this.Ok(model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ToDoModel model)
    {
        model = await this._toDoService.Update(id, model);
        return this.Ok(model);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await this._toDoService.Delete(id);
        return this.Ok();
    }
}