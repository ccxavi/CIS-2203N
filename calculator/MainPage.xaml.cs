using System.Globalization;

namespace calculator;

public partial class MainPage : ContentPage
{
    private string currentInput = "0";
    private double firstOperand = 0;
    private string currentOperator = "";
    private bool isNewInput = true;

    public MainPage()
    {
        InitializeComponent();
    }

    private void UpdateDisplay()
    {
        ResultLabel.Text = currentInput;
    }

    private void OnNumberClicked(object? sender, EventArgs e)
    {
        if (sender is Button button)
        {
            string number = button.Text;

            if (isNewInput)
            {
                currentInput = number;
                isNewInput = false;
            }
            else
            {
                if (currentInput == "0")
                    currentInput = number;
                else
                    currentInput += number;
            }

            UpdateDisplay();
        }
    }

    private void OnDecimalClicked(object? sender, EventArgs e)
    {
        if (isNewInput)
        {
            currentInput = "0.";
            isNewInput = false;
        }
        else if (!currentInput.Contains('.'))
        {
            currentInput += ".";
        }

        UpdateDisplay();
    }

    private void OnOperatorClicked(object? sender, EventArgs e)
    {
        if (sender is Button button)
        {
            if (!isNewInput && currentOperator != "")
            {
                Evaluate();
            }

            firstOperand = double.Parse(currentInput, CultureInfo.InvariantCulture);
            currentOperator = button.Text;
            isNewInput = true;
        }
    }

    private void OnEqualsClicked(object? sender, EventArgs e)
    {
        if (currentOperator != "")
        {
            Evaluate();
            currentOperator = "";
        }
    }

    private void Evaluate()
    {
        double secondOperand = double.Parse(currentInput, CultureInfo.InvariantCulture);
        double result = 0;

        switch (currentOperator)
        {
            case "+":
                result = firstOperand + secondOperand;
                break;
            case "-":
                result = firstOperand - secondOperand;
                break;
            case "x":
                result = firstOperand * secondOperand;
                break;
            case "/":
                if (secondOperand == 0)
                {
                    currentInput = "Error";
                    UpdateDisplay();
                    isNewInput = true;
                    currentOperator = "";
                    return;
                }
                result = firstOperand / secondOperand;
                break;
        }

        currentInput = result.ToString("G10", CultureInfo.InvariantCulture);
        firstOperand = result;
        isNewInput = true;
        UpdateDisplay();
    }

    private void OnClearClicked(object? sender, EventArgs e)
    {
        currentInput = "0";
        firstOperand = 0;
        currentOperator = "";
        isNewInput = true;
        UpdateDisplay();
    }

    private void OnDeleteClicked(object? sender, EventArgs e)
    {
        if (currentInput == "Error")
        {
            currentInput = "0";
        }
        else if (currentInput.Length > 1)
        {
            currentInput = currentInput.Substring(0, currentInput.Length - 1);
        }
        else
        {
            currentInput = "0";
        }

        UpdateDisplay();
    }
}
