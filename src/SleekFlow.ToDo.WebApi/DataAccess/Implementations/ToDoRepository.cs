using System.Data;
using System.Text;
using Dapper;
using Microsoft.Data.Sqlite;
using SleekFlow.ToDo.WebApi.DataAccess.Entities;
using SleekFlow.ToDo.WebApi.Models;

namespace SleekFlow.ToDo.WebApi.DataAccess.Implementations;

public class ToDoRepository : IToDoRepository
{
    private readonly IConfiguration _configuration;
    
    public ToDoRepository(IConfiguration configuration)
    {
        this._configuration = configuration;
    }
    
    public async Task<IEnumerable<ToDoEntity>> GetAll(FilterSortingModel filterSorting)
    {
        const string sql = @"
            SELECT id, name, dueDate, status
            FROM todo
            WHERE (@id IS NULL OR id = @id)
                AND (@name IS NULL OR name = @name)
                AND (@from IS NULL OR dueDate >= @from)
                AND (@to IS NULL OR dueDate <= @to)
                AND (@status IS NULL OR status = @status)
        ";

        StringBuilder sqlBuilder = new StringBuilder(sql);

        if (string.IsNullOrWhiteSpace(filterSorting.Sort) == false)
        {
            string direction = filterSorting.Sort[0] == '+' ? "ASC" : "DESC";
            sqlBuilder.Append($"ORDER BY {filterSorting.Sort.Substring(1)} {direction}");
            Console.WriteLine(sqlBuilder.ToString());
        }

        using (IDbConnection connection = this.GetConnection())
        {
            var param = new
            {
                id = filterSorting.Id.HasValue == false ? (int?) null : filterSorting.Id.Value,
                name = filterSorting.Name,
                from = filterSorting.From.HasValue == false ? (long?) null : filterSorting.From.Value.ToUnixTimeMilliseconds(),
                to = filterSorting.To.HasValue == false ? (long?) null : filterSorting.To.Value.ToUnixTimeMilliseconds(),
                status = filterSorting.Status
            };
            
            return await connection.QueryAsync<ToDoEntity>(sqlBuilder.ToString(), param);
        }
    }

    public async Task<ToDoEntity> Get(int id)
    {
        const string sql = "SELECT id, name, dueDate, status FROM todo WHERE id = @id";

        using (IDbConnection connection = this.GetConnection())
        {
            var param = new { id };
            return await connection.QueryFirstOrDefaultAsync<ToDoEntity>(sql, param);
        }
    }

    public async Task<ToDoEntity> Create(ToDoEntity entity)
    {
        const string sql = "INSERT INTO todo (name, dueDate, status) VALUES (@Name, @DueDate, @Status) RETURNING *";

        using (IDbConnection connection = this.GetConnection())
        {
            return await connection.QuerySingleAsync<ToDoEntity>(sql, entity);
        }
    }

    public async Task<ToDoEntity> Update(int id, ToDoEntity entity)
    {
        const string sql = "UPDATE todo SET name = @Name, dueDate = @DueDate, status = @Status WHERE id = @id RETURNING *";

        using (IDbConnection connection = this.GetConnection())
        {
            var param = new { id, entity.Name, entity.DueDate, entity.Status };
            return await connection.QuerySingleAsync<ToDoEntity>(sql, param);
        }
    }

    public Task<int> Delete(int id)
    {
        const string sql = "DELETE FROM todo WHERE id = @id";
        
        using (IDbConnection connection = this.GetConnection())
        {
            var param = new { id };
            return connection.ExecuteAsync(sql, param);
        }
    }

    private IDbConnection GetConnection() => new SqliteConnection(this._configuration.GetConnectionString("ToDo"));
}