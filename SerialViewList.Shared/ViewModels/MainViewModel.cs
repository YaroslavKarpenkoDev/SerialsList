using System.Diagnostics;
using SerialViewList.Shared.Services;
using TestApp.Shared.Services.Implementations;

namespace SerialViewList.Shared.ViewModels;

public class MainViewModel
{
    private IInternalStorage _storage;
    public MainViewModel(IInternalStorage storage)
    {
        _storage = storage;
        _ = InitializeAsync();
    }
    
    private async Task InitializeAsync()
    {
        try 
        {
            await _storage.Init();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"DB error: {ex.Message}");
        }
    }
    
}