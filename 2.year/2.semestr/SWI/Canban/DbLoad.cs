using Canban.DB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
                db.Columns.RemoveRange(db.Columns);
                db.Tasks.RemoveRange(db.Tasks);
                db.Statuses.RemoveRange(db.Statuses);
                db.Users.RemoveRange(db.Users);
                db.Statuses.Add(new StatusModel()
                {
                    Name = "To Do",
                    Color = "Blue"
                }
                );
                db.Statuses.Add(new StatusModel()
                {
                    Name = "In Progress",
                    Color = "Yellow"
                }
                );
                db.Statuses.Add(new StatusModel()
                {
                    Name = "Done",
                    Color = "Green"
                }
                );
                db.Users.Add(new UserModel()
                {
                    Name = "Venca",
                    Email = "venca@gmail.com",
                    Password = "Password",
                });
                db.SaveChanges();
            
        }
    }
}
   
