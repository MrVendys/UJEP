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
        public BoardModel boardModel;
        private string BoardName = "Kanban";
        public string boardName
        {
            get { return BoardName; }
            set
            {
                BoardName = value;
            }
        }
        public BoardControl(UserModel loggedUser)
        {
            InitializeComponent();
            db = new DatabaseContext();
            db.Boards.Load();
            db.Users.Load();
            db.Boards.RemoveRange(db.Boards);
            db.SaveChanges();
            var user = db.Users.Where(x=>x.Id == loggedUser.Id).FirstOrDefault();
            boardModel = new BoardModel()
            {
                Name = "Kanban",
                UserId = user.Id,
                CreatedBy = user
            };
           
            db.Add(boardModel);
            db.SaveChanges();
        }

    }
}
