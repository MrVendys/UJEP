using Microsoft.EntityFrameworkCore;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Canban.View.Windows;
using System.Threading.Tasks;
namespace Canban.DB.Models
{
    class DatabaseContext : DbContext
    {
        public DbSet<TaskModel> Tasks { set; get; }
        public DbSet<StatusModel> Statuses { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<BoardModel> Boards { get; set; }
        public DbSet<ColumnModel> Columns { get; set; }
        public DbSet<TaskHistoryModel> TaskHistories { get; set; }
        TaskHistory history;
        UserModel loggedUser;
        public UserModel LoggedUser { 
            get { return loggedUser; } 
            set { loggedUser = value; } 
        }

        private string DbPath { get; set; }
        public DatabaseContext()
        {
            SetContext();
        }
        private void SetContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "blogging.db");
            //history = TaskHistory.GetInstance();
            //history.Show();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
        public void UpdateTasks(int id, object updatedValues)
        {
            Users.Load();
            Tasks.Load();
            // Find the entity in the database
            var entity = Tasks.Find(id);
            var oldEntity = Tasks.Find(id);

            if (entity != null)
            {
                // Get all properties of the updatedValues object (DTO or anonymous object)
                var properties = updatedValues.GetType().GetProperties();

                foreach (var prop in properties)
                {
                    if(prop.Name == "Users")
                    {
                        /*foreach (var user in prop.GetValue(prop.Name))
                        {

                            UserModel userModel = Users.Where(x => x.Name == user).First();
                            //db.Users.Find(userModel.Id).TaskModels.Add(newTaskModel);
                            if (userModel != null && !task.Users.Any(c => c.Id == userModel.Id))
                            {
                                task.Users.Add(userModel);

                            }
                            db.SaveChanges();
                            if (task != null && !userModel.TaskModels.Any(c => c.Id == task.Id))
                            {
                                userModel.TaskModels.Add(task);
                            }

                        }
                        */
                    }
                    // Get the value of the current property
                    var value = prop.GetValue(updatedValues);

                    // Only update the entity's property if the value is not null
                    if (value != null)
                    {
                        // Find the matching property in the entity
                        var entityProp = entity.GetType().GetProperty(prop.Name);
                        if (entityProp != null && entityProp.CanWrite)
                        {
                            // Update the value of the entity's property
                            entityProp.SetValue(entity, value);
                        }
                    }
                }
                //history.LoadCollectionData(loggedUser, oldEntity, entity, DateTime.Now);


                ChangeTracker.DetectChanges();
                Console.WriteLine(ChangeTracker.DebugView.LongView);
                SaveChanges();
            }


        }
    }
}
