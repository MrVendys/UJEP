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
        private int dbId = 0;
        public UserModel loggedUser;
        TaskHistory history;
        public BoardWindow(int id, UserModel loggedUser)
        {
            this.loggedUser = loggedUser;
            
            dbId = id;
            db = new DatabaseContext();
            InitializeComponent();
            LoadComponents();
        }
        /// <summary>
        /// Nacteni sloupcu
        /// </summary>
        private void LoadComponents()
        {
            db.Columns.Load();
            db.Boards.Load();
            if (db.Columns.Where(x=>x.BoardId == dbId).Any())
            {
                foreach (var column in db.Columns.Where(x=>x.BoardId == dbId))
                {
                    CreateColumnControlUI(column);
                }
            }
        }

        /// <summary>
        /// Vytvoreni UserControlu pro sloupec -> ColumnControl
        /// </summary>
        /// <param name="name"></param>
        private void CreateColumnControlUI(ColumnModel columnModel)
        {
            ColumnControl columnControl = new ColumnControl(columnModel, loggedUser);
            MainGrid.Children.Add(columnControl);

        }
        /// <summary>
        /// Vytvoreni databazoveho modelu sloupce 
        /// a zavolani funkce pro vytvoreni UserControlu
        /// </summary>
        /// <param name="columnName">Nazev sloupce</param>
        private void CreateColumn(string columnName, string columnColor)
        {
            db.Tasks.Load();
            db.Columns.Load();
            ColumnModel column = new ColumnModel()
            {
                Name = columnName,
                Color = columnColor,
                BoardId = this.dbId,
                Board = db.Boards.Where(x => x.Id == this.dbId).First()
            };
            db.Add(column);
            db.SaveChanges();
            db.ChangeTracker.Clear();
            CreateColumnControlUI(column);
        }
        /// <summary>
        /// Funkce volana tlacitkem v menu pro odhlaseni
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
        /// <summary>
        /// Funkce volana tlaciktem na vytvoreni sloupce
        /// </summary>
        /// <param name="sender">Tlacitko na BoardWindow</param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new DialogWindow();
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                if (!dialog.inputName.IsNullOrEmpty())
                {
                    CreateColumn(dialog.inputName, dialog.colorName);
                }
            }
            
        }

        private void HistoryItem_Click(object sender, RoutedEventArgs e)
        {
            if(this.history == null)
                history = TaskHistory.GetInstance();
            if (!history.IsActive)
                history.Show();
            history.LoadCollectionData();
        }
    }
}

