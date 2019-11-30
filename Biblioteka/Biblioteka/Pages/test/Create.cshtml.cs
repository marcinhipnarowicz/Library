using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Biblioteka.Data;
using Biblioteka.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Biblioteka.Pages.test
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; }

        [TempData]
        public string Message { get; set; }

        public async Task<IActionResult> OnPostAsync(Book book, IFormFile Image2)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            Book.IdUser = user.Id;

            Book.Title = Book.Title.Trim();
            Book.Name = Book.Name.Trim();
            Book.Surname = Book.Surname.Trim();
            Book.Summary = Book.Summary?.Trim();

            if (Image2 != null)
            {
                if (Image2.Length > 0)
                {
                    byte[] p1 = null;

                    using (var fs1 = Image2.OpenReadStream())
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();
                    }

                    Book.Image = p1;
                }
            }

            _context.Book.Add(Book);
            await _context.SaveChangesAsync();

            Message = "Ksiażka " + Book.Title + " została stworzona";

            return RedirectToPage("./Index");
        }
    }
}