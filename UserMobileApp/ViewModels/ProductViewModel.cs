
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Dtos;
using Microsoft.Extensions.DependencyInjection;

namespace UserMobileApp.ViewModels;

public partial class ProductViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private UserDto _userDto;

    public ProductViewModel(IServiceProvider serviceProvider, UserDto userDto)
    {
        _serviceProvider = serviceProvider;
        _userDto = userDto;
    }

    [RelayCommand]
    public void NavigateToMainView()
    {


        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        var startViewModel = _serviceProvider.GetService<StartViewModel>();

        mainViewModel.CurrentViewModel = startViewModel;
    }
}
