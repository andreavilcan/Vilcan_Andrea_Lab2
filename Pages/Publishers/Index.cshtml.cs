using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vilcan_Andrea_Lab2.Data;
using Vilcan_Andrea_Lab2.Models;
using Vilcan_Andrea_Lab2.ViewModels;

namespace Vilcan_Andrea_Lab2.Pages.Publishers
{
    public class IndexModel : PageModel
    {
        private readonly Vilcan_Andrea_Lab2Context _context;

        public IndexModel(Vilcan_Andrea_Lab2Context context)
        {
            _context = context;
        }

        // păstrăm lista simplă de Publisher dacă o cere generatorul
        public IList<Publisher> Publisher { get; set; } = default!;

        public PublisherIndexData PublisherData { get; set; } = default!;
        public int PublisherID { get; set; }
        public int BookID { get; set; }

        public async Task OnGetAsync(int? id, int? bookID)
        {
            PublisherData = new PublisherIndexData();

            PublisherData.Publishers = await _context.Publisher
                .Include(p => p.Books)
                    .ThenInclude(b => b.Author)
                .OrderBy(p => p.PublisherName)
                .ToListAsync();

            if (id != null)
            {
                PublisherID = id.Value;
                var publisher = PublisherData.Publishers
                    .Single(p => p.ID == id.Value);

                PublisherData.Books = publisher.Books ?? new List<Book>();
            }
        }
    }
}