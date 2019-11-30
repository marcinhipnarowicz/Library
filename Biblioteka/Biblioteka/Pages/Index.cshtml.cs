using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteka.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Biblioteka.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Biblioteka.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IList<Book> Book { get; set; }

        public IQueryable<Book> RandomBooks;

        public IndexModel(Biblioteka.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                Random r = new Random();
                RandomBooks = _context.Book.Where(x => x.IdUser != user.Id && x.Image != null).OrderBy(x => r.Next()).Take(3);
            }
            else
            {
                Random r = new Random();
                RandomBooks = _context.Book.Where(x => x.Image != null).OrderBy(x => r.Next()).Take(3);
            }
        }
    }
}