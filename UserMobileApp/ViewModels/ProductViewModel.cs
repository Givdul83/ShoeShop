
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows.Automation;

namespace UserMobileApp.ViewModels;

public partial class ProductViewModel : ObservableObject
{
   
    private readonly BaseProductService _baseProductService;
    private readonly IServiceProvider _serviceProvider;
    private UserDto _userDto;


    [ObservableProperty]
    private ObservableCollection<ProductDto> _products;

    [ObservableProperty]
    private ObservableCollection<ProductDto> _order;

    public ProductViewModel(BaseProductService baseProductService, UserDto userDto, IServiceProvider serviceProvider)
    {

        _baseProductService = baseProductService;
        _products = new ObservableCollection<ProductDto>();
        ShowAllProducts();
        _userDto = userDto;
        _serviceProvider = serviceProvider;
        _order = new ObservableCollection<ProductDto>();
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
    public void AddToOrder(ProductDto productDto)
    {
        Order.Add(productDto);
        OnPropertyChanged(nameof(Sum));

    }

    public void RemoveFromOrder(ProductDto productDto)
    {
        Order.Remove(productDto);
        OnPropertyChanged(nameof(Sum));

    }
    public decimal Sum
    {
        get
        {
            return Order.Sum(item => item.Price);
        }
    }

    public string FirstName
    {
        get=> _userDto.FirstName;
        
    }

    public string LastName
    {
        get => _userDto.LastName;
    }

    public string StreetAddress
    {
        get => _userDto.StreetName;
    }

    public string PostalCode
    {
        get => _userDto.PostalCode;
    }

    public string City
    {
        get => _userDto.City;
    }




    [RelayCommand]
    public void NavigateToMainView()
    {


        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        var startViewModel = _serviceProvider.GetService<StartViewModel>();

        mainViewModel.CurrentViewModel = startViewModel;
    }
}
