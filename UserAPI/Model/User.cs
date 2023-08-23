using Microsoft.AspNetCore.Identity;

namespace UserAPI.Model;

public class User :IdentityUser
{
    public DateTime BirthDate { get; set; }
    public User():base()
    {
        
    }
}
