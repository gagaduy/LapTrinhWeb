namespace Buoi5.Models.ViewModels;

public class CategorySummaryViewModel
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int BookCount { get; set; }
}
