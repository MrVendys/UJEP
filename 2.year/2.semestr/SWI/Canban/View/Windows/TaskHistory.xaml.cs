using Canban.DB.Models;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;

namespace Canban.View.Windows
{
    /// <summary>
    /// Interakční logika pro TaskHistory.xaml
    /// </summary>



     public sealed partial class TaskHistory : Window
     {
         private static TaskHistory _instance;

         private List<TaskHistoryModel> authors = new List<TaskHistoryModel>();
         private DatabaseContext db;

         private TaskHistory()
         {
             InitializeComponent();
         }
         public static TaskHistory GetInstance()
         {
             if (_instance == null)
             {
                 _instance = new TaskHistory();
             }
             return _instance;
         }
         public void LoadCollectionData(UserModel loggedUser, TaskModel oldTask, TaskModel newTask, DateTime timeOfUpdate)
         {

             db = new DatabaseContext();
             db.Users.Load();
             db.Tasks.Load();
            List<string> propertyList = new List<string>();
            List<object> newPropertyValue = new List<object>();
            List<object> oldPropertyValie = new List<object>();

            var entity = oldTask;
            var properties = newTask.GetType().GetProperties();
            foreach (var prop in properties)
            {
                // Get the value of the current property
                var value = prop.GetValue(newTask);

                // Only update the entity's property if the value is not null
                if (value != null)
                {
                    var entityProp = entity.GetType().GetProperty(prop.Name);
                    // Find the matching property in the entity
                    if (entityProp != null && entityProp.GetValue(prop.Name) != value)
                    {
                        // Update the value of the entity's property
                        propertyList.Add(entityProp.Name.ToString());
                        newPropertyValue.Add(value);
                        oldPropertyValie.Add(entityProp.GetValue(prop.Name));
                    }

                }
            }



            DataContext = this;
             TaskHistoryModel taskHistoryModel = new TaskHistoryModel()
             {
                 OldTaskInfo = oldTask,
                 OldTaskInfoId = oldTask.Id,
                 NewTaskInfo = newTask,
                 NewTaskInfoId = newTask.Id,
                 MoveAt = timeOfUpdate,
                 MoveBy = loggedUser,
                 MoveById = oldTask.Id,
             };
             authors.Add(taskHistoryModel);

             db.SaveChanges();
             db.ChangeTracker.Clear();
            DataContext = this;
            TaskDataGrid.ItemsSource = authors;
         }

     }
    
}
