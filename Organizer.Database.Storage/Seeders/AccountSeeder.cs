using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Organizer.Database.Storage.Tables;
using Organizer.Lib.Helper;
using Organizer.Lib.Helper.Enums;

namespace Organizer.Database.Storage.Seeders;

public static class AccountSeeder
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<OrganizerDbContext>();

        var options = serviceProvider.GetService(typeof(IOptionsMonitor<AppSettings>)) as IOptionsMonitor<AppSettings>;
        var adminCredentials = options?.CurrentValue.AdminCredentials;

        var user = context.Users.FirstOrDefault(u => u.Login == adminCredentials.Login);
        if (user != null) return;

        var password = new PasswordHasher<User>().HashPassword(user, adminCredentials.Password);

        context.Users.Add(new User
        {
            Id = Guid.NewGuid(),
            Login = adminCredentials.Login,
            Password = password,
            UserStatus = UserStatusEnum.Activated
        });

        context.SaveChanges();
    }
}