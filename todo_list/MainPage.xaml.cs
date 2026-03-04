namespace todo_list;

using System.Collections.ObjectModel;
using todo_list.Models;

public partial class MainPage : ContentPage
{
    private readonly ObservableCollection<ToDoClass> _todos = [];
    private int _nextId = 1;
    private ToDoClass? _selectedItem;

    public MainPage()
    {
        InitializeComponent();
        ListCard.Items = _todos;
    }

    // ── Add ──────────────────────────────────────────────────────────────────

    private async void AddToDoItem(object? sender, EventArgs e)
    {
        string title = FormCard.TitleText;
        string detail = FormCard.DetailsText;

        if (string.IsNullOrWhiteSpace(title))
        {
            await DisplayAlertAsync("Missing Title", "Please enter a title for your to-do.", "OK");
            return;
        }

        _todos.Add(new ToDoClass
        {
            id = _nextId++,
            title = title,
            detail = detail
        });

        FormCard.ClearForm();
    }

    // ── Edit ─────────────────────────────────────────────────────────────────

    private async void EditToDoItem(object? sender, EventArgs e)
    {
        if (_selectedItem is null) return;

        string title = FormCard.TitleText;
        string detail = FormCard.DetailsText;

        if (string.IsNullOrWhiteSpace(title))
        {
            await DisplayAlertAsync("Missing Title", "Please enter a title for your to-do.", "OK");
            return;
        }

        _selectedItem.title = title;
        _selectedItem.detail = detail;

        _selectedItem = null;
        FormCard.ClearForm();
        ListCard.DeselectItem();
    }

    // ── Cancel ───────────────────────────────────────────────────────────────

    private void CancelEdit(object? sender, EventArgs e)
    {
        _selectedItem = null;
        FormCard.ClearForm();
        ListCard.DeselectItem();
    }

    // ── Delete ───────────────────────────────────────────────────────────────

    private void DeleteToDoItem(object? sender, string classId)
    {
        if (!int.TryParse(classId, out int id)) return;

        var item = _todos.FirstOrDefault(t => t.id == id);
        if (item is not null)
        {
            _todos.Remove(item);

            // If the deleted item was being edited, reset the form
            if (_selectedItem?.id == id)
            {
                _selectedItem = null;
                FormCard.ClearForm();
            }
        }
    }

    // ── Selection ────────────────────────────────────────────────────────────

    private void TodoLV_OnItemSelected(object? sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is ToDoClass selected)
        {
            _selectedItem = selected;
            FormCard.LoadItem(selected);
        }
    }

    private void todoLV_ItemTapped(object? sender, ItemTappedEventArgs e)
    {
        // Deselect on second tap
        if (e.Item is ToDoClass tapped && _selectedItem?.id == tapped.id)
        {
            _selectedItem = null;
            FormCard.ClearForm();
            ListCard.DeselectItem();
        }
    }

    // ── Edit Icon ────────────────────────────────────────────────────────────

    private void HandleEditClick(object? sender, string classId)
    {
        if (!int.TryParse(classId, out int id)) return;

        var item = _todos.FirstOrDefault(t => t.id == id);
        if (item is not null)
        {
            _selectedItem = item;
            FormCard.LoadItem(item);
        }
    }
}
