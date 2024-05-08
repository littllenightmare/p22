using Microsoft.EntityFrameworkCore;
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

namespace p22.database
{
    /// <summary>
    /// Логика взаимодействия для utorize.xaml
    /// </summary>
    public partial class utorize : Window
    {
        
        DispatcherTimer _timer;
        int _countLogin = 1;
        public utorize()
        {
            InitializeComponent();
        }

        private void WindowActivated(object sender, EventArgs e)
        {
            tbLogin.Focus();
            Data.Login = false;
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 10);
            _timer.Tick += new EventHandler(timer_Tick);
        }
        void GetCaptcha()
        {
            string masChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghigklmnopqrstuvwxyz" + "1234567890";
            string captcha = "";
            Random rnd = new Random();
            for (int i = 0; i <= 5; i++)
            {
                var a = masChar[rnd.Next(0, masChar.Length)];
                captcha = captcha + a;
            }
            grid.Visibility = Visibility.Visible;
            Captcha.Text = captcha;
            tbCaptcha.Text = null;
            Captcha.LayoutTransform = new TranslateTransform(-15, 15);

            line.X1 = 5;
            line.Y1 = rnd.Next(10, 40);
            line.X2 = 255;
            line.Y2 = rnd.Next(10, 40);

            line2.X1 = 15;
            line2.Y1 = rnd.Next(10, 40);
            line2.X2 = 115;
            line2.Y2 = rnd.Next(10, 40);

            line3.X1 = 25;
            line3.Y1 = rnd.Next(10, 40);
            line3.X2 = 125;
            line3.Y2 = rnd.Next(10, 40);

            line4.X1 = 35;
            line4.Y1 = rnd.Next(10, 40);
            line4.X2 = 135;
            line4.Y2 = rnd.Next(10, 40);

            line5.X1 = 45;
            line5.Y1 = rnd.Next(10, 40);
            line5.X2 = 145;
            line5.Y2 = rnd.Next(10, 40);

            line6.X1 = 55;
            line6.Y1 = rnd.Next(10, 40);
            line6.X2 = 155;
            line6.Y2 = rnd.Next(10, 40);

            line7.X1 = 65;
            line7.Y1 = rnd.Next(10, 40);
            line7.X2 = 165;
            line7.Y2 = rnd.Next(10, 40);

            line8.X1 = 75;
            line8.Y1 = rnd.Next(10, 40);
            line8.X2 = 175;
            line8.Y2 = rnd.Next(10, 40);

            line9.X1 = 85;
            line9.Y1 = rnd.Next(10, 40);
            line9.X2 = 185;
            line9.Y2 = rnd.Next(10, 40);

            line10.X1 = 95;
            line10.Y1 = rnd.Next(10, 40);
            line10.X2 = 195;
            line10.Y2 = rnd.Next(10, 40);
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            stpanel.IsEnabled = true;
        }

        private void EscClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GuestClick(object sender, RoutedEventArgs e)
        {
            Data.Login = true;
            Data.UserSurname = "Гость";
            Data.Right = "Пользователь";
            Data.UserPatronymic = "";
            Close();
        }

        private void EnterCLick(object sender, RoutedEventArgs e)
        {
using (Pr22Context _db = new Pr22Context())
            {
                var user = _db.Users.Where(user => user.Login == tbLogin.Text &&
                user.Password == tbPas.Password);
                if (user.Count() == 1 && Captcha.Text == tbCaptcha.Text)
                {
                    Data.Login = true;
                    Data.UserSurname = user.First().Surname;
                    Data.UserPatronymic = user.First().Patronymic;
                    _db.Roles.Load();
                    Data.Right = user.First().IdroleNavigation.Role1;
                    Close();
                }
                else
                {
                    if (user.Count() == 1)
                    {
                        MessageBox.Show("Повторите ввод капчи");
                    }
                    else
                    {
                        MessageBox.Show("Логин или пароль неверны. Повторите ввод");
                    }
                    GetCaptcha();
                    if (_countLogin >= 2)
                    {
                        stpanel.IsEnabled = false;
                        _timer.Start();
                    }
                    _countLogin++;
                    tbLogin.Focus();
                }
            }
        }
    }
}
