using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Data;
using Biblioteka.Model;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Identity;

namespace Biblioteka.Pages.test
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public string pomIdUser;

        public EditModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Book Book { get; set; }

        [TempData]
        public string Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string userID)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Book.FirstOrDefaultAsync(m => m.Id == id);
            Book.IdUser = userID;

            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile Image2)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            Book.IdUser = user.Id;

            if (!ModelState.IsValid)
            {
                return Page();
            }

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

            _context.Attach(Book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(Book.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Message = "Książka " + Book.Title + " została zaktualizowana";

            return RedirectToPage("./Index");
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}