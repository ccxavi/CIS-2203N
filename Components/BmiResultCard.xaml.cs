namespace bmi_calculator.Components;

public partial class BmiResultCard : ContentView
{
    // ── BmiValue ──────────────────────────────────────────────────────────────
    public static readonly BindableProperty BmiValueProperty =
        BindableProperty.Create(nameof(BmiValue), typeof(string), typeof(BmiResultCard),
            string.Empty, propertyChanged: OnDataChanged);

    public string BmiValue
    {
        get => (string)GetValue(BmiValueProperty);
        set => SetValue(BmiValueProperty, value);
    }

    // ── Category ──────────────────────────────────────────────────────────────
    public static readonly BindableProperty CategoryProperty =
        BindableProperty.Create(nameof(Category), typeof(string), typeof(BmiResultCard),
            string.Empty, propertyChanged: OnDataChanged);

    public string Category
    {
        get => (string)GetValue(CategoryProperty);
        set => SetValue(CategoryProperty, value);
    }

    // ── Description ───────────────────────────────────────────────────────────
    public static readonly BindableProperty DescriptionProperty =
        BindableProperty.Create(nameof(Description), typeof(string), typeof(BmiResultCard),
            string.Empty, propertyChanged: OnDataChanged);

    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    private static void OnDataChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is BmiResultCard card)
            card.UpdateUI();
    }

    private void UpdateUI()
    {
        BmiValueLabel.Text = BmiValue;
        CategoryLabel.Text = Category;
        DescriptionLabel.Text = Description;
    }

    public BmiResultCard()
    {
        InitializeComponent();
    }
}
