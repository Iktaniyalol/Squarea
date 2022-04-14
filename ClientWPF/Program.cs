using System;

namespace ClientWPF
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            new Client(); //Создаем объект клиента
            App application = new App();
            application.InitializeComponent();
            application.Run();
        }
    }
}
