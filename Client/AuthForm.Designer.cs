
namespace Client
{
    partial class AuthForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthForm));
            this.LogPanel = new System.Windows.Forms.Panel();
            this.LogNicknameInput = new System.Windows.Forms.TextBox();
            this.LogBackButton = new System.Windows.Forms.Button();
            this.LogPasswordInput = new System.Windows.Forms.TextBox();
            this.LoginButton = new System.Windows.Forms.Button();
            this.LogPasswordLabel = new System.Windows.Forms.Label();
            this.LogNicknameLabel = new System.Windows.Forms.Label();
            this.Logo = new System.Windows.Forms.Label();
            this.RegPanel = new System.Windows.Forms.Panel();
            this.RegRepPasswordInput = new System.Windows.Forms.TextBox();
            this.RegPasswordInput = new System.Windows.Forms.TextBox();
            this.RegNicknameInput = new System.Windows.Forms.TextBox();
            this.RegRepPasswordLabel = new System.Windows.Forms.Label();
            this.RegBackButton = new System.Windows.Forms.Button();
            this.RegisterButton = new System.Windows.Forms.Button();
            this.RegPasswordLabel = new System.Windows.Forms.Label();
            this.RegNicknameLabel = new System.Windows.Forms.Label();
            this.AuthPanel = new System.Windows.Forms.Panel();
            this.GuestLogButton = new System.Windows.Forms.Button();
            this.AuthLogButton = new System.Windows.Forms.Button();
            this.AuthRegButton = new System.Windows.Forms.Button();
            this.Loading = new System.Windows.Forms.Label();
            this.CheckingForConnectionTimer = new System.Windows.Forms.Timer(this.components);
            this.LogPanel.SuspendLayout();
            this.RegPanel.SuspendLayout();
            this.AuthPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogPanel
            // 
            this.LogPanel.Controls.Add(this.LogNicknameInput);
            this.LogPanel.Controls.Add(this.LogBackButton);
            this.LogPanel.Controls.Add(this.LogPasswordInput);
            this.LogPanel.Controls.Add(this.LoginButton);
            this.LogPanel.Controls.Add(this.LogPasswordLabel);
            this.LogPanel.Controls.Add(this.LogNicknameLabel);
            this.LogPanel.Location = new System.Drawing.Point(11, 12);
            this.LogPanel.Name = "LogPanel";
            this.LogPanel.Size = new System.Drawing.Size(458, 429);
            this.LogPanel.TabIndex = 11;
            // 
            // LogNicknameInput
            // 
            this.LogNicknameInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(225)))));
            this.LogNicknameInput.Font = new System.Drawing.Font("Comic Sans MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LogNicknameInput.Location = new System.Drawing.Point(208, 152);
            this.LogNicknameInput.Name = "LogNicknameInput";
            this.LogNicknameInput.Size = new System.Drawing.Size(148, 31);
            this.LogNicknameInput.TabIndex = 14;
            // 
            // LogBackButton
            // 
            this.LogBackButton.BackColor = System.Drawing.Color.Transparent;
            this.LogBackButton.BackgroundImage = global::Client.Properties.Resources.back;
            this.LogBackButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LogBackButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LogBackButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(218)))), ((int)(((byte)(218)))));
            this.LogBackButton.FlatAppearance.BorderSize = 0;
            this.LogBackButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LogBackButton.Location = new System.Drawing.Point(23, 371);
            this.LogBackButton.Name = "LogBackButton";
            this.LogBackButton.Size = new System.Drawing.Size(48, 35);
            this.LogBackButton.TabIndex = 12;
            this.LogBackButton.UseVisualStyleBackColor = false;
            this.LogBackButton.Click += new System.EventHandler(this.LogBackButton_Click);
            // 
            // LogPasswordInput
            // 
            this.LogPasswordInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(225)))));
            this.LogPasswordInput.Font = new System.Drawing.Font("Comic Sans MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LogPasswordInput.Location = new System.Drawing.Point(208, 205);
            this.LogPasswordInput.Name = "LogPasswordInput";
            this.LogPasswordInput.Size = new System.Drawing.Size(148, 31);
            this.LogPasswordInput.TabIndex = 11;
            // 
            // LoginButton
            // 
            this.LoginButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(190)))));
            this.LoginButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoginButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(139)))), ((int)(((byte)(147)))));
            this.LoginButton.FlatAppearance.BorderSize = 2;
            this.LoginButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoginButton.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LoginButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(69)))), ((int)(((byte)(75)))));
            this.LoginButton.Location = new System.Drawing.Point(120, 289);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(218, 56);
            this.LoginButton.TabIndex = 9;
            this.LoginButton.Text = "Войти";
            this.LoginButton.UseVisualStyleBackColor = false;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // LogPasswordLabel
            // 
            this.LogPasswordLabel.AutoSize = true;
            this.LogPasswordLabel.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LogPasswordLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(69)))), ((int)(((byte)(75)))));
            this.LogPasswordLabel.Location = new System.Drawing.Point(87, 208);
            this.LogPasswordLabel.Name = "LogPasswordLabel";
            this.LogPasswordLabel.Size = new System.Drawing.Size(65, 20);
            this.LogPasswordLabel.TabIndex = 1;
            this.LogPasswordLabel.Text = "Пароль";
            // 
            // LogNicknameLabel
            // 
            this.LogNicknameLabel.AutoSize = true;
            this.LogNicknameLabel.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LogNicknameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(69)))), ((int)(((byte)(75)))));
            this.LogNicknameLabel.Location = new System.Drawing.Point(87, 157);
            this.LogNicknameLabel.Name = "LogNicknameLabel";
            this.LogNicknameLabel.Size = new System.Drawing.Size(75, 20);
            this.LogNicknameLabel.TabIndex = 0;
            this.LogNicknameLabel.Text = "Никнейм";
            // 
            // Logo
            // 
            this.Logo.AutoSize = true;
            this.Logo.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Logo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(147)))), ((int)(((byte)(152)))));
            this.Logo.Location = new System.Drawing.Point(74, 29);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(339, 91);
            this.Logo.TabIndex = 14;
            this.Logo.Text = "Squarea";
            // 
            // RegPanel
            // 
            this.RegPanel.Controls.Add(this.RegRepPasswordInput);
            this.RegPanel.Controls.Add(this.RegPasswordInput);
            this.RegPanel.Controls.Add(this.RegNicknameInput);
            this.RegPanel.Controls.Add(this.RegRepPasswordLabel);
            this.RegPanel.Controls.Add(this.RegBackButton);
            this.RegPanel.Controls.Add(this.RegisterButton);
            this.RegPanel.Controls.Add(this.RegPasswordLabel);
            this.RegPanel.Controls.Add(this.RegNicknameLabel);
            this.RegPanel.Location = new System.Drawing.Point(11, 12);
            this.RegPanel.Name = "RegPanel";
            this.RegPanel.Size = new System.Drawing.Size(458, 429);
            this.RegPanel.TabIndex = 15;
            // 
            // RegRepPasswordInput
            // 
            this.RegRepPasswordInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(225)))));
            this.RegRepPasswordInput.Font = new System.Drawing.Font("Comic Sans MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RegRepPasswordInput.Location = new System.Drawing.Point(214, 232);
            this.RegRepPasswordInput.Name = "RegRepPasswordInput";
            this.RegRepPasswordInput.Size = new System.Drawing.Size(148, 31);
            this.RegRepPasswordInput.TabIndex = 17;
            // 
            // RegPasswordInput
            // 
            this.RegPasswordInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(225)))));
            this.RegPasswordInput.Font = new System.Drawing.Font("Comic Sans MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RegPasswordInput.Location = new System.Drawing.Point(214, 181);
            this.RegPasswordInput.Name = "RegPasswordInput";
            this.RegPasswordInput.Size = new System.Drawing.Size(148, 31);
            this.RegPasswordInput.TabIndex = 16;
            // 
            // RegNicknameInput
            // 
            this.RegNicknameInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(225)))));
            this.RegNicknameInput.Font = new System.Drawing.Font("Comic Sans MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RegNicknameInput.Location = new System.Drawing.Point(214, 132);
            this.RegNicknameInput.Name = "RegNicknameInput";
            this.RegNicknameInput.Size = new System.Drawing.Size(148, 31);
            this.RegNicknameInput.TabIndex = 15;
            // 
            // RegRepPasswordLabel
            // 
            this.RegRepPasswordLabel.AutoSize = true;
            this.RegRepPasswordLabel.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RegRepPasswordLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(69)))), ((int)(((byte)(75)))));
            this.RegRepPasswordLabel.Location = new System.Drawing.Point(87, 237);
            this.RegRepPasswordLabel.Name = "RegRepPasswordLabel";
            this.RegRepPasswordLabel.Size = new System.Drawing.Size(121, 20);
            this.RegRepPasswordLabel.TabIndex = 13;
            this.RegRepPasswordLabel.Text = "Повтор пароля";
            // 
            // RegBackButton
            // 
            this.RegBackButton.BackColor = System.Drawing.Color.Transparent;
            this.RegBackButton.BackgroundImage = global::Client.Properties.Resources.back;
            this.RegBackButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RegBackButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RegBackButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(218)))), ((int)(((byte)(218)))));
            this.RegBackButton.FlatAppearance.BorderSize = 0;
            this.RegBackButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RegBackButton.Location = new System.Drawing.Point(23, 371);
            this.RegBackButton.Name = "RegBackButton";
            this.RegBackButton.Size = new System.Drawing.Size(48, 35);
            this.RegBackButton.TabIndex = 12;
            this.RegBackButton.UseVisualStyleBackColor = false;
            this.RegBackButton.Click += new System.EventHandler(this.RegBackButton_Click);
            // 
            // RegisterButton
            // 
            this.RegisterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(190)))));
            this.RegisterButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RegisterButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(139)))), ((int)(((byte)(147)))));
            this.RegisterButton.FlatAppearance.BorderSize = 2;
            this.RegisterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RegisterButton.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RegisterButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(69)))), ((int)(((byte)(75)))));
            this.RegisterButton.Location = new System.Drawing.Point(120, 289);
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.Size = new System.Drawing.Size(218, 56);
            this.RegisterButton.TabIndex = 9;
            this.RegisterButton.Text = "Зарегистрироваться";
            this.RegisterButton.UseVisualStyleBackColor = false;
            this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // RegPasswordLabel
            // 
            this.RegPasswordLabel.AutoSize = true;
            this.RegPasswordLabel.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RegPasswordLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(69)))), ((int)(((byte)(75)))));
            this.RegPasswordLabel.Location = new System.Drawing.Point(87, 188);
            this.RegPasswordLabel.Name = "RegPasswordLabel";
            this.RegPasswordLabel.Size = new System.Drawing.Size(65, 20);
            this.RegPasswordLabel.TabIndex = 1;
            this.RegPasswordLabel.Text = "Пароль";
            // 
            // RegNicknameLabel
            // 
            this.RegNicknameLabel.AutoSize = true;
            this.RegNicknameLabel.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RegNicknameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(69)))), ((int)(((byte)(75)))));
            this.RegNicknameLabel.Location = new System.Drawing.Point(87, 139);
            this.RegNicknameLabel.Name = "RegNicknameLabel";
            this.RegNicknameLabel.Size = new System.Drawing.Size(75, 20);
            this.RegNicknameLabel.TabIndex = 0;
            this.RegNicknameLabel.Text = "Никнейм";
            // 
            // AuthPanel
            // 
            this.AuthPanel.Controls.Add(this.GuestLogButton);
            this.AuthPanel.Controls.Add(this.AuthLogButton);
            this.AuthPanel.Controls.Add(this.AuthRegButton);
            this.AuthPanel.Location = new System.Drawing.Point(11, 12);
            this.AuthPanel.Name = "AuthPanel";
            this.AuthPanel.Size = new System.Drawing.Size(458, 429);
            this.AuthPanel.TabIndex = 16;
            // 
            // GuestLogButton
            // 
            this.GuestLogButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(190)))));
            this.GuestLogButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GuestLogButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(139)))), ((int)(((byte)(147)))));
            this.GuestLogButton.FlatAppearance.BorderSize = 2;
            this.GuestLogButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GuestLogButton.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GuestLogButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(69)))), ((int)(((byte)(75)))));
            this.GuestLogButton.Location = new System.Drawing.Point(120, 339);
            this.GuestLogButton.Name = "GuestLogButton";
            this.GuestLogButton.Size = new System.Drawing.Size(218, 56);
            this.GuestLogButton.TabIndex = 11;
            this.GuestLogButton.Text = "Гостевой режим";
            this.GuestLogButton.UseVisualStyleBackColor = false;
            this.GuestLogButton.Click += new System.EventHandler(this.GuestLogButton_Click);
            // 
            // AuthLogButton
            // 
            this.AuthLogButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(190)))));
            this.AuthLogButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AuthLogButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(139)))), ((int)(((byte)(147)))));
            this.AuthLogButton.FlatAppearance.BorderSize = 2;
            this.AuthLogButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AuthLogButton.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AuthLogButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(69)))), ((int)(((byte)(75)))));
            this.AuthLogButton.Location = new System.Drawing.Point(120, 237);
            this.AuthLogButton.Name = "AuthLogButton";
            this.AuthLogButton.Size = new System.Drawing.Size(218, 56);
            this.AuthLogButton.TabIndex = 9;
            this.AuthLogButton.Text = "Войти";
            this.AuthLogButton.UseVisualStyleBackColor = false;
            this.AuthLogButton.Click += new System.EventHandler(this.AuthLogButton_Click);
            // 
            // AuthRegButton
            // 
            this.AuthRegButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(190)))));
            this.AuthRegButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AuthRegButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(139)))), ((int)(((byte)(147)))));
            this.AuthRegButton.FlatAppearance.BorderSize = 2;
            this.AuthRegButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AuthRegButton.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AuthRegButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(69)))), ((int)(((byte)(75)))));
            this.AuthRegButton.Location = new System.Drawing.Point(120, 139);
            this.AuthRegButton.Name = "AuthRegButton";
            this.AuthRegButton.Size = new System.Drawing.Size(218, 56);
            this.AuthRegButton.TabIndex = 10;
            this.AuthRegButton.Text = "Зарегистрироваться";
            this.AuthRegButton.UseVisualStyleBackColor = false;
            this.AuthRegButton.Click += new System.EventHandler(this.AuthRegButton_Click);
            // 
            // Loading
            // 
            this.Loading.AutoSize = true;
            this.Loading.Font = new System.Drawing.Font("Comic Sans MS", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Loading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(147)))), ((int)(((byte)(152)))));
            this.Loading.Location = new System.Drawing.Point(78, 195);
            this.Loading.Name = "Loading";
            this.Loading.Size = new System.Drawing.Size(322, 84);
            this.Loading.TabIndex = 17;
            this.Loading.Text = "Загрузка...";
            // 
            // CheckingForConnectionTimer
            // 
            this.CheckingForConnectionTimer.Enabled = true;
            this.CheckingForConnectionTimer.Interval = 1000;
            this.CheckingForConnectionTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // AuthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(218)))), ((int)(((byte)(218)))));
            this.ClientSize = new System.Drawing.Size(482, 453);
            this.Controls.Add(this.Loading);
            this.Controls.Add(this.Logo);
            this.Controls.Add(this.AuthPanel);
            this.Controls.Add(this.RegPanel);
            this.Controls.Add(this.LogPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AuthForm";
            this.Text = "Squarea Авторизация";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AuthForm_FormClosing);
            this.LogPanel.ResumeLayout(false);
            this.LogPanel.PerformLayout();
            this.RegPanel.ResumeLayout(false);
            this.RegPanel.PerformLayout();
            this.AuthPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel LogPanel;
        private System.Windows.Forms.TextBox LogNicknameInput;
        private System.Windows.Forms.Button LogBackButton;
        private System.Windows.Forms.TextBox LogPasswordInput;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Label LogPasswordLabel;
        private System.Windows.Forms.Label LogNicknameLabel;
        private System.Windows.Forms.Label Logo;
        private System.Windows.Forms.Panel RegPanel;
        private System.Windows.Forms.Button RegBackButton;
        private System.Windows.Forms.Button RegisterButton;
        private System.Windows.Forms.Label RegPasswordLabel;
        private System.Windows.Forms.Label RegNicknameLabel;
        private System.Windows.Forms.Label RegRepPasswordLabel;
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.TextBox RegRepPasswordInput;
        private System.Windows.Forms.TextBox RegPasswordInput;
        private System.Windows.Forms.TextBox RegNicknameInput;
        private System.Windows.Forms.Panel AuthPanel;
        private System.Windows.Forms.Button AuthLogButton;
        private System.Windows.Forms.Button AuthRegButton;
        private System.Windows.Forms.Label Loading;
        private System.Windows.Forms.Button GuestLogButton;
        private System.Windows.Forms.Timer CheckingForConnectionTimer;
    }
}

