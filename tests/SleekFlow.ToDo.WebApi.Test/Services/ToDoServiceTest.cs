using System.Diagnostics;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using SleekFlow.ToDo.WebApi.DataAccess;
using SleekFlow.ToDo.WebApi.DataAccess.Implementations;
using SleekFlow.ToDo.WebApi.MappingProfiles;
using SleekFlow.ToDo.WebApi.Models;
using SleekFlow.ToDo.WebApi.Services;
using SleekFlow.ToDo.WebApi.Services.Implementations;
using Xunit;

namespace SleekFlow.ToDo.WebApi.Test.Services;

public class ToDoServiceTest
{
    private readonly IToDoRepository _toDoRepository;
    
    private readonly IConfiguration _configuration;

    private readonly IMapper _mapper; 
    
    public ToDoServiceTest()
    {
        IDictionary<string, string> configurationDict = new Dictionary<string, string>();
        configurationDict["ConnectionStrings:ToDo"] = "Data Source=ToDo.sqlite";

        IConfiguration configuration= new ConfigurationBuilder()
            .AddInMemoryCollection(configurationDict)
            .Build();

        this._toDoRepository = new ToDoRepository(configuration);
        this._configuration = configuration;
        this._mapper = new MapperConfiguration(cfg => cfg.AddProfile(typeof(ToDoMappingProfile))).CreateMapper();
    }

    [Fact]
    public async void Test()
    {
        IToDoService toDoService = this.GetService();
        
        //// Test Query Success
        ToDoModel todo1 = await toDoService.Get(1);
        Assert.NotNull(todo1);
        Assert.Equal(1, todo1.Id);
        
        //// Test Query Null
        ToDoModel todo100 = await toDoService.Get(100);
        Assert.Null(todo100);
        
        //// Get All and Count
        IEnumerable<ToDoModel> allTodos = await toDoService.GetAll(new FilterSortingModel());
        Assert.Equal(5, allTodos.Count());
        Assert.All(allTodos, x => Assert.Equal("New", x.Status));
        
        //// Create a new Task
        ToDoModel createToDoModel = new ToDoModel { Name = "Task 6", DueDate = DateTimeOffset.Now };
        createToDoModel = await toDoService.Create(createToDoModel);
        Assert.NotEqual(0, createToDoModel.Id);
        
        //// Query all makes sure the data created successfully
        allTodos = await toDoService.GetAll(new FilterSortingModel());
        Assert.Equal(6, allTodos.Count());
        
        //// Update
        todo1.Status = "Done";
        ToDoModel updatedToDo = await toDoService.Update(todo1.Id, todo1);
        Assert.Equal(1, updatedToDo.Id);
        Assert.Equal("Done", updatedToDo.Status);
        
        //// Delete and query all to make sure row is deletedx
        int affectedRow = await toDoService.Delete(createToDoModel.Id);
        allTodos = await toDoService.GetAll(new FilterSortingModel());
        Assert.Equal(1, affectedRow);
        Assert.Equal(5, allTodos.Count());
        
        //// Test filter
        IEnumerable<ToDoModel> filter1 = await toDoService.GetAll(new FilterSortingModel { Id = 1 });
        Assert.Single(filter1);

        IEnumerable<ToDoModel> filter2 = await toDoService.GetAll(new FilterSortingModel { Status = "Done" });
        Assert.Single(filter2);
        
        //// Sorting (Descending by ID)
        IList<ToDoModel> sorting1 = (await toDoService.GetAll(new FilterSortingModel { Sort = "-id"})).ToList();

        for (int i = 0; i < sorting1.Count - 1; i++)
        {
            //// Assert ID are descending
            Assert.True(sorting1[i].Id > sorting1[i + 1].Id);
        }
    }

    private IToDoService GetService()
    {
        return new ToDoService(
            this._toDoRepository,
            this._mapper
        );
    }
}