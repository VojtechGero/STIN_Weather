using Microsoft.AspNetCore.Identity;
using System.Text.Json;
namespace STIN_Weather.Data;
public class JsonFileUserStore : IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserEmailStore<ApplicationUser>
{
    private string FilePath;
    private List<ApplicationUser> _users;

    public JsonFileUserStore(string Filepath= "users.json")
    {
        this.FilePath = Filepath;
        if (File.Exists(FilePath))
        {
            var json = File.ReadAllText(FilePath);
            try
            {
                _users = JsonSerializer.Deserialize<List<ApplicationUser>>(json);
            }
            catch(JsonException)
            {
                _users = new List<ApplicationUser>();
            }
            
        }
        else
        {
            _users = new List<ApplicationUser>();
        }
    }
    public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
        if (!_users.Any())
        {
            existingUser = user;
        }
        if (existingUser != null)
        {
            existingUser.PasswordHash = passwordHash;
            SaveChanges();
            return Task.CompletedTask;
        }
        throw new ArgumentException();
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
        var userToDelete = _users.FirstOrDefault(u => u.Id == user.Id);
        if (userToDelete != null)
        {
            _users.Remove(userToDelete);
            SaveChanges();
            return Task.FromResult(IdentityResult.Success);
        }
        return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "User not found." }));
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
        var SelectedUser = _users.FirstOrDefault(u => u.Id == user.Id);
        if (!_users.Any())
        {
            SelectedUser = user;
        }
        if (SelectedUser != null)
        {
            SelectedUser.NormalizedUserName = normalizedName;
            SaveChanges();
            var test=_users;
            return Task.CompletedTask;
        }
        throw new ArgumentException();
    }

    public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
    {
        var SelectedUser = _users.FirstOrDefault(u => u.Id == user.Id);
        if (!_users.Any())
        {
            SelectedUser = user;
        }
        if (SelectedUser != null)
        {
            SelectedUser.UserName = userName;
            SaveChanges();
            return Task.CompletedTask;
        }
        throw new ArgumentException();
    }

    public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
        if (!_users.Any())
        {
            existingUser = user;
        }
        if (existingUser != null)
        {
            _users.Remove(existingUser);
            _users.Add(user);
            SaveChanges();
            return Task.FromResult(IdentityResult.Success);
        }
        throw new ArgumentException();
    }
    public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
        if (!_users.Any())
        {
            existingUser = user;
        }
        if (existingUser != null)
        {
            existingUser.Email = email;
            SaveChanges();
            return Task.CompletedTask;
        }
        throw new ArgumentException();
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
        var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
        if (!_users.Any())
        {
            existingUser = user;
        }
        if (existingUser != null)
        {
            existingUser.EmailConfirmed = confirmed;
            SaveChanges();
            return Task.CompletedTask;
        }
        throw new ArgumentException();
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
        var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
        if (!_users.Any())
        {
            existingUser = user;
        }
        if (existingUser != null)
        {
            existingUser.NormalizedEmail = normalizedEmail;
            SaveChanges();
            return Task.CompletedTask;
        }
        throw new ArgumentException();
    }
    private void SaveChanges()
    {
        var json = JsonSerializer.Serialize(_users);
        File.WriteAllText(FilePath, json);
    }
}
