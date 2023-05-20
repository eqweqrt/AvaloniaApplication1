using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AvaloniaApplication1;

public partial class RegWindow : Window
{
    public RegWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void AddBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        TextBox Login = this.FindControl<TextBox>("LoginTBox");
        TextBox Password = this.FindControl<TextBox>("PasswordTBox");
        TextBox PasswordRepeat = this.FindControl<TextBox>("PasswordRepeatTBox");
        TextBox PhoneNumber = this.FindControl<TextBox>("PhonenumberTBox");
        DatePicker Birthdate = this.FindControl<DatePicker>("BirthdateDPicker");
        TextBox Name = this.FindControl<TextBox>("NameTBox");
        TextBox Surname = this.FindControl<TextBox>("SurnameTBox");
        TextBlock Info = this.FindControl<TextBlock>("InfoTBlock");

        if (!string.IsNullOrWhiteSpace(Login.Text) &&
            !string.IsNullOrWhiteSpace(Password.Text) &&
            !string.IsNullOrWhiteSpace(PasswordRepeat.Text) &&
            !string.IsNullOrWhiteSpace(PhoneNumber.Text) &&
            !string.IsNullOrWhiteSpace(Birthdate.SelectedDate.ToString()) &&
            !string.IsNullOrWhiteSpace(Name.Text) &&
            !string.IsNullOrWhiteSpace(Surname.Text))
        {
            if (string.Equals(Password.Text, PasswordRepeat.Text))
            {
                User? user = Service.GetContext().Users
                    .FirstOrDefault(u => u.Login == Login.Text || u.PhoneNumber == PhoneNumber.Text);
                if (user == null)
                {
                    User newUser = new User()
                    {
                        Id = Service.GetContext().Users.Max(q=>q.Id) + 1,
                        Login = Login.Text,
                        Password = Password.Text,
                        Name = Name.Text,
                        Surname = Surname.Text,
                        PhoneNumber = PhoneNumber.Text,
                        Birthdate = Birthdate.SelectedDate.Value.DateTime.ToString("d/M/yy"),
                        IdRole = 1
                    };

                    Service.GetContext().Users.Add(newUser);
                    Service.GetContext().SaveChanges();
                    Close();
                }
                else
                {
                    Info.Text = "Login or Phone number already exist";
                }
            }
            else
            {
                Info.Text = "Password don`t repeat";
            }
        }
        else
        {
            Info.Text = "Enter data";
        }
    }

    private void CloseBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}