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
using Улов.Entities;
using Улов.Infrastructure;

namespace Улов
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        int filterDisc = 0;
        double[,] arrayDisc = new double[,] { { 0, 100 }, { 0, 9.99 }, { 10, 14.99 }, { 15, 100 } };

        string search;

        public Catalog()
        {
            InitializeComponent();
            Init();
            Helper.ImportImage();
            Update();
        }

        public void Init()
        {
            ComboBoxSort.SelectedIndex = 0;
            ComboBoxSale.SelectedIndex = 0;
        }

        public void Update()
        {
            List<Entities.Catalog> product = null;
            product = Helper.DbContext.Catalog.ToList();

            if (product != null)
            {
                var allCount = product.Count;
                SaleSortCost(ref product);
                product = product.Where(x => (x.CurrentSale >= arrayDisc[filterDisc, 0] && x.CurrentSale <= arrayDisc[filterDisc, 1])).ToList();

                if (!String.IsNullOrEmpty(search))
                    product = product.Where(x => x.Name.Contains(search)).ToList();

                ListViewCatalog.ItemsSource = product;
                var currentCount = product.Count;
                LabelCount.Content = $"Количество продуктов {currentCount} из {allCount}";
            }
        }

        public void SaleSortCost(ref List<Entities.Catalog> product)
        {
            if (ComboBoxSort.SelectedIndex == 0)
                product = product.OrderBy(x => x.Cost).ToList();
            else
                product = product.OrderByDescending(x => x.Cost).ToList();
        }

        private void OnBtnExitClick(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Вы точно хотите выйти из приложения?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question)) this.Close();
        }

        private void OnComboBoxSortSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void OnComboBoxSaleSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filterDisc = ComboBoxSale.SelectedIndex;
            Update();
        }

        private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            search = TextBoxSearch.Text.Trim();
            Update();
        }
    }
}