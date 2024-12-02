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
        SuspendLayout();
        // 
        // DebitButton
        // 
        DebitButton.Location = new Point(312, 521);
        DebitButton.Name = "DebitButton";
        DebitButton.Size = new Size(474, 79);
        DebitButton.TabIndex = 0;
        DebitButton.Text = "Debit";
        DebitButton.UseVisualStyleBackColor = true;
        // 
        // AccountIdInput
        // 
        AccountIdInput.Location = new Point(312, 140);
        AccountIdInput.Name = "AccountIdInput";
        AccountIdInput.Size = new Size(480, 47);
        AccountIdInput.TabIndex = 1;
        // 
        // AmountInput
        // 
        AmountInput.Location = new Point(312, 248);
        AmountInput.Name = "AmountInput";
        AmountInput.Size = new Size(480, 47);
        AmountInput.TabIndex = 1;
        // 
        // CurrencyInput
        // 
        CurrencyInput.Location = new Point(312, 376);
        CurrencyInput.Name = "CurrencyInput";
        CurrencyInput.Size = new Size(480, 47);
        CurrencyInput.TabIndex = 1;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(315, 82);
        label1.Name = "label1";
        label1.Size = new Size(153, 41);
        label1.TabIndex = 2;
        label1.Text = "AccountId";
        // 
        // AmountLabel
        // 
        AmountLabel.AutoSize = true;
        AmountLabel.Location = new Point(315, 204);
        AmountLabel.Name = "AmountLabel";
        AmountLabel.Size = new Size(125, 41);
        AmountLabel.TabIndex = 2;
        AmountLabel.Text = "Amount";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(312, 318);
        label3.Name = "label3";
        label3.Size = new Size(136, 41);
        label3.TabIndex = 2;
        label3.Text = "Currency";
        // 
        // CreditButton
        // 
        CreditButton.Location = new Point(312, 642);
        CreditButton.Name = "CreditButton";
        CreditButton.Size = new Size(474, 79);
        CreditButton.TabIndex = 0;
        CreditButton.Text = "Credit";
        CreditButton.UseVisualStyleBackColor = true;
        CreditButton.Click += CreditButton_Click;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(17F, 41F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(2577, 1248);
        Controls.Add(label3);
        Controls.Add(AmountLabel);
        Controls.Add(label1);
        Controls.Add(CurrencyInput);
        Controls.Add(AmountInput);
        Controls.Add(AccountIdInput);
        Controls.Add(CreditButton);
        Controls.Add(DebitButton);
        Name = "MainForm";
        Text = "Main Form";
        ResumeLayout(false);
        PerformLayout();
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
}
