using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using SleekFlow.ToDo.WebApi.DataAccess.Entities;

namespace SleekFlow.ToDo.WebApi.DataAccess.Implementations;

public class ToDoRepository : IToDoRepository
{
    private readonly IConfiguration _configuration;
    
    public ToDoRepository(IConfiguration configuration)
    {
        this._configuration = configuration;
    }
    
    public async Task<IEnumerable<ToDoEntity>> GetAll()
    {
        const string sql = "SELECT id, name, dueDate, status FROM todo";

        using (IDbConnection connection = this.GetConnection())
        {
            return await connection.QueryAsync<ToDoEntity>(sql);
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