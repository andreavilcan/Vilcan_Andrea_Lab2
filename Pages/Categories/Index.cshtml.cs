using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vilcan_Andrea_Lab2.Data;
using Vilcan_Andrea_Lab2.Models;
using Vilcan_Andrea_Lab2.ViewModels;  

namespace Vilcan_Andrea_Lab2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Vilcan_Andrea_Lab2.Data.Vilcan_Andrea_Lab2Context _context;

        public IndexModel(Vilcan_Andrea_Lab2.Data.Vilcan_Andrea_Lab2Context context)
        {
            _context = context;
        }
        public CategoryIndexData CategoryData { get; set; } = default!;
        public int CategoryID { get; set; }
        public int BookID { get; set; }
        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync(int? id, int? bookID)
{
    CategoryData = new CategoryIndexData();

    CategoryData.Categories = await _context.Category
        .Include(c => c.BookCategories)
            .ThenInclude(bc => bc.Book)
                .ThenInclude(b => b.Author)
        .OrderBy(c => c.CategoryName)
        .ToListAsync();
    Category = CategoryData.Categories.ToList();

    if (id != null)
    {
        CategoryID = id.Value;
        var category = CategoryData.Categories
            .Single(c => c.ID == id.Value);

        CategoryData.Books = category.BookCategories?
            .Select(bc => bc.Book)
            .Where(b => b != null)! 
            .ToList()
            ?? new List<Book>();
    }
}
    }
}
