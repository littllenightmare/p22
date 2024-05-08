using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using p22.database;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace p22
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadDBInListView();
        }

        void LoadDBInListView()
        {
            using(Pr22Context _db = new Pr22Context())
            {
                int selectedIndex = lv.SelectedIndex;
                _db.Клиентыs.Load();
                lv.ItemsSource = _db.ТуристическиеТурыs.ToList();
                if (selectedIndex != -1)
                {
                    if (selectedIndex == lv.Items.Count) selectedIndex--;
                    lv.SelectedIndex = selectedIndex;
                    lv.ScrollIntoView(lv.Items[selectedIndex]);
                }
                lv.Focus();
            }
        }

        private void WindowInitialized(object sender, EventArgs e)
        {
            utorize f = new utorize();
            f.ShowDialog();
            btndel.IsEnabled = true;
            btnred.IsEnabled = true;
            btnadd.IsEnabled = true;
            if (Data.Login == false) Close();
            if (Data.Right == "Администратор") ;
            else
            {
                btndel.IsEnabled = false;
                btnred.IsEnabled = false;
                btnadd.IsEnabled = false;
            }
        }

        private void addCLick(object sender, RoutedEventArgs e)
        {
            Data.туристическиетуры = null;
            AddRedCLient ar = new AddRedCLient();
            ar.Owner = this;
            ar.ShowDialog();
            LoadDBInListView();
        }

        private void redCLick(object sender, RoutedEventArgs e)
        {
            if (lv.SelectedItems != null)
            {
                Data.туристическиетуры = (ТуристическиеТуры)lv.SelectedItem;
                AddRedCLient ar = new AddRedCLient();
                ar.Owner = this;
                ar.WindowAddEdit.Title = "Редактирование записи";
                ar.btnadd.Content = "Редактировать";
                ar.ShowDialog();
                LoadDBInListView();
            }
        }

        private void delClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    ТуристическиеТуры row = (ТуристическиеТуры)lv.SelectedItem;
                    if (row != null)
                    {
                        using (Pr22Context _db = new Pr22Context())
                        {
                            _db.ТуристическиеТурыs.Remove(row);
                            _db.SaveChanges();
                        }
                        LoadDBInListView();
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка удаления");
                }
            }
            else lv.Focus();
        }

        private void autoClick(object sender, RoutedEventArgs e)
        {
            WindowInitialized(sender, e);
        }

        private void filtrClick(object sender, RoutedEventArgs e)
        {
            if (filtrtb.Text.IsNullOrEmpty() == false)
            {
                using (Pr22Context _db = new Pr22Context())
                {
                    _db.Клиентыs.Load();
                    var filtered = _db.ТуристическиеТурыs.Where(p => p.Страна.Contains(filtrtb.Text) ||
                    p.ДатаОтъезда.ToString().Contains(filtrtb.Text) ||
                    p.СтоимостьТура.ToString().Contains(filtrtb.Text) || p.КлиентNavigation.Адрес.Contains(filtrtb.Text)
                    || p.КлиентNavigation.Телефон.ToString().Contains(filtrtb.Text));
                    lv.ItemsSource = filtered.ToList();
                }
            }
            else
            {
                LoadDBInListView();
            }
        }
        private void sbrosClick(object sender, RoutedEventArgs e)
        {
            LoadDBInListView();
            filtrtb.Clear();
        }

        private void findCLick(object sender, RoutedEventArgs e)
        {
            if (findtb.Text.IsNullOrEmpty() == false)
            {
                List<ТуристическиеТуры> listItem2 = (List<ТуристическиеТуры>)lv.ItemsSource;
                var finded2 = listItem2.Where(p => p.ДатаОтъезда.ToString().Contains(findtb.Text) ||
                    p.КлиентNavigation.Адрес.ToString().Contains(findtb.Text) ||
                    p.КлиентNavigation.Телефон.ToString().Contains(findtb.Text) ||
                p.СтоимостьТура.ToString().Contains(findtb.Text));
                if (finded2.Count() > 0)
                {
                    ТуристическиеТуры item = finded2.First();
                    lv.SelectedItem = item;
                    lv.ScrollIntoView(item);
                    lv.Focus();
                }
            }
        }
    }
}