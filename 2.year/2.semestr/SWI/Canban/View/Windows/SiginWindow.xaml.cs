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
        public SiginWindow()
        {
            InitializeComponent();
        }

        private void SigInButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseContext db = new DatabaseContext();
            UserModel userModel = new UserModel
            {
                Name = NameInput.Text,
                Email = EmailInput.Text,
                Password = PasswordInput.Password.ToString()
            };
            db.Add(userModel);
            db.SaveChanges();
            //MainWindow main = new MainWindow();
            //main.Show();
            this.Close();
        }


        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
