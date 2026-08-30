namespace StudentRecordSystemPrototype;

partial class Form1
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
        this.components = new System.ComponentModel.Container();
        this.titleLabel = new System.Windows.Forms.Label();
        this.subtitleLabel = new System.Windows.Forms.Label();
        this.statusLabel = new System.Windows.Forms.Label();

        this.titleLabel.Text = "Student Record System";
        this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
        this.titleLabel.AutoSize = true;
        this.titleLabel.Location = new System.Drawing.Point(40, 40);

        this.subtitleLabel.Text = "CSP3341 Preliminary Report - Proposed Application (Prototype)";
        this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
        this.subtitleLabel.ForeColor = System.Drawing.Color.Gray;
        this.subtitleLabel.AutoSize = true;
        this.subtitleLabel.Location = new System.Drawing.Point(43, 90);

        this.statusLabel.Text =
            "This is an early project scaffold used to confirm the C# / .NET development\n" +
            "environment is correctly configured (see Part B, Section 3).\n\n" +
            "Planned functionality (login, student record view/search, and admin-only\n" +
            "add access) is described in the specification and process diagram in\n" +
            "Part B, Section 4, and will be implemented for the Technical Report\n" +
            "assessment.";
        this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.statusLabel.AutoSize = true;
        this.statusLabel.Location = new System.Drawing.Point(43, 140);

        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(620, 320);
        this.Text = "Student Record System - Prototype";
        this.Controls.Add(this.titleLabel);
        this.Controls.Add(this.subtitleLabel);
        this.Controls.Add(this.statusLabel);
    }

    private System.Windows.Forms.Label titleLabel;
    private System.Windows.Forms.Label subtitleLabel;
    private System.Windows.Forms.Label statusLabel;

    #endregion
}
