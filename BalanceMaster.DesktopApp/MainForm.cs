using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace BalanceMaster.DesktopApp;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    private async void OnDebitButtonClick(object? sender, EventArgs e)
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

    private void OnSearchCustomerButtonClick(object sender, EventArgs e)
    {
    }
}