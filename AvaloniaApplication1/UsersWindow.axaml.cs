using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaApplication1;

public partial class UsersWindow : Window
{
    public UsersWindow()
    {
        InitializeComponent();

        DataGrid UsersGrid = this.FindControl<DataGrid>("UsersDGrid");
        UsersGrid.Items = Service.GetContext().Users.Include(w=>w.IdRoleNavigation).ToList();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}