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
        private DatabaseContext db;
        public LoginWindow()
        {
            InitializeComponent();
            db = new DatabaseContext();
        }

        /// <summary>
        /// Kliknutí na "Log In" tlačítko
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            CheckUser();
        }

        /// <summary>
        /// Kliknutí na "Sig In" tlačítko
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SigInButton_Click(object sender, RoutedEventArgs e)
        {
            SiginWindow sigin = new SiginWindow();
            sigin.Show();
            this.Hide();
        }

        /// <summary>
        /// Kontrola správnosti dat a uložení uživatele
        /// </summary>
        private void CheckUser()
        {
            db.Users.Load();
            var users = db.Users.Where(x=>x.Email == EmailInput.Text).ToList();
            
            foreach (var user in users)
            {
                if(user.Password == PasswordInput.Password)
                {
                    db.ChangeTracker.Clear();
                    LogIn(user);
                    return;
                }

            }
            MessageBox.Show("Žádný uživatel nebyl nalezen. Zkontrolujte přihlašovací údaje");
   
        }

        /// <summary>
        /// Otevření aplikace po přihlášení
        /// </summary>
        /// <param name="loggedUser">UserModel přihlášeného uživatele</param>
        private void LogIn(UserModel loggedUser) {
            
            db.LoggedUser = loggedUser;
            MainWindow main = new MainWindow(loggedUser);
            main.Show();
            this.Close();
        }
    }
}
