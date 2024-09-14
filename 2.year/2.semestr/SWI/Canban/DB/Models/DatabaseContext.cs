using Microsoft.EntityFrameworkCore;
using Canban.DB.Models;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace Canban.DB.Models
{
    class DatabaseContext : DbContext
    {
        public DbSet<TaskModel> Tasks { set; get; }
        public DbSet<StatusModel> Statuses { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<BoardModel> Boards { get; set; }
        public DbSet<ColumnModel> Columns { get; set; }

        public string DbPath { get; }
        public DatabaseContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "blogging.db");

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
        public void UpdateTasks(int id, object updatedValues)
        {
            /* var task = Tasks.Where(x => x.Id == Id).FirstOrDefault();
             task.Name = Name;
             var status = Statuses.Where(x => x.Name == StatusName).FirstOrDefault();
             task.StatusId = status.Id;
             task.StatusModel = status;
             task.Started = Started;
             task.Deadline = Deadline;
             task.Desc = Desc;
             task.ColumnId = ColumnId != null ? ColumnId : ;
             SaveChanges();
            */
            // Find the entity in the database
            var entity = Tasks.Find(id);

            if (entity != null)
            {
                // Get all properties of the updatedValues object (DTO or anonymous object)
                var properties = updatedValues.GetType().GetProperties();

                foreach (var prop in properties)
                {
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
                ChangeTracker.DetectChanges();
                Console.WriteLine(ChangeTracker.DebugView.LongView);
                SaveChanges();
            }


        }
    }
}
