using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.Models;
using Microsoft.EntityFrameworkCore;


namespace AvaloniaApplication1;

public partial class EditUserWindow : Window
{
    public User SelectedUser { get; set; }
    private TextBox LoginTBox;
    private TextBox PasswordTBox;
    private TextBox PhoneNumberTBox;
    private DatePicker BirthdateDatePicker;
    private TextBox NameTBox;
    private TextBox SurnameTBox;

    public EditUserWindow(User selectedUser)
    {
        InitializeComponent();
        LoginTBox = this.FindControl<TextBox>("LoginTextBox");
        PasswordTBox = this.FindControl<TextBox>("PasswordTextBox");
        PhoneNumberTBox = this.FindControl<TextBox>("PhoneNumberTextBox");
        BirthdateDatePicker = this.FindControl<DatePicker>("BirthdateDatePickerEdit");
        NameTBox = this.FindControl<TextBox>("NameTextBox");
        SurnameTBox = this.FindControl<TextBox>("SurnameTextBox");
        SelectedUser = selectedUser;
        DateTime dateTime = DateTime.Parse(SelectedUser.Birthdate);

        LoginTBox.Text=SelectedUser.Login;
        PasswordTBox.Text = SelectedUser.Password;
        PhoneNumberTBox.Text = SelectedUser.PhoneNumber;
        BirthdateDatePicker.SelectedDate = dateTime;
        NameTBox.Text = SelectedUser.Name;
        SurnameTBox.Text = SelectedUser.Surname;
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    public EditUserWindow()
    {
        InitializeComponent();
    }

    void LoadData()
    {
        this.FindControl<DataGrid>("UsersDGrid").Items = Service.GetContext().Users.Include(w=>w.IdRoleNavigation).ToList();
    }
    public User EditedUser { get; private set; }

    private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
    {
        SelectedUser.Login = LoginTBox.Text;
        SelectedUser.Password = PasswordTBox.Text;
        SelectedUser.PhoneNumber = PhoneNumberTBox.Text;
        SelectedUser.Birthdate = BirthdateDatePicker.SelectedDate.Value.DateTime.ToString("d/M/yy");
        SelectedUser.Name = NameTBox.Text;
        SelectedUser.Surname = SurnameTBox.Text;

        Console.Write(BirthdateDatePicker);
        Service.GetContext().SaveChanges();
       // LoadData();
        Close();
    }

    private void ExitEditBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}