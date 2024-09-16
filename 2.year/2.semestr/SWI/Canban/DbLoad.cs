using Canban.DB.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Windows.Media;

namespace Canban
{
    public class DbLoad
    {
        DatabaseContext db;
        
      public void LoadDB()
        {
                db = new DatabaseContext();
                db.Statuses.Load();
                db.Tasks.Load();
                db.Users.Load();
                db.Columns.Load();
                db.Boards.Load();

                db.Statuses.RemoveRange(db.Statuses);
                db.Tasks.RemoveRange(db.Tasks);
                db.Users.RemoveRange(db.Users);
                db.Columns.RemoveRange(db.Columns);
                db.Boards.RemoveRange(db.Boards);
                db.SaveChanges();

            UserModel userModel = new UserModel()
            {
                Name = "Venca",
                Email = "venca@gmail.com",
                Password = "venca",
            };
            UserModel userModel2 = new UserModel()
            {
                Name = "Bao",
                Email = "Bao@gmail.com",
                Password = "bao",
            };
            db.Users.AddRange(new List<UserModel>{ userModel, userModel2});
            db.SaveChanges();
            BoardModel board = new BoardModel()
            {
                Name = "Kanban",
                CreatedBy = db.Users.Where(x => x.Name == userModel.Name).First(),
                UserId = db.Users.Where(x => x.Name == userModel.Name).First().Id
            };
            db.Boards.Add(board);
            db.SaveChanges();

            db.Columns.Add(new ColumnModel()
            {
                Name = "To Do",
                Board = db.Boards.Where(x => x.Name == board.Name).First(),
                BoardId = db.Boards.Where(x => x.Name == board.Name).First().Id,

            });
            db.SaveChanges();

            db.ChangeTracker.Clear();

        }
    }
}
   
