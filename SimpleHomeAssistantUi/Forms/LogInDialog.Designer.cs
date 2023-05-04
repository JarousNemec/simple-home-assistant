using System.ComponentModel;

namespace SimpleHomeAssistantUi.Forms;

partial class LogInDialog
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
        label1 = new Label();
        label2 = new Label();
        _txtUser = new TextBox();
        _txtPassword = new TextBox();
        _btnLogIn = new Button();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(54, 36);
        label1.Name = "label1";
        label1.Size = new Size(33, 15);
        label1.TabIndex = 0;
        label1.Text = "User:";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(27, 88);
        label2.Name = "label2";
        label2.Size = new Size(60, 15);
        label2.TabIndex = 1;
        label2.Text = "Password:";
        // 
        // _txtUser
        // 
        _txtUser.Location = new Point(94, 33);
        _txtUser.Name = "_txtUser";
        _txtUser.Size = new Size(100, 23);
        _txtUser.TabIndex = 2;
        // 
        // _txtPassword
        // 
        _txtPassword.Location = new Point(94, 85);
        _txtPassword.Name = "_txtPassword";
        _txtPassword.Size = new Size(100, 23);
        _txtPassword.TabIndex = 3;
        // 
        // _btnLogIn
        // 
        _btnLogIn.Location = new Point(105, 124);
        _btnLogIn.Name = "_btnLogIn";
        _btnLogIn.Size = new Size(75, 23);
        _btnLogIn.TabIndex = 4;
        _btnLogIn.Text = "Log in";
        _btnLogIn.UseVisualStyleBackColor = true;
        _btnLogIn.Click += _btnLogIn_Click;
        // 
        // LogInDialog
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(250, 180);
        Controls.Add(_btnLogIn);
        Controls.Add(_txtPassword);
        Controls.Add(_txtUser);
        Controls.Add(label2);
        Controls.Add(label1);
        Name = "LogInDialog";
        Text = "LogInDialog";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private Label label2;
    private TextBox _txtUser;
    private TextBox _txtPassword;
    private Button _btnLogIn;
}