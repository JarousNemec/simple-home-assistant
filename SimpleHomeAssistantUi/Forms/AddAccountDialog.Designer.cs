using System.ComponentModel;

namespace SimpleHomeAssistantUi.Forms;

partial class AddAccountDialog
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
        _btnAdd = new Button();
        label1 = new Label();
        label2 = new Label();
        _txtUser = new TextBox();
        _txtPassword = new TextBox();
        SuspendLayout();
        // 
        // _btnAdd
        // 
        _btnAdd.Location = new Point(89, 101);
        _btnAdd.Name = "_btnAdd";
        _btnAdd.Size = new Size(75, 23);
        _btnAdd.TabIndex = 0;
        _btnAdd.Text = "Add user";
        _btnAdd.UseVisualStyleBackColor = true;
        _btnAdd.Click += _btnAdd_Click;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(39, 28);
        label1.Name = "label1";
        label1.Size = new Size(33, 15);
        label1.TabIndex = 1;
        label1.Text = "User:";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(12, 75);
        label2.Name = "label2";
        label2.Size = new Size(60, 15);
        label2.TabIndex = 2;
        label2.Text = "Password:";
        // 
        // _txtUser
        // 
        _txtUser.Location = new Point(78, 25);
        _txtUser.Name = "_txtUser";
        _txtUser.Size = new Size(100, 23);
        _txtUser.TabIndex = 3;
        // 
        // _txtPassword
        // 
        _txtPassword.Location = new Point(78, 72);
        _txtPassword.Name = "_txtPassword";
        _txtPassword.Size = new Size(100, 23);
        _txtPassword.TabIndex = 4;
        // 
        // AddAccountDialog
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(215, 156);
        Controls.Add(_txtPassword);
        Controls.Add(_txtUser);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(_btnAdd);
        Name = "AddAccountDialog";
        Text = "AddAccountDialog";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button _btnAdd;
    private Label label1;
    private Label label2;
    private TextBox _txtUser;
    private TextBox _txtPassword;
}