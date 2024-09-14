using Canban.DB.Models;
using Canban.View.UserControls;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
    /// Interakční logika pro BoardWindow.xaml
    /// </summary>
    public partial class BoardWindow : Window
    {
        private DatabaseContext db;
        private int Id = 0;
        public BoardWindow(int id)
        {

            Id = id;
            db = new DatabaseContext();
            InitializeComponent();
            LoadComponents();
        }
       
        private void LoadComponents()
        {
            db.Columns.Load();
            db.Boards.Load();
            if (db.Columns.Where(x=>x.BoardId == Id).Any())
            {
                foreach (var column in db.Columns.Where(x=>x.BoardId == Id))
                {
                    LoadColumn(column.Id);
                }
            }
        }
        private void CreateColumnControlUI(string name)
        {
           
            TasksGrid tg = new TasksGrid(this.Id, name);
            MainGrid.Children.Insert(0, tg);

        }
        private void LoadColumn(int id)
        {
            TasksGrid tg = new TasksGrid(id);
            MainGrid.Children.Insert(0, tg);
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new DialogWindow();
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                if (!dialog.NameTextBox.Text.IsNullOrEmpty())
                {
                    CreateColumnControlUI(dialog.boardName);
                }
            }
            
        }
        
    }
}

