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

        var operationService = new OperationService(new InMemoryAccountRepository());
        operationService.Execute(debitCommand);
    }
}