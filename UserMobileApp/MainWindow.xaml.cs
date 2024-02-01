
using System.Windows;


namespace UserMobileApp;


public partial class MainWindow : Window
{
    
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
       
        DataContext = viewModel;
    }



    private void Btn_Company_Checked(object sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
            viewModel.IsCompanyCustomerChecked = true;
        
    }

    private void Btn_Private_Checked(object sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
            viewModel.IsPrivateCustomerChecked = true;
    }

    private void BtnStartReg_Click_1(object sender, RoutedEventArgs e)
    {
        RegistrationForm.Visibility = Visibility.Visible;
    }

    private void BtnSubmit_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
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
        if (DataContext is MainViewModel viewModel)
        {
           
        }

    }
}
