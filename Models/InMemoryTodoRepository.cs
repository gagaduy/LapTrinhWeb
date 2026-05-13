namespace Buoi4_B2.Models;

public class InMemoryTodoRepository : ITodoRepository
{
    private readonly List<TodoItem> _items =
    [
        new() { Id = 1, Name = "Di cho", IsCompleted = true },
        new() { Id = 2, Name = "Choi the thao", IsCompleted = false },
        new() { Id = 3, Name = "Choi game", IsCompleted = false },
        new() { Id = 4, Name = "Hoc bai", IsCompleted = false }
    ];

    public IReadOnlyList<TodoItem> GetAll()
    {
        return _items.Select(Clone).ToList();
    }

    public TodoItem? GetById(int id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        return item is null ? null : Clone(item);
    }

    public void Add(TodoItem item)
    {
        _items.Add(Clone(item));
    }

    public void Update(TodoItem item)
    {
        var existing = _items.FirstOrDefault(x => x.Id == item.Id);

        if (existing is null)
        {
            return;
        }

        existing.Name = item.Name;
        existing.IsCompleted = item.IsCompleted;
    }

    public void Delete(int id)
    {
        var existing = _items.FirstOrDefault(x => x.Id == id);

        if (existing is null)
        {
            return;
        }

        _items.Remove(existing);
    }

    private static TodoItem Clone(TodoItem item)
    {
        return new TodoItem
        {
            Id = item.Id,
            Name = item.Name,
            IsCompleted = item.IsCompleted
        };
    }
}
