using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace BalanceMaster.DesktopApp;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        DebitButton.Click += DebitButtonClicked;
    }

    private async void DebitButtonClicked(object? sender, EventArgs e)
    {
        // Post request
        var debitCommand = new
        {
            Currency = CurrencyInput.Text,
            Amount = decimal.Parse(AmountInput.Text),
            AccountId = int.Parse(AccountIdInput.Text)
        };

        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7017/api/");
        var content = new StringContent(
            JsonSerializer.Serialize(debitCommand),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);

        var response = await client.PostAsync("operations/debit", content);

        // handle response
        if (response.IsSuccessStatusCode)
        {
            MessageBox.Show("Debit operation was successfull!");
            return;
        }

        var responseString = await response.Content.ReadAsStringAsync();
        var apiError = JsonSerializer.Deserialize<ApiError>(responseString);
        if (apiError?.ErrorCode == "ValidationException")
        {
            MessageBox.Show(apiError.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
            File.AppendAllText("logs.txt", apiError?.Detail);
        }
    }

    private async void CreditButton_Click(object sender, EventArgs e)
    {
        var result = await LongRunningTask();
        MessageBox.Show(result.ToString());
    }

    private static async Task<long> LongRunningTask()
    {
        await Task.Delay(5000);

        var resultTask = Task.Run(() => Sum(1_000_000_000));

        var result = await resultTask;
        return result;
    }

    private static long Sum(long numbers)
    {
        long result = 0;

        for (long i = 0; i < numbers; i++)
        {
            result += i;
        }

        return result;
    }
}