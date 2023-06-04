using Microsoft.AspNetCore.Identity;

namespace TestBlog.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        
        //relation
        public List<Post>? Posts { get; set; }
    }
}
