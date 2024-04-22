using Microsoft.AspNetCore.Identity;

namespace STIN_Weather.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public List<SavedLocation> savedLocations { get; set; } = new List<SavedLocation>();
    }

}
