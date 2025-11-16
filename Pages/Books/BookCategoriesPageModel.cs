using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vilcan_Andrea_Lab2.Data;
using Vilcan_Andrea_Lab2.Models;

namespace Vilcan_Andrea_Lab2.Pages.Books
{
    public class BookCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList { get; set; } = new();

        public void PopulateAssignedCategoryData(Vilcan_Andrea_Lab2Context context, Book book)
        {
            var allCategories = context.Category;
            var bookCategories = new HashSet<int>(
                book.BookCategories?.Select(c => c.CategoryID) ?? Enumerable.Empty<int>());

            AssignedCategoryDataList = new List<AssignedCategoryData>();

            foreach (var category in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = category.ID,
                    CategoryName = category.CategoryName,
                    Assigned = bookCategories.Contains(category.ID)
                });
            }
        }

        public void UpdateBookCategories(
            Vilcan_Andrea_Lab2Context context,
            string[]? selectedCategories,
            Book bookToUpdate)
        {
            if (selectedCategories == null || selectedCategories.Length == 0)
            {
                bookToUpdate.BookCategories = new List<BookCategory>();
                return;
            }

            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var currentCategories = new HashSet<int>(
                bookToUpdate.BookCategories?.Select(c => c.CategoryID) ?? Enumerable.Empty<int>());

            foreach (var category in context.Category)
            {
                var categoryIdString = category.ID.ToString();

                if (selectedCategoriesHS.Contains(categoryIdString))
                {
                    if (!currentCategories.Contains(category.ID))
                    {
                        bookToUpdate.BookCategories ??= new List<BookCategory>();
                        bookToUpdate.BookCategories.Add(new BookCategory
                        {
                            BookID = bookToUpdate.ID,
                            CategoryID = category.ID
                        });
                    }
                }
                else
                {
                    if (currentCategories.Contains(category.ID))
                    {
                        var categoryToRemove = bookToUpdate
                            .BookCategories!
                            .FirstOrDefault(c => c.CategoryID == category.ID);

                        if (categoryToRemove != null)
                        {
                            context.BookCategory.Remove(categoryToRemove);
                        }
                    }
                }
            }
        }
    }
}