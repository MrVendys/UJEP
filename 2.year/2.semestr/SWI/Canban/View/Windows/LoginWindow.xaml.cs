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
    /// Interakční logika pro LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        DatabaseContext db;
        public LoginWindow(UserModel newUser)
        {
            

            InitializeComponent();
            db = new DatabaseContext();
            db.Boards.Load();
            LogIn(newUser);
        }
        public LoginWindow()
        {
            InitializeComponent();
            db = new DatabaseContext();
            db.Boards.Load();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            CheckUser();
        }

        private void SigInButton_Click(object sender, RoutedEventArgs e)
        {
            SiginWindow sigin = new SiginWindow();
            sigin.Show();
            this.Hide();
        }
        private void CheckUser()
        {

            db = new DatabaseContext();
            db.Users.Load();
            var users = db.Users.Where(x=>x.Email == EmailInput.Text).ToList();
            db.Dispose();
            foreach (var user in users)
            {
                if(user.Password == PasswordInput.Text)
                {

                    LogIn(user);
                   
                    return;
                }

            }
            MessageBox.Show("Žádný uživatel nebyl nalezen. Zkontrolujte přihlašovací údaje");

           
        }
        private void LogIn(UserModel loggedUser) {
            
            MainWindow main = new MainWindow(loggedUser);
            main.Show();
            this.Close();
        }
    }
}
