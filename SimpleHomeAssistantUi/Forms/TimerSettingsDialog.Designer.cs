using System.ComponentModel;

namespace SimpleHomeAssistantUi.Forms;

partial class TimerSettingsDialog
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        _comboTimers = new ComboBox();
        label1 = new Label();
        _chckEnable = new CheckBox();
        _comboMode = new ComboBox();
        _numTimeHours = new NumericUpDown();
        label2 = new Label();
        _numTimeMinutes = new NumericUpDown();
        _chckListDays = new CheckedListBox();
        _chckRepeat = new CheckBox();
        _comboOutput = new ComboBox();
        _comboAction = new ComboBox();
        _btnSave = new Button();
        label3 = new Label();
        label4 = new Label();
        label5 = new Label();
        label7 = new Label();
        label8 = new Label();
        _numWindow = new NumericUpDown();
        label9 = new Label();
        ((ISupportInitialize)_numTimeHours).BeginInit();
        ((ISupportInitialize)_numTimeMinutes).BeginInit();
        ((ISupportInitialize)_numWindow).BeginInit();
        SuspendLayout();
        // 
        // _comboTimers
        // 
        _comboTimers.FormattingEnabled = true;
        _comboTimers.Location = new Point(101, 14);
        _comboTimers.Name = "_comboTimers";
        _comboTimers.Size = new Size(121, 23);
        _comboTimers.TabIndex = 0;
        _comboTimers.SelectedIndexChanged += _comboTimers_SelectedIndexChanged;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(53, 17);
        label1.Name = "label1";
        label1.Size = new Size(40, 15);
        label1.TabIndex = 1;
        label1.Text = "Timer:";
        // 
        // _chckEnable
        // 
        _chckEnable.AutoSize = true;
        _chckEnable.Location = new Point(101, 43);
        _chckEnable.Name = "_chckEnable";
        _chckEnable.Size = new Size(61, 19);
        _chckEnable.TabIndex = 2;
        _chckEnable.Text = "Enable";
        _chckEnable.UseVisualStyleBackColor = true;
        // 
        // _comboMode
        // 
        _comboMode.FormattingEnabled = true;
        _comboMode.Items.AddRange(new object[] { "0 - clock time", "1 - local sunrise time", "2 - local sunset time" });
        _comboMode.Location = new Point(101, 68);
        _comboMode.Name = "_comboMode";
        _comboMode.Size = new Size(121, 23);
        _comboMode.TabIndex = 3;
        // 
        // _numTimeHours
        // 
        _numTimeHours.Location = new Point(101, 97);
        _numTimeHours.Maximum = new decimal(new int[] { 23, 0, 0, 0 });
        _numTimeHours.Name = "_numTimeHours";
        _numTimeHours.Size = new Size(52, 23);
        _numTimeHours.TabIndex = 4;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
        label2.Location = new Point(158, 97);
        label2.Name = "label2";
        label2.Size = new Size(14, 21);
        label2.TabIndex = 5;
        label2.Text = ":";
        // 
        // _numTimeMinutes
        // 
        _numTimeMinutes.Location = new Point(176, 97);
        _numTimeMinutes.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
        _numTimeMinutes.Name = "_numTimeMinutes";
        _numTimeMinutes.Size = new Size(46, 23);
        _numTimeMinutes.TabIndex = 6;
        // 
        // _chckListDays
        // 
        _chckListDays.FormattingEnabled = true;
        _chckListDays.Items.AddRange(new object[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" });
        _chckListDays.Location = new Point(101, 155);
        _chckListDays.Name = "_chckListDays";
        _chckListDays.Size = new Size(121, 130);
        _chckListDays.TabIndex = 8;
        // 
        // _chckRepeat
        // 
        _chckRepeat.AutoSize = true;
        _chckRepeat.Location = new Point(101, 291);
        _chckRepeat.Name = "_chckRepeat";
        _chckRepeat.Size = new Size(62, 19);
        _chckRepeat.TabIndex = 9;
        _chckRepeat.Text = "Repeat";
        _chckRepeat.UseVisualStyleBackColor = true;
        // 
        // _comboOutput
        // 
        _comboOutput.FormattingEnabled = true;
        _comboOutput.Items.AddRange(new object[] { "1" });
        _comboOutput.Location = new Point(101, 316);
        _comboOutput.Name = "_comboOutput";
        _comboOutput.Size = new Size(121, 23);
        _comboOutput.TabIndex = 10;
        // 
        // _comboAction
        // 
        _comboAction.FormattingEnabled = true;
        _comboAction.Items.AddRange(new object[] { "0 - turn OFF", "1 - turn ON", "2 - Toggle", "3 - RUlE/BLINK" });
        _comboAction.Location = new Point(101, 345);
        _comboAction.Name = "_comboAction";
        _comboAction.Size = new Size(121, 23);
        _comboAction.TabIndex = 11;
        // 
        // _btnSave
        // 
        _btnSave.Location = new Point(64, 390);
        _btnSave.Name = "_btnSave";
        _btnSave.Size = new Size(108, 42);
        _btnSave.TabIndex = 12;
        _btnSave.Text = "Save";
        _btnSave.UseVisualStyleBackColor = true;
        _btnSave.Click += _btnSave_Click;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(44, 348);
        label3.Name = "label3";
        label3.Size = new Size(45, 15);
        label3.TabIndex = 13;
        label3.Text = "Action:";
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(41, 319);
        label4.Name = "label4";
        label4.Size = new Size(48, 15);
        label4.TabIndex = 14;
        label4.Text = "Output:";
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Location = new Point(54, 155);
        label5.Name = "label5";
        label5.Size = new Size(35, 15);
        label5.TabIndex = 15;
        label5.Text = "Days:";
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.Location = new Point(53, 102);
        label7.Name = "label7";
        label7.Size = new Size(36, 15);
        label7.TabIndex = 17;
        label7.Text = "Time:";
        // 
        // label8
        // 
        label8.AutoSize = true;
        label8.Location = new Point(53, 71);
        label8.Name = "label8";
        label8.Size = new Size(41, 15);
        label8.TabIndex = 18;
        label8.Text = "Mode:";
        // 
        // _numWindow
        // 
        _numWindow.Location = new Point(101, 127);
        _numWindow.Maximum = new decimal(new int[] { 15, 0, 0, 0 });
        _numWindow.Name = "_numWindow";
        _numWindow.Size = new Size(120, 23);
        _numWindow.TabIndex = 19;
        // 
        // label9
        // 
        label9.AutoSize = true;
        label9.Location = new Point(35, 129);
        label9.Name = "label9";
        label9.Size = new Size(54, 15);
        label9.TabIndex = 20;
        label9.Text = "Window:";
        // 
        // TimerSettingsDialog
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(238, 444);
        Controls.Add(label9);
        Controls.Add(_numWindow);
        Controls.Add(label8);
        Controls.Add(label7);
        Controls.Add(label5);
        Controls.Add(label4);
        Controls.Add(label3);
        Controls.Add(_btnSave);
        Controls.Add(_comboAction);
        Controls.Add(_comboOutput);
        Controls.Add(_chckRepeat);
        Controls.Add(_chckListDays);
        Controls.Add(_numTimeMinutes);
        Controls.Add(label2);
        Controls.Add(_numTimeHours);
        Controls.Add(_comboMode);
        Controls.Add(_chckEnable);
        Controls.Add(label1);
        Controls.Add(_comboTimers);
        Name = "TimerSettingsDialog";
        Text = "TimerSettingsDialog";
        ((ISupportInitialize)_numTimeHours).EndInit();
        ((ISupportInitialize)_numTimeMinutes).EndInit();
        ((ISupportInitialize)_numWindow).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private ComboBox _comboTimers;
    private Label label1;
    private CheckBox _chckEnable;
    private ComboBox _comboMode;
    private NumericUpDown _numTimeHours;
    private Label label2;
    private NumericUpDown _numTimeMinutes;
    private CheckedListBox _chckListDays;
    private CheckBox _chckRepeat;
    private ComboBox _comboAction;
    private ComboBox _comboOutput;
    private Button _btnSave;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label label7;
    private Label label8;
    private NumericUpDown _numWindow;
    private Label label9;
}