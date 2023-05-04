using System.ComponentModel;

namespace SimpleHomeAssistantUi.Forms;

partial class AccountOptionsDialog
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
        _lblUser = new Label();
        _lblPassword = new Label();
        _btnLogin = new Button();
        _btnAdd = new Button();
        _btnDelete = new Button();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(12, 7);
        label1.Name = "label1";
        label1.Size = new Size(33, 15);
        label1.TabIndex = 0;
        label1.Text = "User:";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(175, 9);
        label2.Name = "label2";
        label2.Size = new Size(60, 15);
        label2.TabIndex = 1;
        label2.Text = "Password:";
        // 
        // _lblUser
        // 
        _lblUser.AutoSize = true;
        _lblUser.Location = new Point(51, 7);
        _lblUser.Name = "_lblUser";
        _lblUser.Size = new Size(22, 15);
        _lblUser.TabIndex = 2;
        _lblUser.Text = "---";
        // 
        // _lblPassword
        // 
        _lblPassword.AutoSize = true;
        _lblPassword.Location = new Point(241, 7);
        _lblPassword.Name = "_lblPassword";
        _lblPassword.Size = new Size(22, 15);
        _lblPassword.TabIndex = 3;
        _lblPassword.Text = "---";
        // 
        // _btnLogin
        // 
        _btnLogin.Location = new Point(139, 35);
        _btnLogin.Name = "_btnLogin";
        _btnLogin.Size = new Size(75, 23);
        _btnLogin.TabIndex = 4;
        _btnLogin.Text = "Login";
        _btnLogin.UseVisualStyleBackColor = true;
        _btnLogin.Click += _btnLogin_Click;
        // 
        // _btnAdd
        // 
        _btnAdd.Location = new Point(139, 64);
        _btnAdd.Name = "_btnAdd";
        _btnAdd.Size = new Size(75, 23);
        _btnAdd.TabIndex = 5;
        _btnAdd.Text = "Add";
        _btnAdd.UseVisualStyleBackColor = true;
        _btnAdd.Click += _btnAdd_Click;
        // 
        // _btnDelete
        // 
        _btnDelete.Location = new Point(139, 93);
        _btnDelete.Name = "_btnDelete";
        _btnDelete.Size = new Size(75, 23);
        _btnDelete.TabIndex = 6;
        _btnDelete.Text = "Delete";
        _btnDelete.UseVisualStyleBackColor = true;
        _btnDelete.Click += _btnDelete_Click;
        // 
        // AccountOptionsDialog
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(351, 134);
        Controls.Add(_btnDelete);
        Controls.Add(_btnAdd);
        Controls.Add(_btnLogin);
        Controls.Add(_lblPassword);
        Controls.Add(_lblUser);
        Controls.Add(label2);
        Controls.Add(label1);
        Name = "AccountOptionsDialog";
        Text = "AccountOptionsDialog";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private Label label2;
    private Label _lblUser;
    private Label _lblPassword;
    private Button _btnLogin;
    private Button _btnAdd;
    private Button _btnDelete;
}