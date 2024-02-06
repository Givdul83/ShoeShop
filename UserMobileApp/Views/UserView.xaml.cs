using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserMobileApp.ViewModels;

namespace UserMobileApp.Views
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : UserControl
    {
        public UserView()
        {
            InitializeComponent();
            
        }
        public UserView(UserViewModel viewModel) :this() 
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
    }
}
