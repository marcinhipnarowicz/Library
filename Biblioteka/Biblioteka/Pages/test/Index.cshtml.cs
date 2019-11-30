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
    public class IndexModel : PageModel
    {
        private readonly Biblioteka.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public string CurrentFilter { get; private set; }

        public string TitleSort { get; set; }
        public string NameSort { get; set; }
        public string SurnameSort { get; set; }
        public string CategorySort { get; set; }
        public string ISBNSort { get; set; }

        public IList<Book> Book { get; set; }

        public List<Book> UserBooks = new List<Book>();

        public IndexModel(Biblioteka.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [TempData]
        public string Message { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            TitleSort = sortOrder == "Title" ? "title_desc" : "Title";
            NameSort = sortOrder == "Name" ? "name_desc" : "Name";
            SurnameSort = sortOrder == "Surname" ? "surname_desc" : "Surname";
            ISBNSort = sortOrder == "ISBN" ? "isbn_desc" : "ISBN";
            CategorySort = sortOrder == "Category" ? "category_desc" : "Category";

            CurrentFilter = searchString;

            IQueryable<Book> bookIQ = from b in _context.Book
                                      where b.IdUser == user.Id
                                      select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                bookIQ = bookIQ.Where(b => b.Title.Contains(searchString)
                                        || b.Name.Contains(searchString)
                                        || b.Surname.Contains(searchString)
                                        || b.Category.Contains(searchString)
                                        || b.ISBN.Contains(searchString)
                                        || b.OwnershipStatus.Contains(searchString)
                                        || b.Summary.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Title":
                    bookIQ = bookIQ.OrderByDescending(b => b.Title);
                    break;

                case "title_desc":
                    bookIQ = bookIQ.OrderBy(b => b.Title);
                    break;

                case "Name":
                    bookIQ = bookIQ.OrderByDescending(b => b.Name);
                    break;

                case "name_desc":
                    bookIQ = bookIQ.OrderBy(b => b.Name);
                    break;

                case "Surname":
                    bookIQ = bookIQ.OrderByDescending(b => b.Surname);
                    break;

                case "surname_desc":
                    bookIQ = bookIQ.OrderBy(b => b.Surname);
                    break;

                case "ISBN":
                    bookIQ = bookIQ.OrderByDescending(b => b.ISBN);
                    break;

                case "isbn_desc":
                    bookIQ = bookIQ.OrderBy(b => b.ISBN);
                    break;

                case "Category":
                    bookIQ = bookIQ.OrderByDescending(b => b.Category);
                    break;

                case "category_desc":
                    bookIQ = bookIQ.OrderBy(b => b.Category);
                    break;
            }
            Book = await bookIQ.AsNoTracking().ToListAsync();

            UserBooks = Book.Where(item => item.IdUser == user.Id).ToList();
        }
    }
}