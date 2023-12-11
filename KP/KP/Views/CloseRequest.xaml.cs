using KP.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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

namespace KP.Views
{
    /// <summary>
    /// Логика взаимодействия для CloseRequest.xaml
    /// </summary>
    public partial class CloseRequest : Page
    {
        private TestRequests _currentTest = new TestRequests();


        public CloseRequest(TestRequests selectedTest)
        {
            InitializeComponent();


            if (selectedTest != null)
            {
                _currentTest = selectedTest;
            }

            DataContext = _currentTest;

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _currentTest.DateOfClose = DateTime.Now;

            StringBuilder errors = new StringBuilder();

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentTest.Id >= 0)
                StankiEntities.GetContext().TestRequests.AddOrUpdate(_currentTest);

            try
            {
                StankiEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                Manager.MyFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
