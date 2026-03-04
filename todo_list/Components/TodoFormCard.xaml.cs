namespace todo_list.Components;

using todo_list.Models;

public partial class TodoFormCard : ContentView
{
    public event EventHandler? AddClicked;
    public event EventHandler? EditClicked;
    public event EventHandler? CancelClicked;

    public string TitleText => titleEntry.Text?.Trim() ?? string.Empty;
    public string DetailsText => detailsEditor.Text?.Trim() ?? string.Empty;

    public TodoFormCard()
    {
        InitializeComponent();
    }

    /// <summary>Populate the form and switch to edit mode.</summary>
    public void LoadItem(ToDoClass item)
    {
        titleEntry.Text = item.title;
        detailsEditor.Text = item.detail;
        addBtn.IsVisible = false;
        editRow.IsVisible = true;
    }

    /// <summary>Clear fields and switch back to add mode.</summary>
    public void ClearForm()
    {
        titleEntry.Text = string.Empty;
        detailsEditor.Text = string.Empty;
        addBtn.IsVisible = true;
        editRow.IsVisible = false;
    }

    private void OnAddClicked(object? sender, EventArgs e) => AddClicked?.Invoke(this, e);
    private void OnEditClicked(object? sender, EventArgs e) => EditClicked?.Invoke(this, e);
    private void OnCancelClicked(object? sender, EventArgs e) => CancelClicked?.Invoke(this, e);
}
