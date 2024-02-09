using Infrastructure.Dtos;
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

namespace UserMobileApp.Views;


public partial class ProductView : UserControl
{
    public ProductView()
    {
        InitializeComponent();
    }
    public ProductView(ProductViewModel viewModel) : this()
    {
        DataContext = viewModel;
    }

    private void AddToOrder_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is ProductDto product)
        {
            var viewModel = DataContext as ProductViewModel;
            viewModel?.AddToOrder(product);
            SendOrder.Visibility = Visibility.Visible;
            ShowSum.Visibility = Visibility.Visible;
        }
    }

    private void RemoveFromOrder_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is ProductDto product)
        {
            var viewModel = DataContext as ProductViewModel;
            viewModel?.RemoveFromOrder(product);

        }
    }

    private void SendOrder_Click(object sender, RoutedEventArgs e)
    {
        OrderConfirmation.Visibility = Visibility.Visible;
    }
}
