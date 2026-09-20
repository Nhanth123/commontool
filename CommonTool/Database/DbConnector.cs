namespace CommonTool.Database;

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

public class DbConnector : IAsyncDisposable, IDisposable
{
    private readonly SqliteConnection _connection;

    public DbConnector(string dbPath)
    {
        _connection = new SqliteConnection($"Data Source={dbPath}");
    }

    private async Task OpenConnectionAsync()
    {
        if (_connection.State != ConnectionState.Open)
        {
            await _connection.OpenAsync();
        }
    }
    
    public async Task<int> ExecuteWriteAsync(string query, Dictionary<string, object> parameters = null)
    {
        await OpenConnectionAsync();

        using var command = _connection.CreateCommand();
        command.CommandText = query;
        AddParameters(command, parameters);

        return await command.ExecuteNonQueryAsync();
    }
    
    public async Task<List<Dictionary<string, object>>> ExecuteReadAsync(string query,
        Dictionary<string, object> parameters = null)
    {
        await OpenConnectionAsync();
        var results = new List<Dictionary<string, object>>();

        using var command = _connection.CreateCommand();
        command.CommandText = query;
        AddParameters(command, parameters);

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var row = new Dictionary<string, object>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                // Store column name and its corresponding value
                row.Add(reader.GetName(i), reader.GetValue(i));
            }

            results.Add(row);
        }

        return results;
    }

    private void AddParameters(SqliteCommand command, Dictionary<string, object> parameters)
    {
        if (parameters != null)
        {
            foreach (var param in parameters)
            {
                // Protect against SQL injection by using parameterized queries
                command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
            }
        }
    }

    #region Dispose

    public void Dispose()
    {
        _connection?.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (_connection != null)
        {
            await _connection.DisposeAsync();
        }
    }

    #endregion
}