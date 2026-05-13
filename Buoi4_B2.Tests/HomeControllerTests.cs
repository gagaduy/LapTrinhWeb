using Buoi4_B2.Controllers;
using Buoi4_B2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Buoi4_B2.Tests;

public class HomeControllerTests
{
    [Fact]
    public void Index_Redirects_To_TodoList()
    {
        var controller = CreateController();

        var result = controller.Index();

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("TodoList", redirect.ActionName);
    }

    [Fact]
    public void Create_Returns_Empty_Todo_Item_View()
    {
        var controller = CreateController();

        var result = controller.Create();

        var view = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<TodoItem>(view.Model);
        Assert.Equal(0, model.Id);
        Assert.Equal(string.Empty, model.Name);
        Assert.False(model.IsCompleted);
    }

    [Fact]
    public void Create_Post_Adds_Item_And_Redirects_To_TodoList()
    {
        var controller = CreateController();

        var result = controller.Create(new TodoItem
        {
            Id = 5,
            Name = "Doc sach",
            IsCompleted = true
        });

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("TodoList", redirect.ActionName);

        var listResult = controller.TodoList();
        var listView = Assert.IsType<ViewResult>(listResult);
        var items = Assert.IsAssignableFrom<IReadOnlyList<TodoItem>>(listView.Model);
        Assert.Equal(5, items.Count);
        var created = Assert.Single(items, x => x.Id == 5);
        Assert.Equal("Doc sach", created.Name);
        Assert.True(created.IsCompleted);
    }

    [Fact]
    public void Edit_Returns_First_Default_Item()
    {
        var controller = CreateController();

        var result = controller.Edit(1);

        var view = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<TodoItem>(view.Model);
        Assert.Equal(1, model.Id);
        Assert.Equal("Di cho", model.Name);
        Assert.True(model.IsCompleted);
    }

    [Fact]
    public void Edit_Post_Updates_Item_And_Redirects_To_TodoList()
    {
        var controller = CreateController();

        var result = controller.Edit(new TodoItem
        {
            Id = 1,
            Name = "Di sieu thi",
            IsCompleted = false
        });

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("TodoList", redirect.ActionName);

        var listResult = controller.TodoList();
        var listView = Assert.IsType<ViewResult>(listResult);
        var items = Assert.IsAssignableFrom<IReadOnlyList<TodoItem>>(listView.Model);
        var updated = Assert.Single(items, x => x.Id == 1);
        Assert.Equal("Di sieu thi", updated.Name);
        Assert.False(updated.IsCompleted);
    }

    [Fact]
    public void Delete_Removes_Item_And_Redirects_To_TodoList()
    {
        var controller = CreateController();

        var result = controller.Delete(2);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("TodoList", redirect.ActionName);

        var listResult = controller.TodoList();
        var listView = Assert.IsType<ViewResult>(listResult);
        var items = Assert.IsAssignableFrom<IReadOnlyList<TodoItem>>(listView.Model);
        Assert.Equal(3, items.Count);
        Assert.DoesNotContain(items, x => x.Id == 2);
    }

    [Fact]
    public void Details_Returns_Item_View()
    {
        var controller = CreateController();

        var result = controller.Details(3);

        var view = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<TodoItem>(view.Model);
        Assert.Equal(3, model.Id);
        Assert.Equal("Choi game", model.Name);
        Assert.False(model.IsCompleted);
    }

    [Fact]
    public void TodoList_Returns_Four_Default_Items()
    {
        var controller = CreateController();

        var result = controller.TodoList();

        var view = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IReadOnlyList<TodoItem>>(view.Model);
        Assert.Equal(4, model.Count);
        Assert.Collection(
            model,
            item =>
            {
                Assert.Equal("Di cho", item.Name);
                Assert.True(item.IsCompleted);
            },
            item => Assert.Equal("Choi the thao", item.Name),
            item => Assert.Equal("Choi game", item.Name),
            item => Assert.Equal("Hoc bai", item.Name));
    }

    private static HomeController CreateController()
    {
        return new HomeController(NullLogger<HomeController>.Instance, new InMemoryTodoRepository());
    }
}
