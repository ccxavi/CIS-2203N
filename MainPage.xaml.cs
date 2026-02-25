namespace bmi_calculator;

public partial class MainPage : ContentPage
{
    // WHO standard constraints
    private const double MinHeightCm = 50.0;
    private const double MaxHeightCm = 272.0;
    private const double MinWeightKg = 0.5;
    private const double MaxWeightKg = 500.0;

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCalculateClicked(object? sender, EventArgs e)
    {
        // Parse height (cm) and weight (kg)
        if (!double.TryParse(HeightInput.Value, out double heightCm) || !IsValidHeight(heightCm))
        {
            await DisplayAlertAsync("Invalid Height", $"Please enter a valid height between {MinHeightCm} and {MaxHeightCm} centimetres.", "OK");
            return;
        }

        if (!double.TryParse(WeightInput.Value, out double weightKg) || !IsValidWeight(weightKg))
        {
            await DisplayAlertAsync("Invalid Weight", $"Please enter a valid weight between {MinWeightKg} and {MaxWeightKg} kilograms.", "OK");
            return;
        }

        // Calculate BMI
        double heightM = heightCm / 100.0;
        double bmi = weightKg / (heightM * heightM);

        // Determine category and description
        string category;
        string description;

        if (bmi < 18.5)
        {
            category = "Underweight";
            description = "Your BMI suggests your body weight may be lower than recommended for your height.";
        }
        else if (bmi < 25.0)
        {
            category = "Normal Weight";
            description = "Your BMI is within the healthy range. Keep maintaining a balanced diet and regular activity.";
        }
        else if (bmi < 30.0)
        {
            category = "Overweight";
            description = "Your BMI is above the healthy range. Consider a balanced diet and increased physical activity.";
        }
        else
        {
            category = "Obese";
            description = "Your BMI indicates obesity. Consulting a healthcare professional is recommended.";
        }

        // Update result card
        ResultCard.BmiValue = bmi.ToString("F1");
        ResultCard.Category = category;
        ResultCard.Description = description;
        ResultCard.IsVisible = true;
    }

    private void OnResetClicked(object? sender, EventArgs e)
    {
        HeightInput.Value = string.Empty;
        WeightInput.Value = string.Empty;
        ResultCard.IsVisible = false;
    }

    private bool IsValidHeight(double height) => height >= MinHeightCm && height <= MaxHeightCm;

    private bool IsValidWeight(double weight) => weight >= MinWeightKg && weight <= MaxWeightKg;
}
