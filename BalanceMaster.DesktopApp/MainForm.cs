using BalanceMaster.Service.Commands;
using BalanceMaster.Service.Services.Implementations;

namespace BalanceMaster.DesktopApp;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        DebitButton.Click += DebitButtonClicked;
    }

    private void DebitButtonClicked(object? sender, EventArgs e)
    {
        var debitCommand = new DebitCommand
        {
            Currency = CurrencyInput.Text,
            Amount = decimal.Parse(AmountInput.Text),
            AccountId = int.Parse(AccountIdInput.Text)
        };

        debitCommand.Validate();

        var operationService = new OperationService(new InMemoryAccountRepository(), new InMemoryOperationRepository());
        operationService.ExecuteAsync(debitCommand);
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