namespace StudentRecordSystem.Forms;

partial class LoginForm
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
        titleLabel = new Label();
        usernameLabel = new Label();
        usernameTextBox = new TextBox();
        passwordLabel = new Label();
        passwordTextBox = new TextBox();
        loginButton = new Button();
        errorLabel = new Label();
        SuspendLayout();
        // 
        // titleLabel
        // 
        titleLabel.AutoSize = true;
        titleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        titleLabel.Location = new Point(28, 20);
        titleLabel.Name = "titleLabel";
        titleLabel.Text = "Student Record System";
        // 
        // usernameLabel
        // 
        usernameLabel.AutoSize = true;
        usernameLabel.Location = new Point(30, 83);
        usernameLabel.Name = "usernameLabel";
        usernameLabel.Text = "Username";
        // 
        // usernameTextBox
        // 
        usernameTextBox.Location = new Point(130, 80);
        usernameTextBox.Name = "usernameTextBox";
        usernameTextBox.Size = new Size(220, 23);
        usernameTextBox.TabIndex = 0;
        // 
        // passwordLabel
        // 
        passwordLabel.AutoSize = true;
        passwordLabel.Location = new Point(30, 123);
        passwordLabel.Name = "passwordLabel";
        passwordLabel.Text = "Password";
        // 
        // passwordTextBox
        // 
        passwordTextBox.Location = new Point(130, 120);
        passwordTextBox.Name = "passwordTextBox";
        passwordTextBox.Size = new Size(220, 23);
        passwordTextBox.TabIndex = 1;
        passwordTextBox.UseSystemPasswordChar = true;
        // 
        // loginButton
        // 
        loginButton.Location = new Point(250, 160);
        loginButton.Name = "loginButton";
        loginButton.Size = new Size(100, 30);
        loginButton.TabIndex = 2;
        loginButton.Text = "Log In";
        loginButton.UseVisualStyleBackColor = true;
        loginButton.Click += LoginButton_Click;
        // 
        // errorLabel
        // 
        errorLabel.ForeColor = Color.Firebrick;
        errorLabel.Location = new Point(30, 205);
        errorLabel.Name = "errorLabel";
        errorLabel.Size = new Size(320, 40);
        // 
        // LoginForm
        // 
        AcceptButton = loginButton;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(384, 261);
        Controls.Add(titleLabel);
        Controls.Add(usernameLabel);
        Controls.Add(usernameTextBox);
        Controls.Add(passwordLabel);
        Controls.Add(passwordTextBox);
        Controls.Add(loginButton);
        Controls.Add(errorLabel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        Name = "LoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Student Record System - Log In";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label titleLabel;
    private Label usernameLabel;
    private TextBox usernameTextBox;
    private Label passwordLabel;
    private TextBox passwordTextBox;
    private Button loginButton;
    private Label errorLabel;
}
