namespace Buoi5.Models.ViewModels;

public class BookCatalogViewModel
{
    public List<Book> Books { get; set; } = new();
    public List<CategorySummaryViewModel> Categories { get; set; } = new();
    public int? SelectedCategoryId { get; set; }
}
