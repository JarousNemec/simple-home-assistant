using System.ComponentModel;

namespace SimpleHomeAssistantUi.Controls;

partial class PropertyControl
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        _lblName = new Label();
        _txtValue = new TextBox();
        _btnUpdate = new Button();
        SuspendLayout();
        // 
        // _lblName
        // 
        _lblName.AutoSize = true;
        _lblName.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
        _lblName.Location = new Point(3, 9);
        _lblName.Name = "_lblName";
        _lblName.Size = new Size(45, 17);
        _lblName.TabIndex = 0;
        _lblName.Text = "label1";
        // 
        // _txtValue
        // 
        _txtValue.Location = new Point(201, 7);
        _txtValue.Name = "_txtValue";
        _txtValue.Size = new Size(168, 23);
        _txtValue.TabIndex = 1;
        // 
        // _btnUpdate
        // 
        _btnUpdate.Location = new Point(375, 7);
        _btnUpdate.Name = "_btnUpdate";
        _btnUpdate.Size = new Size(75, 23);
        _btnUpdate.TabIndex = 2;
        _btnUpdate.Text = "Update";
        _btnUpdate.UseVisualStyleBackColor = true;
        _btnUpdate.Click += _btnUpdate_Click;
        // 
        // PropertyControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(_btnUpdate);
        Controls.Add(_txtValue);
        Controls.Add(_lblName);
        Name = "PropertyControl";
        Size = new Size(462, 36);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label _lblName;
    private TextBox _txtValue;
    private Button _btnUpdate;
}