using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Data;
using Biblioteka.Model;
using Microsoft.AspNetCore.Identity;

namespace Biblioteka.Pages.test
{
    public class DetailsModel : PageModel
    {
        private readonly Biblioteka.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public string Idpom;

        public DetailsModel(Biblioteka.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Book Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                Idpom = user.Id;
            }

            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Book.FirstOrDefaultAsync(m => m.Id == id);

            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}