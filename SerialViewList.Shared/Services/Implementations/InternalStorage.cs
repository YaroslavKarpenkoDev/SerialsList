using SerialViewList.Shared.Models;
using SerialViewList.Shared.Services;
using SQLite;
using TestApp.Shared.Services;

namespace TestApp.Shared.Services.Implementations;

public class InternalStorage : IInternalStorage
{
    private SQLiteAsyncConnection _db;
    private readonly string _dbPath;

    public InternalStorage(string dbPath)
    {
        _dbPath = dbPath;
    }

    public async Task Init()
    {
        if (_db is not null) return;
        
        _db = new SQLiteAsyncConnection(_dbPath);
        await _db.CreateTableAsync<Serial>();
    }

    public async Task Save<T>(T obj) where T : class, new()
    {
        await _db.InsertAsync(obj);
    }

    public async Task Update<T>(T obj) where T : class, new()
    {
        await _db.UpdateAsync(obj);
    }

    public async Task Delete<T>(T obj) where T : class, new()
    {
        await _db.DeleteAsync(obj);
    }

    public async Task<List<T>> GetAll<T>() where T : class, new()
    {
        return await _db.Table<T>().ToListAsync();
    }
}