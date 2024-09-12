using Canban.DB.Models;
using Canban.View.Windows;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace Canban.View.UserControls
{
    /// <summary>
    /// Interakční logika pro BoardControl.xaml
    /// </summary>
    public partial class BoardControl : UserControl
    {
        DatabaseContext db;
        public BoardWindow boardWindow;
        public BoardControl(UserModel loggedUser)
        {
            InitializeComponent();
            db = new DatabaseContext();
            db.Boards.Load();
            db.Users.Load();
            db.Boards.RemoveRange(db.Boards);
            db.SaveChanges();
            var user = db.Users.Where(x=>x.Id == loggedUser.Id).FirstOrDefault();
            BoardModel boardModel = new BoardModel()
            {
                Name = "Kanban",
                UserId = user.Id,
                CreatedBy = user
            };
           
            db.Add(boardModel);
            db.SaveChanges();
            boardWindow = new BoardWindow(boardModel.Id);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
