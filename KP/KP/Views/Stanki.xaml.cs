using KP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
using Word = Microsoft.Office.Interop.Word;


namespace KP.Views
{
    /// <summary>
    /// Логика взаимодействия для Stanki.xaml
    /// </summary>
    public partial class Stanki : Page
    {
        public Stanki()
        {

            InitializeComponent();

            if (Authorization.Globals.UserRole == 1)
            {
                Delete.Visibility = Visibility.Visible;
            }
            else if (Authorization.Globals.UserRole == 3)
            {
                Delete.Visibility = Visibility.Collapsed;
                Add.Visibility = Visibility.Collapsed;
            }
            else
            {
                Delete.Visibility = Visibility.Collapsed;
            }

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MyFrame.Navigate(new AddEditStanki((sender as Button).DataContext as Machine));
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Manager.MyFrame.Navigate(new AddEditStanki(null));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var machineForRemoving = MachinesGrid.SelectedItems.Cast<Machine>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {machineForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    StankiEntities.GetContext().Machine.RemoveRange(machineForRemoving);
                    StankiEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    MachinesGrid.ItemsSource = StankiEntities.GetContext().Machine.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                StankiEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                MachinesGrid.ItemsSource = StankiEntities.GetContext().Machine.ToList();
            }
        }

        
    }
}
