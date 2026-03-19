using System.Collections.ObjectModel;
using System.Diagnostics;
using SerialViewList.Shared.Models;
using SerialViewList.Shared.Services;
using TestApp.Shared.Services.Implementations;

namespace SerialViewList.Shared.ViewModels;

public class MainViewModel : BaseViewModel
{
    private IInternalStorage _storage;
    private Serial _newSerial = new();
    public MainViewModel(IInternalStorage storage)
    {
        _storage = storage;
    }

    public ObservableCollection<Serial> Serials { get; set; } = new();
    public Serial NewSerial
    {
        get => _newSerial;
        set
        {
            if (_newSerial != value)
            {
                _newSerial = value;
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
        if (string.IsNullOrWhiteSpace(NewSerial.Name)) 
            return;

        var newSerial = NewSerial;
        
        await _storage.Save(newSerial);
        
        Serials.Add(newSerial);
        NewSerial = new();
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