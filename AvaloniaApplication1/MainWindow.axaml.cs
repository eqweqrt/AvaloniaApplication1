using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApplication1.Models;

namespace AvaloniaApplication1;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void SigninBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        TextBox? Login = this.FindControl<TextBox>("LoginTBox");
        TextBox? Password = this.FindControl<TextBox>("PasswordTBox");
        
        if (!string.IsNullOrWhiteSpace(LoginTBox.Text) && !string.IsNullOrWhiteSpace(PasswordTBox.Text))
        {
            User? userAuth = Service.GetContext().Users
                .FirstOrDefault(u => u.Login == Login.Text && u.Password == Password.Text);

            if (userAuth != null && userAuth.IdRole == 1)
            {
                new UsersWindow().Show();
                Close();
            } else if (userAuth != null && userAuth.IdRole == 2)
            {
                new AdminWindow().Show();
                Close();                
            }
        }
    }

    private void SignupBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        new RegWindow().ShowDialog(this);
    }
}