using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaApplication1;

public partial class EditUserWindow : Window
{
    public User SelectedUser { get; set; }
    public TextBox NameTextBox { get; set; }
    public TextBox SurnameTextBox { get; set; }
    public TextBox PhoneNumberTextBox { get; set; }
    public TextBox BirthdatePicker { get; set; }
    public TextBox LoginTextBox { get; set; }
    public TextBox RoleComboBox { get; set; }

    public Grid MainGrid { get; set; }
    public object SelectedItem { get; set; }
    public DateTime? SelectedDate { get; set; }
    
    public EditUserWindow(User selectedUser)
    {
        InitializeComponent();

        SelectedUser = selectedUser;
        
        NameTextBox = new TextBox();
        NameTextBox.Text = selectedUser.Name;
        Grid.SetColumn(NameTextBox, 1);
        Grid.SetRow(NameTextBox, 0);
        MainGrid.Children.Add(NameTextBox);

    }
    /*public EditUserWindow(User? user)
    {
        InitializeComponent();
        
#if DEBUG
        this.AttachDevTools();
#endif
    }*/

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    public EditUserWindow()
    {
        InitializeComponent();
    }

    private void AcceptBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        DataGrid userSelected = this.FindControl<DataGrid>("UsersDGrid");

        if (userSelected.SelectedItem != null)
        {
            Service.GetContext().SaveChanges();
            LoadData();
            Close();
        }
    }

    void LoadData()
    {
        this.FindControl<DataGrid>("UsersDGrid").Items = Service.GetContext().Users.Include(w=>w.IdRoleNavigation).ToList();
    }
    public User EditedUser { get; private set; }

    private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
    {
        User editedUser = new User
        {
            Id = SelectedUser.Id,
            Name = NameTextBox.Text,
            Surname = SurnameTextBox.Text,
            PhoneNumber = PhoneNumberTextBox.Text,
           // Birthdate = BirthdatePicker.SelectedDate.Value,
            Login = LoginTextBox.Text,
            //IdRole = ((Role)RoleComboBox.SelectedItem).Id
        };
        
        EditedUser = editedUser;
        
        Close();
    }
    
    
    
}