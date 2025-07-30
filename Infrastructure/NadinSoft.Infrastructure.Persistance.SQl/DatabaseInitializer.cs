using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace NadinSoft.Infrastructure.Persistance.SQl;

public static class DatabaseInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
        context.Database.Migrate(); 
    }
}