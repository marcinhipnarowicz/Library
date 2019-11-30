using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Data;
using Biblioteka.Model;

namespace Biblioteka.Pages.test
{
    public class DeleteModel : PageModel
    {
        private readonly Biblioteka.Data.ApplicationDbContext _context;

        public DeleteModel(Biblioteka.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; }

        [TempData]
        public string Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Book.FindAsync(id);

            if (Book != null)
            {
                _context.Book.Remove(Book);
                await _context.SaveChangesAsync();
            }

            Message = "Książka została usunięta";

            return RedirectToPage("./Index");
        }
    }
}