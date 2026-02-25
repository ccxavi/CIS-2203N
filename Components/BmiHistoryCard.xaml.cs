using System.Collections.ObjectModel;
using bmi_calculator.Models;

namespace bmi_calculator.Components;

public partial class BmiHistoryCard : ContentView
{
    private ObservableCollection<BmiRecord> _items = [];

    public ObservableCollection<BmiRecord> Items
    {
        get => _items;
        set
        {
            _items = value;
            HistoryList.ItemsSource = _items;
            _items.CollectionChanged += (_, _) => UpdateCount();
            UpdateCount();
        }
    }

    private void UpdateCount()
    {
        CountLabel.Text = _items.Count == 1 ? "1 entry" : $"{_items.Count} entries";
    }

    public BmiHistoryCard()
    {
        InitializeComponent();
    }
}
