namespace WinformsLineGenerator;

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
            this.listView1 = new System.Windows.Forms.ListView();
            this.Id = new System.Windows.Forms.ColumnHeader();
            this.Line = new System.Windows.Forms.ColumnHeader();
            this.startButton = new System.Windows.Forms.Button();
            this.inputLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.mdbCheckBox = new System.Windows.Forms.CheckBox();
            this.sqlsCheckBox = new System.Windows.Forms.CheckBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Id,
            this.Line});
            this.listView1.Location = new System.Drawing.Point(62, 157);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(663, 321);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Id
            // 
            this.Id.Text = "Thread Id";
            this.Id.Width = 100;
            // 
            // Line
            // 
            this.Line.Text = "Line";
            this.Line.Width = 450;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(613, 94);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(112, 34);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // inputLabel
            // 
            this.inputLabel.AutoSize = true;
            this.inputLabel.Location = new System.Drawing.Point(62, 69);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(163, 25);
            this.inputLabel.TabIndex = 2;
            this.inputLabel.Text = "Number of threads";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(62, 97);
            this.textBox1.MaxLength = 2;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(163, 31);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "2";
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // mdbCheckBox
            // 
            this.mdbCheckBox.AutoSize = true;
            this.mdbCheckBox.Checked = true;
            this.mdbCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mdbCheckBox.Location = new System.Drawing.Point(62, 499);
            this.mdbCheckBox.Name = "mdbCheckBox";
            this.mdbCheckBox.Size = new System.Drawing.Size(140, 29);
            this.mdbCheckBox.TabIndex = 4;
            this.mdbCheckBox.Text = "Save to mdb";
            this.mdbCheckBox.UseVisualStyleBackColor = true;
            // 
            // sqlsCheckBox
            // 
            this.sqlsCheckBox.AutoSize = true;
            this.sqlsCheckBox.Location = new System.Drawing.Point(219, 499);
            this.sqlsCheckBox.Name = "sqlsCheckBox";
            this.sqlsCheckBox.Size = new System.Drawing.Size(188, 29);
            this.sqlsCheckBox.TabIndex = 5;
            this.sqlsCheckBox.Text = "Save to SQL Server";
            this.sqlsCheckBox.UseVisualStyleBackColor = true;
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(613, 494);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(112, 34);
            this.clearButton.TabIndex = 6;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 589);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.sqlsCheckBox);
            this.Controls.Add(this.mdbCheckBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.inputLabel);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.listView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private ListView listView1;
    private Button startButton;
    private Label inputLabel;
    private TextBox textBox1;
    private ColumnHeader Id;
    private ColumnHeader Line;
    private CheckBox mdbCheckBox;
    private CheckBox sqlsCheckBox;
    private Button clearButton;
}
