using System;
using System.Windows.Forms;

namespace Client
{
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
            AuthPanel.Visible = false;
            LogPanel.Visible = false;
            RegPanel.Visible = false;
            Loading.Visible = true;//Текст, информирующий о том, что идет загрузка
        }

        private void AuthLogButton_Click(object sender, EventArgs e)
        {
            LogPanel.Visible = true;
            AuthPanel.Visible = false;
        }

        private void AuthRegButton_Click(object sender, EventArgs e)
        {
            RegPanel.Visible = true;
            AuthPanel.Visible = false;
        }

        private void LogBackButton_Click(object sender, EventArgs e)
        {
            LogPanel.Visible = false;
            AuthPanel.Visible = true;
        }

        private void RegBackButton_Click(object sender, EventArgs e)
        {
            RegPanel.Visible = false;
            AuthPanel.Visible = true;
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            //Регистрация
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            //Авторизация
        }

        private void GuestLogButton_Click(object sender, EventArgs e)
        {
            //Гостевой режим без регистрации и авторизации.
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!Client.GetInstance.GetNetwork.connected)
            {
                AuthPanel.Visible = false;
                LogPanel.Visible = false;
                RegPanel.Visible = false;
                Loading.Visible = true;
            }
            else
            {
                Loading.Visible = false; //Текст, информирующий о том, что идет загрузка.
                RegPanel.Visible = false;
                LogPanel.Visible = false;
                AuthPanel.Visible = true;
            }
        }

        private void AuthForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Client.GetInstance.GetNetwork.DestroyNetworkThreads();
        }
    }
}
