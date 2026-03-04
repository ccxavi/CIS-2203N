namespace todo_list.Components;

using System.Collections.ObjectModel;
using todo_list.Models;

public partial class TodoListCard : ContentView
{
    public event EventHandler<SelectedItemChangedEventArgs>? ItemSelected;
    public event EventHandler<ItemTappedEventArgs>? ItemTapped;
    public event EventHandler<string>? DeleteClicked;
    public event EventHandler<string>? EditClicked;
    public event EventHandler? ClearAllClicked;

    private ObservableCollection<ToDoClass> _items = [];

    public ObservableCollection<ToDoClass> Items
    {
        get => _items;
        set
        {
            _items = value;
            todoLV.ItemsSource = _items;
            _items.CollectionChanged += (_, _) => UpdateView();
            UpdateView();
        }
    }

    public TodoListCard()
    {
        InitializeComponent();
    }

    public void DeselectItem()
    {
        todoLV.SelectedItem = null;
    }

    private void UpdateView()
    {
        bool hasItems = _items.Count > 0;
        emptyLabel.IsVisible = !hasItems;
        todoLV.IsVisible = hasItems;
        clearAllButton.IsVisible = hasItems;
        countLabel.Text = _items.Count == 1 ? "  •  1 item" : $"  •  {_items.Count} items";
    }

    private void ListView_ItemSelected(object? sender, SelectedItemChangedEventArgs e)
        => ItemSelected?.Invoke(this, e);

    private void ListView_ItemTapped(object? sender, ItemTappedEventArgs e)
        => ItemTapped?.Invoke(this, e);

    private void DeleteButton_Clicked(object? sender, EventArgs e)
    {
        if (sender is ImageButton btn)
            DeleteClicked?.Invoke(this, btn.ClassId);
    }

    private void EditButton_Clicked(object? sender, EventArgs e)
    {
        if (sender is ImageButton btn)
            EditClicked?.Invoke(this, btn.ClassId);
    }

    private void ClearAllButton_Clicked(object? sender, EventArgs e)
        => ClearAllClicked?.Invoke(this, e);

    private void EditIconTapped(object? sender, TappedEventArgs e)
    {
        if (e.Parameter is int id)
            EditClicked?.Invoke(this, id.ToString());
    }

    private void DeleteIconTapped(object? sender, TappedEventArgs e)
    {
        if (e.Parameter is int id)
            DeleteClicked?.Invoke(this, id.ToString());
    }
}
