namespace SerialViewList.Shared.Services;

public interface IInternalStorage
{
    Task Init();
    
    Task Save<T>(T obj) where T : class, new();
    Task Update<T>(T obj) where T : class, new();
    Task Delete<T>(T obj) where T : class, new();
    Task<List<T>> GetAll<T>() where T : class, new();
}