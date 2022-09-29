using Microsoft.AspNetCore.Mvc;

namespace SleekFlow.ToDo.WebApi.Models;

public class FilterSortingModel
{
    /// <summary>
    /// Id
    /// </summary>
    [FromQuery(Name = "id")]
    public int? Id { get; set; }
    
    /// <summary>
    /// Name
    /// </summary>
    [FromQuery(Name = "name")]
    public string Name { get; set; }
    
    /// <summary>
    /// Due Date Range From <br />
    /// Example: 2022-10-01T13:46:03.81+00:00
    /// </summary>
    [FromQuery(Name = "from")]
    public DateTimeOffset? From { get; set; }
    
    /// <summary>
    /// Due Date Range To <br />
    /// Example: 2022-10-01T13:46:03.81+00:00
    /// </summary>
    [FromQuery(Name = "to")]
    public DateTimeOffset? To { get; set; }
    
    /// <summary>
    /// Status
    /// </summary>
    [FromQuery(Name = "status")]
    public string Status { get; set; }
    
    /// <summary>
    /// Sort by property, '+' for ASC, '-' for DESC <br />
    /// Example: -id
    /// </summary>
    [FromQuery(Name = "sort")]
    public string Sort { get; set; }
}