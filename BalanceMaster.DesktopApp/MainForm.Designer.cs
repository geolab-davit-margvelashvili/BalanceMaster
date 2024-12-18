namespace BalanceMaster.DesktopApp;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        DebitButton = new Button();
        AccountIdInput = new TextBox();
        AmountInput = new TextBox();
        CurrencyInput = new TextBox();
        label1 = new Label();
        AmountLabel = new Label();
        label3 = new Label();
        CreditButton = new Button();
        PageContainer = new TabControl();
        CustomersPage = new TabPage();
        AccountsPage = new TabPage();
        OperationsPage = new TabPage();
        CustomerGridView = new DataGridView();
        PrivateNumberInput = new TextBox();
        PrivateNumberLabel = new Label();
        SearchCustomerButton = new Button();
        PageContainer.SuspendLayout();
        CustomersPage.SuspendLayout();
        AccountsPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)CustomerGridView).BeginInit();
        SuspendLayout();
        // 
        // DebitButton
        // 
        DebitButton.Location = new Point(851, 320);
        DebitButton.Margin = new Padding(1, 1, 1, 1);
        DebitButton.Name = "DebitButton";
        DebitButton.Size = new Size(195, 29);
        DebitButton.TabIndex = 0;
        DebitButton.Text = "Debit";
        DebitButton.UseVisualStyleBackColor = true;
        DebitButton.Click += OnDebitButtonClick;
        // 
        // AccountIdInput
        // 
        AccountIdInput.Location = new Point(851, 180);
        AccountIdInput.Margin = new Padding(1, 1, 1, 1);
        AccountIdInput.Name = "AccountIdInput";
        AccountIdInput.Size = new Size(200, 23);
        AccountIdInput.TabIndex = 1;
        // 
        // AmountInput
        // 
        AmountInput.Location = new Point(851, 220);
        AmountInput.Margin = new Padding(1, 1, 1, 1);
        AmountInput.Name = "AmountInput";
        AmountInput.Size = new Size(200, 23);
        AmountInput.TabIndex = 1;
        // 
        // CurrencyInput
        // 
        CurrencyInput.Location = new Point(851, 267);
        CurrencyInput.Margin = new Padding(1, 1, 1, 1);
        CurrencyInput.Name = "CurrencyInput";
        CurrencyInput.Size = new Size(200, 23);
        CurrencyInput.TabIndex = 1;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(853, 159);
        label1.Margin = new Padding(1, 0, 1, 0);
        label1.Name = "label1";
        label1.Size = new Size(62, 15);
        label1.TabIndex = 2;
        label1.Text = "AccountId";
        // 
        // AmountLabel
        // 
        AmountLabel.AutoSize = true;
        AmountLabel.Location = new Point(853, 204);
        AmountLabel.Margin = new Padding(1, 0, 1, 0);
        AmountLabel.Name = "AmountLabel";
        AmountLabel.Size = new Size(51, 15);
        AmountLabel.TabIndex = 2;
        AmountLabel.Text = "Amount";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(851, 245);
        label3.Margin = new Padding(1, 0, 1, 0);
        label3.Name = "label3";
        label3.Size = new Size(55, 15);
        label3.TabIndex = 2;
        label3.Text = "Currency";
        // 
        // CreditButton
        // 
        CreditButton.Location = new Point(851, 364);
        CreditButton.Margin = new Padding(1, 1, 1, 1);
        CreditButton.Name = "CreditButton";
        CreditButton.Size = new Size(195, 29);
        CreditButton.TabIndex = 0;
        CreditButton.Text = "Credit";
        CreditButton.UseVisualStyleBackColor = true;
        
        // 
        // PageContainer
        // 
        PageContainer.Controls.Add(CustomersPage);
        PageContainer.Controls.Add(AccountsPage);
        PageContainer.Controls.Add(OperationsPage);
        PageContainer.Dock = DockStyle.Fill;
        PageContainer.Location = new Point(0, 0);
        PageContainer.Name = "PageContainer";
        PageContainer.SelectedIndex = 0;
        PageContainer.Size = new Size(1209, 663);
        PageContainer.TabIndex = 3;
        // 
        // CustomersPage
        // 
        CustomersPage.Controls.Add(SearchCustomerButton);
        CustomersPage.Controls.Add(PrivateNumberLabel);
        CustomersPage.Controls.Add(PrivateNumberInput);
        CustomersPage.Controls.Add(CustomerGridView);
        CustomersPage.Location = new Point(4, 24);
        CustomersPage.Name = "CustomersPage";
        CustomersPage.Padding = new Padding(3);
        CustomersPage.Size = new Size(1201, 635);
        CustomersPage.TabIndex = 0;
        CustomersPage.Text = "Customers";
        CustomersPage.UseVisualStyleBackColor = true;
        // 
        // AccountsPage
        // 
        AccountsPage.Controls.Add(DebitButton);
        AccountsPage.Controls.Add(label3);
        AccountsPage.Controls.Add(CreditButton);
        AccountsPage.Controls.Add(AmountLabel);
        AccountsPage.Controls.Add(AccountIdInput);
        AccountsPage.Controls.Add(label1);
        AccountsPage.Controls.Add(AmountInput);
        AccountsPage.Controls.Add(CurrencyInput);
        AccountsPage.Location = new Point(4, 24);
        AccountsPage.Name = "AccountsPage";
        AccountsPage.Padding = new Padding(3);
        AccountsPage.Size = new Size(1201, 635);
        AccountsPage.TabIndex = 1;
        AccountsPage.Text = "Accounts";
        AccountsPage.UseVisualStyleBackColor = true;
        // 
        // OperationsPage
        // 
        OperationsPage.Location = new Point(4, 24);
        OperationsPage.Name = "OperationsPage";
        OperationsPage.Size = new Size(762, 514);
        OperationsPage.TabIndex = 2;
        OperationsPage.Text = "Operations";
        OperationsPage.UseVisualStyleBackColor = true;
        // 
        // CustomerGridView
        // 
        CustomerGridView.BackgroundColor = SystemColors.Control;
        CustomerGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        CustomerGridView.Dock = DockStyle.Bottom;
        CustomerGridView.Location = new Point(3, 94);
        CustomerGridView.Name = "CustomerGridView";
        CustomerGridView.Size = new Size(1195, 538);
        CustomerGridView.TabIndex = 0;
        // 
        // PrivateNumberInput
        // 
        PrivateNumberInput.Location = new Point(25, 41);
        PrivateNumberInput.Name = "PrivateNumberInput";
        PrivateNumberInput.Size = new Size(296, 23);
        PrivateNumberInput.TabIndex = 1;
        // 
        // PrivateNumberLabel
        // 
        PrivateNumberLabel.AutoSize = true;
        PrivateNumberLabel.Location = new Point(25, 19);
        PrivateNumberLabel.Name = "PrivateNumberLabel";
        PrivateNumberLabel.Size = new Size(90, 15);
        PrivateNumberLabel.TabIndex = 2;
        PrivateNumberLabel.Text = "Private Number";
        // 
        // SearchCustomerButton
        // 
        SearchCustomerButton.Location = new Point(342, 39);
        SearchCustomerButton.Name = "SearchCustomerButton";
        SearchCustomerButton.Size = new Size(79, 27);
        SearchCustomerButton.TabIndex = 3;
        SearchCustomerButton.Text = "Search";
        SearchCustomerButton.UseVisualStyleBackColor = true;
        SearchCustomerButton.Click += OnSearchCustomerButtonClick;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1209, 663);
        Controls.Add(PageContainer);
        Margin = new Padding(1, 1, 1, 1);
        Name = "MainForm";
        Text = "Main";
        PageContainer.ResumeLayout(false);
        CustomersPage.ResumeLayout(false);
        CustomersPage.PerformLayout();
        AccountsPage.ResumeLayout(false);
        AccountsPage.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)CustomerGridView).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Button DebitButton;
    private TextBox AccountIdInput;
    private TextBox AmountInput;
    private TextBox CurrencyInput;
    private Label label1;
    private Label AmountLabel;
    private Label label3;
    private Button CreditButton;
    private TabControl PageContainer;
    private TabPage CustomersPage;
    private TabPage AccountsPage;
    private TabPage OperationsPage;
    private Button SearchCustomerButton;
    private Label PrivateNumberLabel;
    private TextBox PrivateNumberInput;
    private DataGridView CustomerGridView;
}
