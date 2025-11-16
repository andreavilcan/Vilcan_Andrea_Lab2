using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vilcan_Andrea_Lab2.Data;
using Vilcan_Andrea_Lab2.Models;
using Microsoft.AspNetCore.Authorization;

namespace Vilcan_Andrea_Lab2.Pages.Books
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : BookCategoriesPageModel
    {
        private readonly Vilcan_Andrea_Lab2Context _context;

        public CreateModel(Vilcan_Andrea_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = new Book();

        [BindProperty]
        public string[]? SelectedCategories { get; set; }

        public IActionResult OnGet()
        {
            Book = new Book
            {
                BookCategories = new List<BookCategory>()
            };

            PopulateAssignedCategoryData(_context, Book);

            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName");
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "LastName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateAssignedCategoryData(_context, Book);
                ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName");
                ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "LastName");
                return Page();
            }

            Book.BookCategories = new List<BookCategory>();
            UpdateBookCategories(_context, SelectedCategories, Book);

            _context.Book.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}