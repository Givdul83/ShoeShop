
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace UserMobileApp.ViewModels;

public partial class ProductViewModel : ObservableObject
{
   
    private readonly BaseProductService _baseProductService;
    private UserDto _userDto;


    [ObservableProperty]
    private ObservableCollection<ProductDto> _products;

    public ProductViewModel(BaseProductService baseProductService, UserDto userDto)
    {

        _baseProductService = baseProductService;
        _products = new ObservableCollection<ProductDto>();
        ShowAllProducts();
        _userDto = userDto;
    }

    private async void ShowAllProducts()
    {
        var productsFound= await _baseProductService.GetAllProductsAsync();

        foreach (var product in productsFound)
        {
            Products.Add(product);
        }
    }








    [RelayCommand]
    public void NavigateToMainView()
    {


        //var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        //var startViewModel = _serviceProvider.GetService<StartViewModel>();

        //mainViewModel.CurrentViewModel = startViewModel;
    }
}
