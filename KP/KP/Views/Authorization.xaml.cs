using DocumentFormat.OpenXml.Drawing.Charts;
using KP.Model;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace KP.Views
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {

        DispatcherTimer timer = new DispatcherTimer();
        string code;

        public Authorization()
        {
            InitializeComponent();

        }
        public static class Globals
        {
            public static int UserRole;
            public static UsersTable userinfo { get; set; }
        }

        private void gencode()
        {
            code = null;
            Random random = new Random();
            string[] massiveCharacter = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", };
            for (int i = 0; i < 4; i++)
            {
                code += massiveCharacter[random.Next(0, massiveCharacter.Length)];
            }
            if (MessageBox.Show(code.ToString(), "Code", MessageBoxButton.OK, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                timer.Interval = TimeSpan.FromSeconds(10);
                timer.Tick += Timer_Tick;
                timer.Start();

                CodeBox.IsEnabled = true;
                TxbLogin.IsEnabled = true;
                RefreshBtn.Visibility = Visibility.Visible;

            }
        }
        private void Login_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                using (var db = new StankiEntities())
                {
                    var userLogin = Encryption.hashPassword(TxbLogin.Text);
                    var login = AppData.db.UsersTable.FirstOrDefault(l => l.Login == userLogin);
                    if (login == null)
                    {
                        MessageBox.Show("Неверный логин");
                    }
                    else
                    {
                        TxbPassword.IsEnabled = true;
                        TxbLogin.IsEnabled = false;
                        TxbPassword.Focus();
                    }
                }
            }
        }
        private void Password_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                using (var db = new StankiEntities())
                {
                    var userLogin = Encryption.hashPassword(TxbLogin.Text);
                    var userPass = Encryption.hashPassword(TxbPassword.Password);
                    var login = AppData.db.UsersTable.FirstOrDefault(l => l.Login == userLogin && l.Password == userPass);
                    if (login == null)
                    {
                        MessageBox.Show("Неверный пароль");
                    }
                    else
                    {
                        TxbPassword.IsEnabled = false;
                        CodeBox.Visibility = Visibility.Visible;
                        CodeBlock.Visibility = Visibility.Visible;
                        RefreshBtn.Visibility = Visibility.Visible;
                        gencode();
                        CodeBox.Focus();
                    }
                }
            }
        }
        void Timer_Tick(Object sender, EventArgs e)
        {
            code = null;
            MessageBox.Show("Время написания кода вышло. Повторите попытку");
            timer.Stop();
        }

        private void Autorization_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new StankiEntities())
            {
                var userLogin = Encryption.hashPassword(TxbLogin.Text);
                var userPass = Encryption.hashPassword(TxbPassword.Password);
                var auth = AppData.db.UsersTable.FirstOrDefault(m => m.Login == userLogin && m.Password == userPass);
                if (auth != null & code == CodeBox.Text)
                {
                    timer.Stop();
                    Globals.UserRole = auth.RoleId;
                    Globals.userinfo = auth;
                    MainWindow main = new MainWindow();
                    main.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Неверный код, повторите попытку!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    timer.Stop();
                }
            }
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            gencode();
            CodeBox.Focus();
        }
    }
}



