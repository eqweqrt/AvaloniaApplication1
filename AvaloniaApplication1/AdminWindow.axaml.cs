using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaApplication1;

public partial class AdminWindow : Window
{
    public AdminWindow()
    {
        InitializeComponent();
        LoadData();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    void LoadData()
    {
        this.FindControl<DataGrid>("UsersDGrid").Items = Service.GetContext().Users.Include(w=>w.IdRoleNavigation).ToList();
    }
    private void DeleteBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        DataGrid userSelected = this.FindControl<DataGrid>("UsersDGrid");

        if (userSelected.SelectedItem != null)
        {
            Service.GetContext().Users.Remove(userSelected.SelectedItem as User);
            Service.GetContext().SaveChanges();
            LoadData();
        }
    }
    
    private async void EditBtn_OnClick(object sender, RoutedEventArgs e)
    {
        User selectedUser = (User)this.FindControl<DataGrid>("UsersDGrid").SelectedItem;

        await new EditUserWindow(selectedUser).ShowDialog(this);
        LoadData();
        //bool? result = true;//editUserWindow.ShowDialog();


        /*if (result == true)
        {
            User editedUser = editUserWindow.EditedUser;

            Service.GetContext().Users.Update(editedUser);
            Service.GetContext().SaveChanges();

            LoadData();
        }*/
    }

    private void ExitAdminBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}