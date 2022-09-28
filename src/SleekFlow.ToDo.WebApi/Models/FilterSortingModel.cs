using Microsoft.AspNetCore.Mvc;

namespace SleekFlow.ToDo.WebApi.Models;

public class FilterSortingModel
{
    [FromQuery(Name = "id")]
    public int? Id { get; set; }
    
    [FromQuery(Name = "name")]
    public string Name { get; set; }
    
    [FromQuery(Name = "from")]
    public DateTimeOffset? From { get; set; }
    
    [FromQuery(Name = "to")]
    public DateTimeOffset? To { get; set; }
    
    [FromQuery(Name = "status")]
    public string Status { get; set; }
}