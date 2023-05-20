using AvaloniaApplication1.Models;

namespace AvaloniaApplication1;

public class Service
{
    private static UsersContext? _dbContext;

    public static UsersContext GetContext()
    {
        if (_dbContext == null)
        {
            _dbContext = new UsersContext();
        }

        return _dbContext;
    }
}