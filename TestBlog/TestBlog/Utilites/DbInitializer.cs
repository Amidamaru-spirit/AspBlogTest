
using TestBlog.Data;
using TestBlog.Models;
using Microsoft.AspNetCore.Identity;

namespace TestBlog.Utilites
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext context,
                            UserManager<ApplicationUser> userManager, 
                            RoleManager<IdentityRole> roleManager) 
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {
            if(!_roleManager.RoleExistsAsync(WebsiteRoles.WebsiteAdmin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebsiteAdmin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebsiteAuthor)).GetAwaiter().GetResult();
                _userManager.CreateAsync(new ApplicationUser()
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    FirstName = "Super",
                    LastName = "Admin"
                }, "Admin@0011").Wait();

                var appUser = _context.ApplicationUsers.FirstOrDefault(x=>x.Email=="admin@gmail.com");

                if (appUser != null)
                {
                    _userManager.AddToRoleAsync(appUser, WebsiteRoles.WebsiteAdmin).GetAwaiter().GetResult();
                }

                var AboutPage = new Page()
                {
                    Title = "о нас",
                    Slug = "about-us"
                };

                var ContactPage = new Page()
                {
                    Title = "свяжитесь с нами",
                    Slug = "contact"
                };

                var PrivacyPolicyPage = new Page()
                {
                    Title = "Privacy",
                    Slug = "privacy"
                };

                _context.Pages.Add(AboutPage);
                _context.Pages.Add(ContactPage);
                _context.Pages.Add(PrivacyPolicyPage);
                _context.SaveChanges();

                var listOfPages = new List<Page>()
                {
                    new Page()
                    {
                    Title = "о нас",
                    Slug = "about-us"
                    },

                    new Page()
                    {
                        Title = "свяжитесь с нами",
                        Slug = "contact"
                    },

                    new Page()
                    {
                        Title = "Privacy",
                        Slug = "privacy"
                    }
                };

                _context.Pages.AddRange(listOfPages);
                _context.SaveChanges();
            }
        }
    }
}
