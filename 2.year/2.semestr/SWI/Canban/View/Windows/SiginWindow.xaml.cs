using Canban.DB.Models;
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

namespace Canban.View.Windows
{
    /// <summary>
    /// Interakční logika pro SiginWindow.xaml
    /// </summary>
    public partial class SiginWindow : Window
    {
        private DatabaseContext db;
        public SiginWindow()
        {
            InitializeComponent();
        }

        private void SigInButton_Click(object sender, RoutedEventArgs e)
        {
           
            SignIn();
        }


        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
        private void SignIn()
        {
            db = new DatabaseContext();
            db.Users.Load();
            
            
            if (!db.Users.Any(u=>u.Email == EmailInput.Text)) {
                if (PasswordInput.Password.Equals(PasswordCheckInput.Password) && PasswordInput.Password.Length >= 8)
                {
                    UserModel userModel = new UserModel
                    {
                        Name = NameInput.Text,
                        Email = EmailInput.Text,
                        Password = PasswordInput.Password.ToString()
                    };
                    db.Add(userModel);
                    db.SaveChanges();

                    MainWindow main = new MainWindow(userModel);
                    main.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hesla se nehodují, nebo je moc krátké (min 8 znaků)");
                }

            }
            else
            {
                MessageBox.Show("Email je již použit");
            }
            
           
        }

        private void ShowPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowPasswordFunction();
            MessageBox.Show("Test");
        }

        private void ShowPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            HidePasswordFunction();
            MessageBox.Show("Test");
        }

        private void ShowPassword_MouseLeave(object sender, MouseEventArgs e)
        {
            HidePasswordFunction();
        }

        private void ShowPasswordFunction()
        {
            MessageBox.Show("Test");
            PasswordUnmask.Visibility = Visibility.Visible;
            PasswordInput.Visibility = Visibility.Hidden;
            PasswordUnmask.Text = PasswordInput.Password;
        }

        private void HidePasswordFunction()
        {
            PasswordUnmask.Visibility = Visibility.Hidden;
            PasswordInput.Visibility = Visibility.Visible;
        }
    }
}
