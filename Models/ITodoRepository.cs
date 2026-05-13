namespace Buoi4_B2.Models;

public interface ITodoRepository
{
    IReadOnlyList<TodoItem> GetAll();

    TodoItem? GetById(int id);

    void Add(TodoItem item);

    void Update(TodoItem item);

    void Delete(int id);
}
