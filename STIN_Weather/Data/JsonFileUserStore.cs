namespace STIN_Weather.Data;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class JsonFileUserStore : IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserEmailStore<ApplicationUser>
{
    private const string FilePath = "users.json";
    private List<ApplicationUser> _users;

    public JsonFileUserStore()
    {
        if (File.Exists(FilePath))
        {
            var json = File.ReadAllText(FilePath);
            _users = JsonSerializer.Deserialize<List<ApplicationUser>>(json);
        }
        else
        {
            _users = new List<ApplicationUser>();
        }
    }
    public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
    {
        user.PasswordHash = passwordHash;
        SaveChanges();
        return Task.CompletedTask;
    }

    public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.PasswordHash);
    }

    public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.PasswordHash != null);
    }

    public Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        _users.Add(user);
        SaveChanges();
        return Task.FromResult(IdentityResult.Success);
    }

    public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        _users.Remove(user);
        SaveChanges();
        return Task.FromResult(IdentityResult.Success);
    }

    public void Dispose()
    {
        // No-op
    }

    public Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        return Task.FromResult(_users.FirstOrDefault(u => u.Id == userId));
    }

    public Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        return Task.FromResult(_users.FirstOrDefault(u => u.NormalizedUserName == normalizedUserName));
    }

    public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.NormalizedUserName);
    }

    public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Id);
    }

    public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.UserName);
    }

    public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
    {
        user.NormalizedUserName = normalizedName;
        SaveChanges();
        return Task.CompletedTask;
    }

    public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
    {
        user.UserName = userName;
        SaveChanges();
        return Task.CompletedTask;
    }

    public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser != null)
        {
            _users.Remove(existingUser);
            _users.Add(user);
            SaveChanges();
            return Task.FromResult(IdentityResult.Success);
        }
        else
        {
            return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = $"User with ID {user.Id} not found." }));
        }
    }
    public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
    {
        user.Email = email;
        SaveChanges();
        return Task.CompletedTask;
    }

    public Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Email);
    }

    public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.EmailConfirmed);
    }

    public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
    {
        user.EmailConfirmed = confirmed;
        SaveChanges();
        return Task.CompletedTask;
    }

    public Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    {
        return Task.FromResult(_users.FirstOrDefault(u => u.NormalizedEmail == normalizedEmail));
    }

    public Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.NormalizedEmail);
    }

    public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
    {
        user.NormalizedEmail = normalizedEmail;
        SaveChanges();
        return Task.CompletedTask;
    }
    private void SaveChanges()
    {
        var json = JsonSerializer.Serialize(_users);
        File.WriteAllText(FilePath, json);
    }
}
