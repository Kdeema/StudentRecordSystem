namespace StudentRecordSystem.Forms;

partial class StudentEditForm
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
        studentIdLabel = new Label();
        studentIdTextBox = new TextBox();
        firstNameLabel = new Label();
        firstNameTextBox = new TextBox();
        lastNameLabel = new Label();
        lastNameTextBox = new TextBox();
        emailLabel = new Label();
        emailTextBox = new TextBox();
        courseLabel = new Label();
        courseTextBox = new TextBox();
        enrolmentsGroupBox = new GroupBox();
        unitComboBox = new ComboBox();
        markLabel = new Label();
        markTextBox = new TextBox();
        addUnitButton = new Button();
        enrolmentsGrid = new DataGridView();
        unitCodeColumn = new DataGridViewTextBoxColumn();
        unitNameColumn = new DataGridViewTextBoxColumn();
        creditPointsColumn = new DataGridViewTextBoxColumn();
        markColumn = new DataGridViewTextBoxColumn();
        gradeColumn = new DataGridViewTextBoxColumn();
        removeUnitButton = new Button();
        saveButton = new Button();
        cancelButton = new Button();
        enrolmentsGroupBox.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)enrolmentsGrid).BeginInit();
        SuspendLayout();
        // 
        // studentIdLabel
        // 
        studentIdLabel.AutoSize = true;
        studentIdLabel.Location = new Point(20, 23);
        studentIdLabel.Name = "studentIdLabel";
        studentIdLabel.Text = "Student ID";
        // 
        // studentIdTextBox
        // 
        studentIdTextBox.Location = new Point(130, 20);
        studentIdTextBox.MaxLength = 8;
        studentIdTextBox.Name = "studentIdTextBox";
        studentIdTextBox.PlaceholderText = "8 digits";
        studentIdTextBox.Size = new Size(250, 23);
        studentIdTextBox.TabIndex = 0;
        // 
        // firstNameLabel
        // 
        firstNameLabel.AutoSize = true;
        firstNameLabel.Location = new Point(20, 58);
        firstNameLabel.Name = "firstNameLabel";
        firstNameLabel.Text = "First name";
        // 
        // firstNameTextBox
        // 
        firstNameTextBox.Location = new Point(130, 55);
        firstNameTextBox.Name = "firstNameTextBox";
        firstNameTextBox.Size = new Size(250, 23);
        firstNameTextBox.TabIndex = 1;
        // 
        // lastNameLabel
        // 
        lastNameLabel.AutoSize = true;
        lastNameLabel.Location = new Point(20, 93);
        lastNameLabel.Name = "lastNameLabel";
        lastNameLabel.Text = "Last name";
        // 
        // lastNameTextBox
        // 
        lastNameTextBox.Location = new Point(130, 90);
        lastNameTextBox.Name = "lastNameTextBox";
        lastNameTextBox.Size = new Size(250, 23);
        lastNameTextBox.TabIndex = 2;
        // 
        // emailLabel
        // 
        emailLabel.AutoSize = true;
        emailLabel.Location = new Point(20, 128);
        emailLabel.Name = "emailLabel";
        emailLabel.Text = "Email";
        // 
        // emailTextBox
        // 
        emailTextBox.Location = new Point(130, 125);
        emailTextBox.Name = "emailTextBox";
        emailTextBox.Size = new Size(250, 23);
        emailTextBox.TabIndex = 3;
        // 
        // courseLabel
        // 
        courseLabel.AutoSize = true;
        courseLabel.Location = new Point(20, 163);
        courseLabel.Name = "courseLabel";
        courseLabel.Text = "Course code";
        // 
        // courseTextBox
        // 
        courseTextBox.CharacterCasing = CharacterCasing.Upper;
        courseTextBox.Location = new Point(130, 160);
        courseTextBox.Name = "courseTextBox";
        courseTextBox.PlaceholderText = "e.g. BCS";
        courseTextBox.Size = new Size(250, 23);
        courseTextBox.TabIndex = 4;
        // 
        // enrolmentsGroupBox
        // 
        enrolmentsGroupBox.Controls.Add(unitComboBox);
        enrolmentsGroupBox.Controls.Add(markLabel);
        enrolmentsGroupBox.Controls.Add(markTextBox);
        enrolmentsGroupBox.Controls.Add(addUnitButton);
        enrolmentsGroupBox.Controls.Add(enrolmentsGrid);
        enrolmentsGroupBox.Controls.Add(removeUnitButton);
        enrolmentsGroupBox.Location = new Point(20, 200);
        enrolmentsGroupBox.Name = "enrolmentsGroupBox";
        enrolmentsGroupBox.Size = new Size(580, 255);
        enrolmentsGroupBox.TabIndex = 5;
        enrolmentsGroupBox.TabStop = false;
        enrolmentsGroupBox.Text = "Unit enrolments (leave mark blank if the result is not released)";
        // 
        // unitComboBox
        // 
        unitComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        unitComboBox.Location = new Point(12, 26);
        unitComboBox.Name = "unitComboBox";
        unitComboBox.Size = new Size(300, 23);
        unitComboBox.TabIndex = 0;
        // 
        // markLabel
        // 
        markLabel.AutoSize = true;
        markLabel.Location = new Point(322, 29);
        markLabel.Name = "markLabel";
        markLabel.Text = "Mark";
        // 
        // markTextBox
        // 
        markTextBox.Location = new Point(362, 26);
        markTextBox.MaxLength = 3;
        markTextBox.Name = "markTextBox";
        markTextBox.Size = new Size(60, 23);
        markTextBox.TabIndex = 1;
        // 
        // addUnitButton
        // 
        addUnitButton.Location = new Point(432, 24);
        addUnitButton.Name = "addUnitButton";
        addUnitButton.Size = new Size(136, 27);
        addUnitButton.TabIndex = 2;
        addUnitButton.Text = "Add / Update Unit";
        addUnitButton.UseVisualStyleBackColor = true;
        addUnitButton.Click += AddUnitButton_Click;
        // 
        // enrolmentsGrid
        // 
        enrolmentsGrid.AllowUserToAddRows = false;
        enrolmentsGrid.AllowUserToDeleteRows = false;
        enrolmentsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        enrolmentsGrid.Columns.AddRange(new DataGridViewColumn[] { unitCodeColumn, unitNameColumn, creditPointsColumn, markColumn, gradeColumn });
        enrolmentsGrid.Location = new Point(12, 60);
        enrolmentsGrid.MultiSelect = false;
        enrolmentsGrid.Name = "enrolmentsGrid";
        enrolmentsGrid.ReadOnly = true;
        enrolmentsGrid.RowHeadersVisible = false;
        enrolmentsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        enrolmentsGrid.Size = new Size(556, 148);
        enrolmentsGrid.TabIndex = 3;
        // 
        // unitCodeColumn
        // 
        unitCodeColumn.DataPropertyName = "UnitCode";
        unitCodeColumn.HeaderText = "Unit Code";
        unitCodeColumn.Name = "unitCodeColumn";
        unitCodeColumn.ReadOnly = true;
        // 
        // unitNameColumn
        // 
        unitNameColumn.DataPropertyName = "UnitName";
        unitNameColumn.FillWeight = 250F;
        unitNameColumn.HeaderText = "Unit Name";
        unitNameColumn.Name = "unitNameColumn";
        unitNameColumn.ReadOnly = true;
        // 
        // creditPointsColumn
        // 
        creditPointsColumn.DataPropertyName = "CreditPoints";
        creditPointsColumn.HeaderText = "Credits";
        creditPointsColumn.Name = "creditPointsColumn";
        creditPointsColumn.ReadOnly = true;
        // 
        // markColumn
        // 
        markColumn.DataPropertyName = "Mark";
        markColumn.HeaderText = "Mark";
        markColumn.Name = "markColumn";
        markColumn.ReadOnly = true;
        // 
        // gradeColumn
        // 
        gradeColumn.DataPropertyName = "Grade";
        gradeColumn.HeaderText = "Grade";
        gradeColumn.Name = "gradeColumn";
        gradeColumn.ReadOnly = true;
        // 
        // removeUnitButton
        // 
        removeUnitButton.Location = new Point(12, 216);
        removeUnitButton.Name = "removeUnitButton";
        removeUnitButton.Size = new Size(160, 27);
        removeUnitButton.TabIndex = 4;
        removeUnitButton.Text = "Remove Selected Unit";
        removeUnitButton.UseVisualStyleBackColor = true;
        removeUnitButton.Click += RemoveUnitButton_Click;
        // 
        // saveButton
        // 
        saveButton.Location = new Point(410, 468);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(92, 30);
        saveButton.TabIndex = 6;
        saveButton.Text = "Save";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += SaveButton_Click;
        // 
        // cancelButton
        // 
        cancelButton.DialogResult = DialogResult.Cancel;
        cancelButton.Location = new Point(508, 468);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(92, 30);
        cancelButton.TabIndex = 7;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        // 
        // StudentEditForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = cancelButton;
        ClientSize = new Size(620, 512);
        Controls.Add(studentIdLabel);
        Controls.Add(studentIdTextBox);
        Controls.Add(firstNameLabel);
        Controls.Add(firstNameTextBox);
        Controls.Add(lastNameLabel);
        Controls.Add(lastNameTextBox);
        Controls.Add(emailLabel);
        Controls.Add(emailTextBox);
        Controls.Add(courseLabel);
        Controls.Add(courseTextBox);
        Controls.Add(enrolmentsGroupBox);
        Controls.Add(saveButton);
        Controls.Add(cancelButton);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "StudentEditForm";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Student";
        enrolmentsGroupBox.ResumeLayout(false);
        enrolmentsGroupBox.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)enrolmentsGrid).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label studentIdLabel;
    private TextBox studentIdTextBox;
    private Label firstNameLabel;
    private TextBox firstNameTextBox;
    private Label lastNameLabel;
    private TextBox lastNameTextBox;
    private Label emailLabel;
    private TextBox emailTextBox;
    private Label courseLabel;
    private TextBox courseTextBox;
    private GroupBox enrolmentsGroupBox;
    private ComboBox unitComboBox;
    private Label markLabel;
    private TextBox markTextBox;
    private Button addUnitButton;
    private DataGridView enrolmentsGrid;
    private DataGridViewTextBoxColumn unitCodeColumn;
    private DataGridViewTextBoxColumn unitNameColumn;
    private DataGridViewTextBoxColumn creditPointsColumn;
    private DataGridViewTextBoxColumn markColumn;
    private DataGridViewTextBoxColumn gradeColumn;
    private Button removeUnitButton;
    private Button saveButton;
    private Button cancelButton;
}
