using Microsoft.AspNetCore.Identity;
using STIN_Weather.Data;
using System.Text.Json;
namespace Tests;

[TestClass]
public class JsonFileUserStoreTests
{
    private string _tempFilePath;
    private JsonFileUserStore _store;
    private List<ApplicationUser> _testUsers;

    [TestInitialize]
    public void Initialize()
    {
        _tempFilePath = Path.GetTempFileName();
        _testUsers = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "1", UserName = "user1", NormalizedUserName = "USER1", Email = "user1@example.com", NormalizedEmail = "USER1@EXAMPLE.COM", PasswordHash = "hash1" },
                new ApplicationUser { Id = "2", UserName = "user2", NormalizedUserName = "USER2", Email = "user2@example.com", NormalizedEmail = "USER2@EXAMPLE.COM", PasswordHash = "hash2" }
            };
        WriteUsersToJsonFile(_testUsers);

        _store = new JsonFileUserStore(_tempFilePath);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _store.Dispose();
        if (File.Exists(_tempFilePath))
        {
            File.Delete(_tempFilePath);
        }
        
    }
    [TestMethod]
    public async Task DeleteAsync_DeletesUserCorrectly()
    {
        // Arrange
        var before = GetUsersFromJsonFile();
        // Act
        await _store.DeleteAsync(_testUsers[0], CancellationToken.None);

        // Assert
        var users = GetUsersFromJsonFile();
        Assert.AreEqual(before.Count-1, users.Count);
        Assert.AreEqual("2", users[0].Id);
    }
    [TestMethod]
    public async Task CreateAsync_AddsUserCorrectly()
    {
        // Arrange
        var newUser = new ApplicationUser { Id = "3", UserName = "user3" };
        var before = GetUsersFromJsonFile();
        // Act
        await _store.CreateAsync(newUser, CancellationToken.None);

        // Assert
        var users = GetUsersFromJsonFile();
        Assert.IsNotNull(users);
        Assert.AreEqual(before.Count+1, users.Count);
        Assert.AreEqual(newUser.Id, users[2].Id);
        Assert.AreEqual(newUser.UserName, users[2].UserName);
    }
    [TestMethod]
    public async Task FindByIdAsync_ReturnsCorrectUser()
    {
        // Act
        var user = await _store.FindByIdAsync("1", CancellationToken.None);

        // Assert
        Assert.IsNotNull(user);
        Assert.AreEqual("user1", user.UserName);
    }

    [TestMethod]
    public async Task FindByNameAsync_ReturnsCorrectUser()
    {
        // Act
        var user = await _store.FindByNameAsync("USER1", CancellationToken.None);

        // Assert
        Assert.IsNotNull(user);
        Assert.AreEqual("1", user.Id);
    }

    [TestMethod]
    public async Task UpdateAsync_UpdatesUserCorrectly()
    {
        // Arrange
        var userToUpdate = new ApplicationUser { Id = "1", UserName = "updatedUser1" };

        // Act
        await _store.UpdateAsync(userToUpdate, CancellationToken.None);

        // Assert
        var users = GetUsersFromJsonFile();
        var updatedUser = users.Find(u => u.Id == "1");
        Assert.IsNotNull(updatedUser);
        Assert.AreEqual("updatedUser1", updatedUser.UserName);
    }

    [TestMethod]
    public async Task SetEmailAsync_UpdatesUserEmailCorrectly()
    {
        // Arrange
        var userEmailToUpdate = "updatedemail@example.com";
        var user = _testUsers.First();

        // Act
        await _store.SetEmailAsync(user, userEmailToUpdate, CancellationToken.None);

        // Assert
        var users = GetUsersFromJsonFile();
        var updatedUser = users.Find(u => u.Id == user.Id);
        Assert.IsNotNull(updatedUser);
        Assert.AreEqual(userEmailToUpdate, updatedUser.Email);
    }

    [TestMethod]
    public async Task SetUserNameAsync_SetsUserNameCorrectly()
    {
        // Arrange
        var user = _testUsers.First();
        string newName = "Slop";

        // Act
        await _store.SetUserNameAsync(user, newName, CancellationToken.None);

        // Assert
        var users = GetUsersFromJsonFile();
        var updatedUser = users.Find(u => u.Id == user.Id);
        Assert.IsNotNull(updatedUser);
        Assert.AreEqual(newName, updatedUser.UserName);
    }

    [TestMethod]
    public async Task SetEmailConfirmedAsync_SetsEmailConfirmedCorrectly()
    {
        // Arrange
        var user = _testUsers.First();
        bool emailConfirmed = true;

        // Act
        await _store.SetEmailConfirmedAsync(user, emailConfirmed, CancellationToken.None);

        // Assert
        var users = GetUsersFromJsonFile();
        var updatedUser = users.Find(u => u.Id == user.Id);
        Assert.IsNotNull(updatedUser);
        Assert.AreEqual(emailConfirmed, updatedUser.EmailConfirmed);
    }


    [TestMethod]
    public async Task DeleteAsync_FailsGracefullyWhenUserNotFound()
    {
        // Arrange
        var userToDelete = new ApplicationUser { Id = "nonexistent" };

        // Act 
        var result = await _store.DeleteAsync(userToDelete, CancellationToken.None);

        // Assert
        Assert.IsFalse(result.Succeeded);
        Assert.AreEqual("User not found.", result.Errors.First().Description);
    }

    [TestMethod]
    public async Task SetPasswordHashAsync_SetsPasswordCorrectly()
    {
        // Arrange
        var user = _testUsers.First();
        var newPasswordHash = "newHash123";

        // Act & Assert
        await _store.SetPasswordHashAsync(user, newPasswordHash, CancellationToken.None);

        // Assert
        var users = GetUsersFromJsonFile();
        var updatedUser = users.FirstOrDefault(u => u.Id == user.Id);
        Assert.IsNotNull(updatedUser);
        Assert.AreEqual(newPasswordHash, updatedUser.PasswordHash);
    }

    [TestMethod]
    public async Task SetNormalizedUserNameAndEmailAsync_SetsValuesCorrectly()
    {
        // Arrange
        var user = GetUsersFromJsonFile().First();
        var normalizedUserName = "NORMALIZEDUSERNAME";
        var normalizedEmail = "NORMALIZEDEMAIL@EXAMPLE.COM";

        // Act
        await _store.SetNormalizedUserNameAsync(user, normalizedUserName, CancellationToken.None);
        await _store.SetNormalizedEmailAsync(user, normalizedEmail, CancellationToken.None);

        // Assert
        var users = GetUsersFromJsonFile();
        var updatedUser = users.FirstOrDefault(u => u.Id == user.Id);
        Assert.IsNotNull(updatedUser);
        Assert.AreEqual(normalizedUserName, updatedUser.NormalizedUserName);
        Assert.AreEqual(normalizedEmail, updatedUser.NormalizedEmail);
    }
    [TestMethod]
    public async Task RandomGetsTest()
    {
        var user = new ApplicationUser { Id = "9", UserName = "user9", NormalizedUserName = "USER9", Email = "user9@example.com", NormalizedEmail = "USER9@EXAMPLE.COM", PasswordHash = "hash9" ,EmailConfirmed=false};
        Assert.AreEqual(await _store.GetEmailAsync(user,CancellationToken.None), user.Email);
        Assert.AreEqual(await _store.GetNormalizedEmailAsync(user, CancellationToken.None), user.NormalizedEmail);
        Assert.AreEqual(await _store.GetPasswordHashAsync(user, CancellationToken.None), user.PasswordHash);
        Assert.AreEqual(await _store.GetUserIdAsync(user, CancellationToken.None), user.Id);
        Assert.AreEqual(await _store.GetNormalizedUserNameAsync(user, CancellationToken.None), user.NormalizedUserName);
        Assert.AreEqual(await _store.GetUserNameAsync(user, CancellationToken.None), user.UserName);
        Assert.IsTrue(await _store.HasPasswordAsync(user, CancellationToken.None));
        Assert.IsFalse(await _store.GetEmailConfirmedAsync(user, CancellationToken.None));
        
    }

    [TestMethod]
    public async Task RandomArgumentExceptionsTest()
    {
        var user = new ApplicationUser { Id = "9", UserName = "user9", NormalizedUserName = "USER9", Email = "user9@example.com", NormalizedEmail = "USER9@EXAMPLE.COM", PasswordHash = "hash9", EmailConfirmed = false };

        Assert.ThrowsExceptionAsync<ArgumentException>(() =>_store.SetEmailAsync(user,"",CancellationToken.None));
        Assert.ThrowsExceptionAsync<ArgumentException>(() => _store.SetNormalizedEmailAsync(user, "", CancellationToken.None));
        Assert.ThrowsExceptionAsync<ArgumentException>(() => _store.SetEmailConfirmedAsync(user, false, CancellationToken.None));
        Assert.ThrowsExceptionAsync<ArgumentException>(() => _store.SetNormalizedUserNameAsync(user, "", CancellationToken.None));
        Assert.ThrowsExceptionAsync<ArgumentException>(() => _store.SetUserNameAsync(user, "", CancellationToken.None));
        Assert.ThrowsExceptionAsync<ArgumentException>(() => _store.SetPasswordHashAsync(user, "", CancellationToken.None));
    }
    private List<ApplicationUser> GetUsersFromJsonFile()
    {
        string json = File.ReadAllText(_tempFilePath);
        return JsonSerializer.Deserialize<List<ApplicationUser>>(json) ?? new List<ApplicationUser>();
    }

    private void WriteUsersToJsonFile(List<ApplicationUser> users)
    {
        var json = JsonSerializer.Serialize(users);
        File.WriteAllText(_tempFilePath, json);
    }
}