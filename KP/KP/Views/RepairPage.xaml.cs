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
using Word = Microsoft.Office.Interop.Word;


namespace KP.Views
{
    /// <summary>
    /// Логика взаимодействия для RepairPage.xaml
    /// </summary>
    public partial class RepairPage : Page
    {
        public RepairPage()
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
                Excel.Visibility = Visibility.Collapsed;
            }
            else
            {
                Excel.Visibility = Visibility.Collapsed;
                Delete.Visibility = Visibility.Collapsed;
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            StankiEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            RepairGrid.ItemsSource = StankiEntities.GetContext().RepairRequest.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MyFrame.Navigate(new AddEditRepair((sender as Button).DataContext as RepairRequest));
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {

            Manager.MyFrame.Navigate(new CloseRequestR((sender as Button).DataContext as RepairRequest));
        
        
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Manager.MyFrame.Navigate(new AddEditRepair(null));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var RequestForRemoving = RepairGrid.SelectedItems.Cast<RepairRequest>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {RequestForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    StankiEntities.GetContext().RepairRequest.RemoveRange(RequestForRemoving);
                    StankiEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    RepairGrid.ItemsSource = StankiEntities.GetContext().RepairRequest.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            var allRequest = StankiEntities.GetContext().RepairRequest.ToList();

            var application = new Word.Application();

            Word.Document document = application.Documents.Add();

            Word.Paragraph userParagraph = document.Paragraphs.Add();
            Word.Range userRange = userParagraph.Range;
            userRange.Text = "Заявки на ремонт";

            userRange.InsertParagraphAfter();

            Word.Paragraph tableParagraph = document.Paragraphs.Add();
            Word.Range tableRange = tableParagraph.Range;
            Word.Table paymentsTable = document.Tables.Add(tableRange, allRequest.Count() + 1, 6);
            paymentsTable.Borders.InsideLineStyle = paymentsTable.Borders.OutsideLineStyle
                = Word.WdLineStyle.wdLineStyleSingle;
            paymentsTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

            Word.Range cellRange;

            

            cellRange = paymentsTable.Cell(1, 1).Range;
            cellRange.Text = "Номер заявки";
            cellRange = paymentsTable.Cell(1, 2).Range;
            cellRange.Text = "Номер станка";
            cellRange = paymentsTable.Cell(1, 3).Range;
            cellRange.Text = "Дата Создания";
            cellRange = paymentsTable.Cell(1, 4).Range;
            cellRange.Text = "Создатель заявки";
            cellRange = paymentsTable.Cell(1, 5).Range;
            cellRange.Text = "Инженер";
            cellRange = paymentsTable.Cell(1, 6).Range;
            cellRange.Text = "Результат";



            paymentsTable.Rows[1].Range.Bold = 1;
            paymentsTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            for (int i = 0; i < allRequest.Count(); i++)
            {
                var currentCategory = allRequest[i];
                if(currentCategory.Result != null) 
                { 
                cellRange = paymentsTable.Cell(i + 2, 1).Range;
                cellRange.Text = Convert.ToString(currentCategory.Id);
                cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                cellRange = paymentsTable.Cell(i + 2, 2).Range;
                cellRange.Text = Convert.ToString(currentCategory.IdMachine);

                cellRange = paymentsTable.Cell(i + 2, 3).Range;
                cellRange.Text = currentCategory.DateOfCreate.ToString("dd.MM.yyyy");

                cellRange = paymentsTable.Cell(i + 2, 4).Range;
                cellRange.Text = Convert.ToString(currentCategory.Creator);

                cellRange = paymentsTable.Cell(i + 2, 5).Range;
                cellRange.Text = Convert.ToString(currentCategory.UsersTable.FullName);

                cellRange = paymentsTable.Cell(i + 2, 6).Range;
                cellRange.Text = Convert.ToString(currentCategory.Result);
                }
            }

            application.Visible = true;
        }
    }
}
