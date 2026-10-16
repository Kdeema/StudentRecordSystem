namespace StudentRecordSystem.Forms;

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
        DataGridViewCellStyle wamCellStyle = new DataGridViewCellStyle();
        welcomeLabel = new Label();
        signOutButton = new Button();
        searchLabel = new Label();
        searchTextBox = new TextBox();
        studentsGrid = new DataGridView();
        studentIdColumn = new DataGridViewTextBoxColumn();
        nameColumn = new DataGridViewTextBoxColumn();
        courseColumn = new DataGridViewTextBoxColumn();
        emailColumn = new DataGridViewTextBoxColumn();
        wamColumn = new DataGridViewTextBoxColumn();
        enrolmentsLabel = new Label();
        enrolmentsGrid = new DataGridView();
        unitCodeColumn = new DataGridViewTextBoxColumn();
        unitNameColumn = new DataGridViewTextBoxColumn();
        creditPointsColumn = new DataGridViewTextBoxColumn();
        markColumn = new DataGridViewTextBoxColumn();
        gradeColumn = new DataGridViewTextBoxColumn();
        adminPanel = new FlowLayoutPanel();
        addButton = new Button();
        editButton = new Button();
        deleteButton = new Button();
        statusStrip = new StatusStrip();
        statusLabel = new ToolStripStatusLabel();
        ((System.ComponentModel.ISupportInitialize)studentsGrid).BeginInit();
        ((System.ComponentModel.ISupportInitialize)enrolmentsGrid).BeginInit();
        adminPanel.SuspendLayout();
        statusStrip.SuspendLayout();
        SuspendLayout();
        // 
        // welcomeLabel
        // 
        welcomeLabel.AutoSize = true;
        welcomeLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        welcomeLabel.Location = new Point(12, 12);
        welcomeLabel.Name = "welcomeLabel";
        welcomeLabel.Text = "Welcome";
        // 
        // signOutButton
        // 
        signOutButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        signOutButton.Location = new Point(888, 9);
        signOutButton.Name = "signOutButton";
        signOutButton.Size = new Size(100, 28);
        signOutButton.TabIndex = 5;
        signOutButton.Text = "Sign Out";
        signOutButton.UseVisualStyleBackColor = true;
        signOutButton.Click += SignOutButton_Click;
        // 
        // searchLabel
        // 
        searchLabel.AutoSize = true;
        searchLabel.Location = new Point(12, 51);
        searchLabel.Name = "searchLabel";
        searchLabel.Text = "Search (ID, name or course):";
        // 
        // searchTextBox
        // 
        searchTextBox.Location = new Point(190, 48);
        searchTextBox.Name = "searchTextBox";
        searchTextBox.PlaceholderText = "e.g. 10648998, Kusal or BCS";
        searchTextBox.Size = new Size(300, 23);
        searchTextBox.TabIndex = 0;
        searchTextBox.TextChanged += SearchTextBox_TextChanged;
        // 
        // studentsGrid
        // 
        studentsGrid.AllowUserToAddRows = false;
        studentsGrid.AllowUserToDeleteRows = false;
        studentsGrid.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        studentsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        studentsGrid.Columns.AddRange(new DataGridViewColumn[] { studentIdColumn, nameColumn, courseColumn, emailColumn, wamColumn });
        studentsGrid.Location = new Point(12, 82);
        studentsGrid.MultiSelect = false;
        studentsGrid.Name = "studentsGrid";
        studentsGrid.ReadOnly = true;
        studentsGrid.RowHeadersVisible = false;
        studentsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        studentsGrid.Size = new Size(976, 260);
        studentsGrid.TabIndex = 1;
        studentsGrid.CellDoubleClick += StudentsGrid_CellDoubleClick;
        studentsGrid.SelectionChanged += StudentsGrid_SelectionChanged;
        // 
        // studentIdColumn
        // 
        studentIdColumn.DataPropertyName = "StudentId";
        studentIdColumn.HeaderText = "Student ID";
        studentIdColumn.Name = "studentIdColumn";
        studentIdColumn.ReadOnly = true;
        // 
        // nameColumn
        // 
        nameColumn.DataPropertyName = "FullName";
        nameColumn.HeaderText = "Name";
        nameColumn.Name = "nameColumn";
        nameColumn.ReadOnly = true;
        // 
        // courseColumn
        // 
        courseColumn.DataPropertyName = "CourseCode";
        courseColumn.HeaderText = "Course";
        courseColumn.Name = "courseColumn";
        courseColumn.ReadOnly = true;
        // 
        // emailColumn
        // 
        emailColumn.DataPropertyName = "Email";
        emailColumn.HeaderText = "Email";
        emailColumn.Name = "emailColumn";
        emailColumn.ReadOnly = true;
        // 
        // wamColumn
        // 
        wamColumn.DataPropertyName = "Wam";
        wamCellStyle.Format = "F2";
        wamCellStyle.NullValue = "N/A";
        wamColumn.DefaultCellStyle = wamCellStyle;
        wamColumn.HeaderText = "WAM";
        wamColumn.Name = "wamColumn";
        wamColumn.ReadOnly = true;
        // 
        // enrolmentsLabel
        // 
        enrolmentsLabel.AutoSize = true;
        enrolmentsLabel.Location = new Point(12, 352);
        enrolmentsLabel.Name = "enrolmentsLabel";
        enrolmentsLabel.Text = "Units for selected student:";
        // 
        // enrolmentsGrid
        // 
        enrolmentsGrid.AllowUserToAddRows = false;
        enrolmentsGrid.AllowUserToDeleteRows = false;
        enrolmentsGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        enrolmentsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        enrolmentsGrid.Columns.AddRange(new DataGridViewColumn[] { unitCodeColumn, unitNameColumn, creditPointsColumn, markColumn, gradeColumn });
        enrolmentsGrid.Location = new Point(12, 374);
        enrolmentsGrid.MultiSelect = false;
        enrolmentsGrid.Name = "enrolmentsGrid";
        enrolmentsGrid.ReadOnly = true;
        enrolmentsGrid.RowHeadersVisible = false;
        enrolmentsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        enrolmentsGrid.Size = new Size(976, 170);
        enrolmentsGrid.TabIndex = 2;
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
        creditPointsColumn.HeaderText = "Credit Points";
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
        // adminPanel
        // 
        adminPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        adminPanel.Controls.Add(addButton);
        adminPanel.Controls.Add(editButton);
        adminPanel.Controls.Add(deleteButton);
        adminPanel.Location = new Point(9, 552);
        adminPanel.Name = "adminPanel";
        adminPanel.Size = new Size(500, 38);
        adminPanel.TabIndex = 3;
        // 
        // addButton
        // 
        addButton.Name = "addButton";
        addButton.Size = new Size(120, 30);
        addButton.TabIndex = 0;
        addButton.Text = "Add Student";
        addButton.UseVisualStyleBackColor = true;
        addButton.Click += AddButton_Click;
        // 
        // editButton
        // 
        editButton.Name = "editButton";
        editButton.Size = new Size(120, 30);
        editButton.TabIndex = 1;
        editButton.Text = "Edit Student";
        editButton.UseVisualStyleBackColor = true;
        editButton.Click += EditButton_Click;
        // 
        // deleteButton
        // 
        deleteButton.Name = "deleteButton";
        deleteButton.Size = new Size(120, 30);
        deleteButton.TabIndex = 2;
        deleteButton.Text = "Delete Student";
        deleteButton.UseVisualStyleBackColor = true;
        deleteButton.Click += DeleteButton_Click;
        // 
        // statusStrip
        // 
        statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
        statusStrip.Name = "statusStrip";
        statusStrip.TabIndex = 6;
        // 
        // statusLabel
        // 
        statusLabel.Name = "statusLabel";
        statusLabel.Text = "Ready";
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1000, 620);
        Controls.Add(welcomeLabel);
        Controls.Add(signOutButton);
        Controls.Add(searchLabel);
        Controls.Add(searchTextBox);
        Controls.Add(studentsGrid);
        Controls.Add(enrolmentsLabel);
        Controls.Add(enrolmentsGrid);
        Controls.Add(adminPanel);
        Controls.Add(statusStrip);
        MinimumSize = new Size(800, 560);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Student Record System";
        Load += MainForm_Load;
        ((System.ComponentModel.ISupportInitialize)studentsGrid).EndInit();
        ((System.ComponentModel.ISupportInitialize)enrolmentsGrid).EndInit();
        adminPanel.ResumeLayout(false);
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label welcomeLabel;
    private Button signOutButton;
    private Label searchLabel;
    private TextBox searchTextBox;
    private DataGridView studentsGrid;
    private DataGridViewTextBoxColumn studentIdColumn;
    private DataGridViewTextBoxColumn nameColumn;
    private DataGridViewTextBoxColumn courseColumn;
    private DataGridViewTextBoxColumn emailColumn;
    private DataGridViewTextBoxColumn wamColumn;
    private Label enrolmentsLabel;
    private DataGridView enrolmentsGrid;
    private DataGridViewTextBoxColumn unitCodeColumn;
    private DataGridViewTextBoxColumn unitNameColumn;
    private DataGridViewTextBoxColumn creditPointsColumn;
    private DataGridViewTextBoxColumn markColumn;
    private DataGridViewTextBoxColumn gradeColumn;
    private FlowLayoutPanel adminPanel;
    private Button addButton;
    private Button editButton;
    private Button deleteButton;
    private StatusStrip statusStrip;
    private ToolStripStatusLabel statusLabel;
}
