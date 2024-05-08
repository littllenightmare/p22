using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using p22.database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
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

namespace p22
{
    /// <summary>
    /// Логика взаимодействия для AddRed.xaml
    /// </summary>
    public partial class AddRedCLient : Window
    {
        public AddRedCLient()
        {
            InitializeComponent();
        }
        Pr22Context _db = new Pr22Context();
        ТуристическиеТуры _туристическиетуры;
        OpenFileDialog open = new OpenFileDialog();

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            if(Data.туристическиетуры == null)
            {
                _туристическиетуры = new ТуристическиеТуры();
                tbadress.Visibility = Visibility.Hidden;
                tbphone.Visibility = Visibility.Hidden;
                imgPhoto.Visibility = Visibility.Hidden;
            }
            else
            {
                _туристическиетуры = _db.ТуристическиеТурыs.Find(Data.туристическиетуры.ДатаОтъезда, Data.туристическиетуры.Клиент);
            }
            WindowAddEdit.DataContext = _туристическиетуры;
            _db.Клиентыs.Load();
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            _db.Клиентыs.Load();
            StringBuilder errors = new StringBuilder();
            if(tbcountry.Text.Length == 0) errors.AppendLine("Введите страну");
            if (tbsum.Text.Length == 0 || tbsum.Text == "0" || BigInteger.TryParse(tbsum.Text, out BigInteger a)== false) errors.AppendLine("Введите сумму");

            if (errors.Length > 0)
            {
                System.Windows.MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                if(open.SafeFileName.Length != 0)
                {
                    string newNamePhoto = Directory.GetCurrentDirectory() +
                        "\\image\\" + open.SafeFileName;
                    File.Copy(open.FileName, newNamePhoto, true);
                    _туристическиетуры.КлиентNavigation.Фото = open.SafeFileName;
                }
            } catch { }
            try
            {
                if(Data.клиенты== null)
                {
                    Random rnd = new Random();
                    _туристическиетуры.Клиент = rnd.Next(1,9);
                    _туристическиетуры.Фирма = rnd.Next(1, 9);
                    _db.ТуристическиеТурыs.Add(_туристическиетуры);
                    _db.SaveChanges();
                }
                else
                {
                    _db.SaveChanges();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }

        private void AddPhoto(object sender, RoutedEventArgs e)
        {
            open.Filter = "Все файлы |*.*| Файлы *.jpg|*.jpg";
            open.FilterIndex = 2;
            if (open.ShowDialog() == true)
            {
                BitmapImage img = new BitmapImage(new Uri(open.FileName));
                imgPhoto.Source = img;
            }
        }

        private void NoClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
