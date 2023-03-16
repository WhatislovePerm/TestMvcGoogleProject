using Microsoft.AspNetCore.Identity;

namespace TestMvcGoogleProject.Data.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public string PictureUrl { get; internal set; }
    }
}

