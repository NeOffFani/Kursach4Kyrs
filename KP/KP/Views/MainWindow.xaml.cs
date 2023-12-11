using KP.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;


namespace KP.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MyFrame.Navigate(new Stanki());
            Manager.MyFrame = MyFrame;

            if (Authorization.Globals.UserRole == 1)
            {
                BackUp.Visibility = Visibility.Visible;
            }
            else
            {
                BackUp.Visibility = Visibility.Collapsed;
            }

        }

        private void Repair_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(new RepairPage());
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(new TestPage());
        }


        private void Machine_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(new Stanki());
        }

        private void BackUp_Click(object sender, RoutedEventArgs e)
        {
            {
                try
                {
                    string serverName = "DESKTOP-FEQVI6A\\SQLEXPRESS";
                    string databaseName = "Department";
                    string backupPath = @"E:\BackUps\backup.bak";

                    ServerConnection serverConnection = new ServerConnection(serverName);
                    Server server = new Server(serverConnection);
                    Backup backup = new Backup
                    {
                        Action = BackupActionType.Database,
                        Database = databaseName
                    };

                    backup.Devices.AddDevice(backupPath, DeviceType.File);

                    backup.Initialize = true;
                    backup.Checksum = true;
                    backup.ContinueAfterError = true;

                    backup.SqlBackup(server);

                    MessageBox.Show("Backup completed successfully.", "Backup Status", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during backup: {ex.Message}", "Backup Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}

