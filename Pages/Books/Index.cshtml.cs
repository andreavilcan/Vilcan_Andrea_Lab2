using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vilcan_Andrea_Lab2.Data;
using Vilcan_Andrea_Lab2.Models;

namespace Vilcan_Andrea_Lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Vilcan_Andrea_Lab2.Data.Vilcan_Andrea_Lab2Context _context;

        public IndexModel(Vilcan_Andrea_Lab2.Data.Vilcan_Andrea_Lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; } = default!;
        public IList<Category> CategoryList { get; set; } = default!;
        public int? SelectedCategory { get; set; }
        public string TitleSort { get; set; } = string.Empty;
        public string AuthorSort { get; set; } = string.Empty;
        public string CurrentSort { get; set; } = string.Empty;
        public string CurrentFilter { get; set; } = string.Empty;
        public async Task OnGetAsync(
        string? sortOrder,
        string? currentFilter,
        string? searchString,
        int? CategoryID)
{
    // sort info
    CurrentSort = sortOrder ?? string.Empty;
    TitleSort = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
    AuthorSort = sortOrder == "Author" ? "author_desc" : "Author";

    // category filter
    SelectedCategory = CategoryID;
    CategoryList = await _context.Category.ToListAsync();

    // search logic
    if (!string.IsNullOrEmpty(searchString))
    {
        // nou search – pagina ar fi resetată la 1 dacă aveam pagination
    }
    else
    {
        searchString = currentFilter;
    }

    CurrentFilter = searchString ?? string.Empty;

    // baza query-ului
    var books = _context.Book
        .Include(b => b.Publisher)
        .Include(b => b.Author)
        .Include(b => b.BookCategories)
            .ThenInclude(bc => bc.Category)
        .AsQueryable();

    // filtrare după search (Title sau Author)
    if (!string.IsNullOrEmpty(searchString))
    {
        books = books.Where(b =>
            b.Title.Contains(searchString) ||
            (b.Author != null &&
             ((b.Author.FirstName + " " + b.Author.LastName).Contains(searchString))));
    }

    // filtrare după categorie
    if (CategoryID != null)
    {
        books = books.Where(b => b.BookCategories
            .Any(c => c.CategoryID == CategoryID));
    }

    // sortare
    books = sortOrder switch
    {
        "title_desc" => books.OrderByDescending(b => b.Title),
        "Author" => books
            .OrderBy(b => b.Author!.LastName)
            .ThenBy(b => b.Author.FirstName),
        "author_desc" => books
            .OrderByDescending(b => b.Author!.LastName)
            .ThenByDescending(b => b.Author.FirstName),
        _ => books.OrderBy(b => b.Title),
    };

    Book = await books.AsNoTracking().ToListAsync();
}
    }
}
