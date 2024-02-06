
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace UserMobileApp.ViewModels;

public partial class StartViewModel : ObservableObject
{
    private readonly BaseService _baseService;

    [ObservableProperty]
    private UserRegDto _userRegForm = new();


    [ObservableProperty]
    private ObservableCollection<UserDto> _users = [];

    private bool _isPrivateCustomerChecked = true;

    public StartViewModel(BaseService baseService)
    {
        _baseService = baseService;
        GetUsersFromDataBase();
    }



    private async Task GetUsersFromDataBase()
    {
        var usersFound = await _baseService.GetAllUsersAsync();
        foreach (var user in usersFound)
        {
            Users.Add(user);
        }
    }

    [RelayCommand]
    public async Task AddUserToDataBase()
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
            await _baseService.CreateUserAsync(UserRegForm);
            var newUserDto = await _baseService.FindUserAsync(UserRegForm.Email);
            if (newUserDto != null)
            {
                Users.Add(newUserDto);
            }
        }
    }

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

    [RelayCommand]
    public async Task DeleteUserFromDatabase(UserDto user)
    {
        if (user != null)
        {

            await _baseService.DeleteUserAsync(user.Email);
            Users.Remove(user);
        }
    }


}
