using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ClientWPF.Net.Packets;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace ClientWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        Grid activeGrid;
        bool isClosedForTransition = false;
        string playerName = null;
        WaitCallbackCountdown countdown;
        DataPacket resultAuthPacket = null;

        public MainWindow()
        {
            InitializeComponent();
            Client.Instance.MainWindow = this;
            SetActiveAuthGrid();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        private void TimerTick(object sender, EventArgs e)
        {
            if (!Client.Instance.Network.connected)
            {
                AuthGrid.Visibility = Visibility.Hidden;
                RegGrid.Visibility = Visibility.Hidden;
                LogGrid.Visibility = Visibility.Hidden;
                LoadingGrid.Visibility = Visibility.Visible;
                activeGrid = AuthGrid;

            }
            else
            {
                LoadingGrid.Visibility = Visibility.Hidden;
                AuthGrid.Visibility = Visibility.Hidden;
                RegGrid.Visibility = Visibility.Hidden;
                LogGrid.Visibility = Visibility.Hidden;
                activeGrid.Visibility = Visibility.Visible;
            }
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            SetActiveRegisterGrid();
        }

        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            SetActiveLoginGrid();
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            GuestLoginPacket guestLoginPacket = new GuestLoginPacket();
            Client.Instance.SendPacketToServer(guestLoginPacket);
            SetActiveLoadingGrid();
            countdown = new WaitCallbackCountdown(this, WaitCallbackCountdown.CallbackType.GUEST, 5);
            //Ждем ответ, закрытие формы произойдет если придет ответ. А так выдаем загрузку.
            //Если ответ не придет через 5 секунд, выдаст ошибку.
        }

        private void AuthWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isClosedForTransition)
            {
                Client.Instance.DisconnectFromServer();
                Client.Instance.Close();
            }
        }

        private void LogBackButton_Click(object sender, RoutedEventArgs e)
        {
            NicknameInput.Text = "";
            PasswordInput.Password = "";
            NicknameInput1.Text = "";
            RetypePasswordInput.Password = "";
            PasswordInput1.Password = "";
            SetActiveAuthGrid();
        }

        private void RegBackButton_Click(object sender, RoutedEventArgs e)
        {
            NicknameInput.Text = "";
            PasswordInput.Password = "";
            NicknameInput1.Text = "";
            RetypePasswordInput.Password = "";
            PasswordInput1.Password = "";
            SetActiveAuthGrid();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(NicknameInput.Text, "^[a-zA-Z0-9_-]{3,20}$"))
            {
                MessageBox.Show("Недопустимый никнейм. Допустимая длина: 3-20\nДопустимые символы:\nанглийские буквы, цифры, нижнее подчеркивание и дефис.");
                return;
            }
            if (!Regex.IsMatch(PasswordInput.Password, "^((?=.*[a-zA-Z])(?=.*[0-9]).{6,20})$"))
            {
                MessageBox.Show("Недопустимый пароль. Длина пароля должна быть от 6 до 20\nи он должен обязательно содержать буквы любого регистра и цифры.");
                return;
            }
            LoginPacket loginPacket = new LoginPacket();
            loginPacket.username = NicknameInput.Text;
            loginPacket.password = PasswordInput.Password;
            Client.Instance.SendPacketToServer(loginPacket);
            SetActiveLoadingGrid();
            countdown = new WaitCallbackCountdown(this, WaitCallbackCountdown.CallbackType.LOGIN, 5);
            playerName = NicknameInput.Text;
            //Ждем ответ, закрытие формы произойдет если придет ответ. А так выдаем загрузку.
            //Если ответ не придет через 5 секунд, выдаст ошибку.
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordInput1.Password != RetypePasswordInput.Password)
            {
                MessageBox.Show("Введённые пароли не совпадают!");
                return;
            }
            if (!Regex.IsMatch(NicknameInput1.Text, "^[a-zA-Z0-9_-]{3,20}$"))
            {
                MessageBox.Show("Недопустимый никнейм. Допустимая длина: 3-20;\nДопустимые символы:\nанглийские буквы, цифры, нижнее подчеркивание и дефис.");
                return;
            }
            if (!Regex.IsMatch(PasswordInput1.Password, "^((?=.*[a-zA-Z])(?=.*[0-9]).{6,20})$"))
            {
                MessageBox.Show("Недопустимый пароль. Длина пароля должна быть от 6 до 20\nи он должен обязательно содержать буквы любого регистра и цифры.");
                return;
            }
            RegisterPacket registerPacket = new RegisterPacket();
            registerPacket.username = NicknameInput1.Text;
            registerPacket.password = PasswordInput1.Password;
            Client.Instance.SendPacketToServer(registerPacket);
            SetActiveLoadingGrid();
            countdown = new WaitCallbackCountdown(this, WaitCallbackCountdown.CallbackType.REGISTER, 5);
            playerName = NicknameInput1.Text;
            //Ждем ответ, закрытие формы произойдет если придет ответ. А так выдаем загрузку.
            //Если ответ не придет через 5 секунд, выдаст ошибку.
        }

        public void SetClosedForTransition()
        {
            isClosedForTransition = true;
            countdown.Stop();
        }

        public void CreatePreGameWindow ()
        {
            isClosedForTransition = true;
            PreGameWindow window = new PreGameWindow(playerName); //Передача ника в новую форму
            window.Show();
            Client.Instance.MainWindow.Close();
        }

        public void SetActiveRegisterGrid()
        {
            activeGrid = RegGrid;
            RegGrid.Visibility = Visibility.Visible;
            LogGrid.Visibility = Visibility.Hidden;
            AuthGrid.Visibility = Visibility.Hidden;
            LoadingGrid.Visibility = Visibility.Hidden;
        }

        public void SetActiveLoginGrid()
        {
            activeGrid = LogGrid;
            LogGrid.Visibility = Visibility.Visible;
            AuthGrid.Visibility = Visibility.Hidden;
            LoadingGrid.Visibility = Visibility.Hidden;
            RegGrid.Visibility = Visibility.Hidden;
        }

        public void SetActiveLoadingGrid()
        {
            activeGrid = LoadingGrid;
            LoadingGrid.Visibility = Visibility.Visible;
            AuthGrid.Visibility = Visibility.Hidden;
            LogGrid.Visibility = Visibility.Hidden;
            RegGrid.Visibility = Visibility.Hidden;
        }

        public void SetActiveAuthGrid()
        {
            activeGrid = AuthGrid;
            AuthGrid.Visibility = Visibility.Visible;
            LoadingGrid.Visibility = Visibility.Hidden;
            LogGrid.Visibility = Visibility.Hidden;
            RegGrid.Visibility = Visibility.Hidden;
        }

        public DataPacket ResultAuthPacket
        {
            get
            {
                return resultAuthPacket;
            }
            set
            {
                resultAuthPacket = value;
            }
        }

        public string PlayerName
        {
            get
            {
                return playerName;
            }
            set
            {
                playerName = value;
            }
        }
    }

    class WaitCallbackCountdown
    {
        TimeSpan time;
        DispatcherTimer timer;
        MainWindow window;
        int countdown;
        public enum CallbackType
        {
            REGISTER,
            LOGIN,
            GUEST
        }

        public WaitCallbackCountdown(MainWindow window, CallbackType type, int countdown)
        {
            this.window = window;
            this.countdown = countdown;
            switch (type)
            {
                case CallbackType.REGISTER:
                    {
                        RegisterCountdown();
                        break;
                    }
                case CallbackType.LOGIN:
                    {
                        LoginCountdown();
                        break;
                    }
                case CallbackType.GUEST:
                    {
                        GuestCountdown();
                        break;
                    }
            }
        }
        private void RegisterCountdown()
        {
            time = TimeSpan.FromSeconds(countdown);
            timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate {
                if (window.ResultAuthPacket != null)
                {
                    if (!(window.ResultAuthPacket is RegisterResultPacket))
                    {
                        MessageBox.Show("Произошла ошибка");
                        window.SetActiveRegisterGrid();
                    }
                    else
                    {
                        switch (((RegisterResultPacket)window.ResultAuthPacket).status)
                        {
                            case RegisterResultPacket.Result.UsernameAlreadyUsed:
                                {
                                    MessageBox.Show("Такой никнейм уже занят. Придумайте другой.");
                                    window.SetActiveRegisterGrid();
                                    break;
                                }
                            case RegisterResultPacket.Result.Error:
                                {
                                    MessageBox.Show("Произошла ошибка");
                                    window.SetActiveRegisterGrid();
                                    break;
                                }
                            case RegisterResultPacket.Result.Success:
                                {
                                    window.SetActiveLoginGrid();
                                    break;
                                }
                            default:
                                {
                                    MessageBox.Show("Произошла ошибка");
                                    window.SetActiveRegisterGrid();
                                    break;
                                }
                        }

                    }
                    timer.Stop();
                    return;
                }
                if (time <= TimeSpan.Zero)
                {
                    timer.Stop();
                    MessageBox.Show("Превышено время ожидания");
                    window.SetActiveRegisterGrid();
                    return;

                }
                time = time.Add(TimeSpan.FromSeconds(-1));

            }, Application.Current.Dispatcher);
            timer.Start();
        }

        private void LoginCountdown()
        {
            time = TimeSpan.FromSeconds(countdown);
            timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate {
                if (window.ResultAuthPacket != null)
                {
                    if (!(window.ResultAuthPacket is LoginResultPacket))
                    {
                        MessageBox.Show("Произошла ошибка");
                        window.SetActiveLoginGrid();
                    }
                    else
                    {
                        switch (((LoginResultPacket)window.ResultAuthPacket).status)
                        {
                            case LoginResultPacket.Result.InvalidLoginOrPassword:
                                {
                                    MessageBox.Show("Неверный логин или пароль. Попробуйте снова!");
                                    window.SetActiveLoginGrid();
                                    break;
                                }
                            case LoginResultPacket.Result.Error:
                                {
                                    MessageBox.Show("Произошла ошибка");
                                    window.SetActiveLoginGrid();
                                    break;
                                }
                            case LoginResultPacket.Result.Success:
                                {
                                    window.CreatePreGameWindow();
                                    break;
                                }
                            default:
                                {
                                    MessageBox.Show("Произошла ошибка");
                                    window.SetActiveLoginGrid();
                                    break;
                                }
                        }

                    }
                    timer.Stop();
                    return;
                }
                if (time <= TimeSpan.Zero)
                {
                    timer.Stop();
                    MessageBox.Show("Превышено время ожидания");
                    window.SetActiveLoginGrid();
                    return;

                }
                time = time.Add(TimeSpan.FromSeconds(-1));

            }, Application.Current.Dispatcher);
            timer.Start();
        }

        private void GuestCountdown()
        {
            time = TimeSpan.FromSeconds(countdown);
            timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (window.ResultAuthPacket != null)
                {
                    if (!(window.ResultAuthPacket is GuestLoginResultPacket))
                    {
                        MessageBox.Show("Произошла ошибка");
                        window.SetActiveAuthGrid();
                    }
                    else
                    {
                        window.PlayerName = ((GuestLoginResultPacket)window.ResultAuthPacket).playerName;
                        window.CreatePreGameWindow();
                    }
                    timer.Stop();
                    return;
                }

                if (time <= TimeSpan.Zero)
                {
                    MessageBox.Show("Превышено время ожидания");
                    window.SetActiveAuthGrid();
                    timer.Stop();
                    return;

                }
                time = time.Add(TimeSpan.FromSeconds(-1));

            }, Application.Current.Dispatcher);
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }
    }
}
