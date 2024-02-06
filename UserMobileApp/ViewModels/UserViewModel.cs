

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace UserMobileApp.ViewModels;

public partial class UserViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    private readonly BaseService _baseService;

    [ObservableProperty]
    private UserDto _userDto;

    [ObservableProperty]
    private UserRegDto _userRegForm = new();

    public UserViewModel(IServiceProvider serviceProvider, UserDto userDto, BaseService baseService)
    {
        _serviceProvider = serviceProvider;
        _userDto = userDto;
        _baseService = baseService;


        UserRegForm.FirstName = _userDto.FirstName;
        UserRegForm.LastName = _userDto.LastName;
        UserRegForm.Email = _userDto.Email;
        UserRegForm.StreetName = _userDto.StreetName;
        UserRegForm.PostalCode = _userDto.PostalCode;
        UserRegForm.City = _userDto.City;
        UserRegForm.TypeOfCustomer = _userDto.TypeOfCustomer;


        if (_userDto.TypeOfCustomer == "Private")
        {
            _isPrivateCustomerChecked = true;
            _isCompanyCustomerChecked = false;
        }
        else if (_userDto.TypeOfCustomer == "Company")
        {
            _isCompanyCustomerChecked = true;
            _isPrivateCustomerChecked = false;
        }
        _baseService = baseService;
    }

    [RelayCommand]
    public void NavigateToMainView()
    {

        
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        var startViewModel = _serviceProvider.GetService<StartViewModel>();

       mainViewModel.CurrentViewModel = startViewModel;
    }

    [RelayCommand]

    public async Task UpdateUserInfo()
    {
        if (_isPrivateCustomerChecked == true)
        {
            UserRegForm.TypeOfCustomer = "Private";
        }
        else
        {
            UserRegForm.TypeOfCustomer = "Company";
        }

        if (!string.IsNullOrWhiteSpace(UserRegForm.FirstName) && !string.IsNullOrWhiteSpace(UserRegForm.LastName)
            && !string.IsNullOrWhiteSpace(UserRegForm.Email) && !string.IsNullOrWhiteSpace(UserRegForm.StreetName)
            && !string.IsNullOrWhiteSpace(UserRegForm.PostalCode) && !string.IsNullOrWhiteSpace(UserRegForm.City))
        {

            var updatedUser = new UserRegDto();
            updatedUser.FirstName = UserRegForm.FirstName;
            updatedUser.LastName = UserRegForm.LastName;
            updatedUser.Email = UserRegForm.Email;
            updatedUser.StreetName = UserRegForm.StreetName;
            updatedUser.PostalCode = UserRegForm.PostalCode;
            updatedUser.City = UserRegForm.City;
            updatedUser.TypeOfCustomer = UserRegForm.TypeOfCustomer;

            var result =await _baseService.UpdateUser(updatedUser);
            if(result)
            {
                
            }

        }
    }

    private bool _isPrivateCustomerChecked;
    public bool IsPrivateCustomerChecked
    {
        get => _isPrivateCustomerChecked;
        set
        {
            SetProperty(ref _isPrivateCustomerChecked, value);
            if (value) UserRegForm.TypeOfCustomer = "Private";
        }
    }

    private bool _isCompanyCustomerChecked;
    public bool IsCompanyCustomerChecked
    {
        get => _isCompanyCustomerChecked;
        set
        {
            SetProperty(ref _isCompanyCustomerChecked, value);
            if (value) UserRegForm.TypeOfCustomer = "Company";
        }
    }
}
