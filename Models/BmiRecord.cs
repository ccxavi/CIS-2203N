namespace bmi_calculator.Models;

public class BmiRecord
{
    public string BmiValue { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public double HeightCm { get; init; }
    public double WeightKg { get; init; }
    public DateTime RecordedAt { get; init; } = DateTime.Now;

    /// <summary>Formatted timestamp, e.g. "Feb 25 · 11:17 PM"</summary>
    public string Timestamp =>
        RecordedAt.ToString("MMM d · h:mm tt");

    /// <summary>Compact measurement summary, e.g. "170 cm · 70 kg"</summary>
    public string Measurements =>
        $"{HeightCm:F0} cm  ·  {WeightKg:F1} kg";
}
