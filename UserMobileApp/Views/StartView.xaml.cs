
using Infrastructure.Dtos;
using System.Windows;
using System.Windows.Controls;

using UserMobileApp.ViewModels;

namespace UserMobileApp.Views;

/// <summary>
/// Interaction logic for StartView.xaml
/// </summary>
public partial class StartView : UserControl
{
    public StartView()
    {
        InitializeComponent();
          
    }

    public StartView(StartViewModel viewModel) :this() 
    {
        DataContext = viewModel;
    }
    private void Btn_Company_Checked(object sender, RoutedEventArgs e)
    {
        if (DataContext is StartViewModel viewModel)
            viewModel.IsCompanyCustomerChecked = true;

    }

    private void Btn_Private_Checked(object sender, RoutedEventArgs e)
    {
        if (DataContext is StartViewModel viewModel)
            viewModel.IsPrivateCustomerChecked = true;
    }

    private void BtnStartReg_Click_1(object sender, RoutedEventArgs e)
    {
        RegistrationForm.Visibility = Visibility.Visible;
    }

    private void BtnSubmit_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is StartViewModel viewModel)
        {
            viewModel.AddUserToDataBase();
        }
    }

    private void BtnSelect_Click(object sender, RoutedEventArgs e)
    {

    }

    private void BtnShow_Click(object sender, RoutedEventArgs e)
    {

    }

    private void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is UserDto user)
        {
            if (DataContext is StartViewModel viewModel)
            {
                viewModel.DeleteUserFromDatabaseCommand.Execute(user);
            }
        }

    }
}
