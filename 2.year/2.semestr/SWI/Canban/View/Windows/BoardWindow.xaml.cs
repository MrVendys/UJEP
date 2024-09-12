using Canban.DB.Models;
using Canban.View.UserControls;
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
            //LoadComponents();
        }
       
        private void LoadComponents()
        {
            for (int i = 0; i < 4; i++)
            {
                TasksGrid tg = new TasksGrid(i);
                Grid.SetColumn(tg, i);
                Grid.SetRow(tg, 2);
                MainGrid.Children.Add(tg);
                
            }
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            db.Columns.Add(new ColumnModel()
            {
                Name = "Novy sloupec",
                BoardId = Id,
                Board = db.Boards.Where(x => x.Id == Id).First()

            });
            db.SaveChanges();
            var columns = db.Columns.OrderBy(i=>i.Id);
            int id = 0;
            if (columns.Any())
                id = columns.Last().Id + 1;
            TasksGrid tg = new TasksGrid(id);
            MainGrid.Children.Insert(0, tg);
        }
    }
}

