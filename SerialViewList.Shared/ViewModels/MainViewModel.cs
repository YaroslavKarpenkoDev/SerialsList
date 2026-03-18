using System.Collections.ObjectModel;
using System.Diagnostics;
using SerialViewList.Shared.Models;
using SerialViewList.Shared.Services;
using TestApp.Shared.Services.Implementations;

namespace SerialViewList.Shared.ViewModels;

public class MainViewModel : BaseViewModel
{
    private IInternalStorage _storage;
    private string _newSerialName = string.Empty;
    public MainViewModel(IInternalStorage storage)
    {
        _storage = storage;
    }

    public ObservableCollection<Serial> Serials { get; set; } = new();
    public string NewSerialName
    {
        get => _newSerialName;
        set
        {
            if (_newSerialName != value)
            {
                _newSerialName = value;
                OnPropertyChanged();
            }
        }
    }
    public async Task InitializeAsync()
    {
        try 
        {
            await _storage.Init();
           var serials = await _storage.GetAll<Serial>();
           if (serials != null && serials.Any())
           {
               Serials = new ObservableCollection<Serial>(serials);
           }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"DB error: {ex.Message}");
        }
    }
    public async Task SaveSerial()
    {
        if (string.IsNullOrWhiteSpace(NewSerialName)) return;

        var newSerial = new Serial 
        { 
            Name = NewSerialName
        };
        
        await _storage.Save(newSerial);
        
        Serials.Add(newSerial);

        NewSerialName = string.Empty;
    }

    public async Task RefreshList()
    {
        var serials = await _storage.GetAll<Serial>();
        Serials.Clear();
        
        foreach (var item in serials)
        {
            Serials.Add(item);
        }
    }
    
}