using Canban.DB.Models;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Security.Policy;
using System.Windows;
using System.Windows.Media.Media3D.Converters;

namespace Canban.View.Windows
{
    /// <summary>
    /// Interakční logika pro TaskHistory.xaml
    /// </summary>

    public class Data
    {
        public object PropertyName { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }
    }

     public sealed partial class TaskHistory : Window
     {
         private static TaskHistory _instance;

         public ObservableCollection<Data> changes = new ObservableCollection<Data>();
        public ObservableCollection<Data> Changes { get { return changes; } }

        private List<string> ShowProperties = new List<string> { 
            "OldName", "OldDesc", "OldDeadline", "OldStarted", "OldCompleted",
            "NewName", "NewDesc", "NewDeadline", "NewStarted", "NewCompleted"
        };
         List<string> propertyList;
         private DatabaseContext db;

         private TaskHistory()
         {
             InitializeComponent();
            db = new DatabaseContext();
            DataContext = changes;
            contextDataGrid.ItemsSource = Changes;
        }
         public static TaskHistory GetInstance()
         {
             if (_instance == null)
             {
                 _instance = new TaskHistory();
             }
             return _instance;
         }
         public void LoadCollectionData()
         {
            db = new DatabaseContext();
            db.TaskHistories.Load();
            db.Tasks.Load();
            var taskHistories = db.TaskHistories;

            Changes.Clear();

            foreach (var history in taskHistories)
            {
                var properties = history.GetType().GetProperties();
                foreach (var prop in properties)
                {
                    if (prop.GetValue(history) != null && ShowProperties.Contains(prop.Name))
                    {
                        var value = prop.GetValue(history);
                        changes.Add(new Data
                        {
                            PropertyName = prop.Name.ToString(),
                            NewValue = value,
                            OldValue = 
                        });
                    }
                    // Get the value of the current property


                }
            }

        }
        private void LoadData()
        {

        }
        public void CreateData(UserModel loggedUser, TaskModel oldTask, TaskModel newTask, DateTime timeOfUpdate)
        {
            
            db.TaskHistories.Load();
            db.Tasks.Load();
            propertyList = new List<string>();
            List<object> newPropertyValue = new List<object>();
            List<object> oldPropertyValue = new List<object>();

            var entity = oldTask;
            var properties = newTask.GetType().GetProperties();
            foreach (var prop in properties)
            {
                if(prop.Name != "Users" && prop.Name != "LazyLoader")
                {
                    var value = prop.GetValue(newTask);
                    var value2 = prop.GetValue(oldTask);
                    if (!Equals(prop.GetValue(newTask), prop.GetValue(oldTask)))
                    {
                        propertyList.Add(prop.Name);
                        newPropertyValue.Add(value);
                        oldPropertyValue.Add(value2);

                    }
                }
                   // Get the value of the current property
                   
                  
            }
   
            db.Users.Load();
            if(oldPropertyValue[propertyList.IndexOf("Desc")] == null)
            {
                oldPropertyValue[propertyList.IndexOf("Desc")] = "";
            }
            if (newPropertyValue[propertyList.IndexOf("Desc")] == null)
            {
                newPropertyValue[propertyList.IndexOf("Desc")] = "";
            }

            var logUser = db.Users.Find(loggedUser.Id);
            TaskHistoryModel taskHistoryModel = new TaskHistoryModel()
            {
                OldName = propertyList.Contains("Name") ? oldPropertyValue[propertyList.IndexOf("Name")].ToString() : null,
                NewName = propertyList.Contains("Name") ? newPropertyValue[propertyList.IndexOf("Name")].ToString() : null,
                OldDesc = propertyList.Contains("Desc") ? oldPropertyValue[propertyList.IndexOf("Desc")].ToString() : null,
                NewDesc = propertyList.Contains("Desc") ? newPropertyValue[propertyList.IndexOf("Desc")].ToString() : null,
                OldColumnId = propertyList.Contains("ColumnId") ? (int)oldPropertyValue[propertyList.IndexOf("ColumnId")] : null,
                NewColumnId = propertyList.Contains("ColumnId") ? (int)newPropertyValue[propertyList.IndexOf("ColumnId")] : null,
                MoveAt = DateTime.Now,
                MoveBy = logUser.Name,
            };
            //db.ChangeTracker.Clear();
            db.Add(taskHistoryModel);
            db.SaveChanges();

            db.ChangeTracker.Clear();
            LoadCollectionData();
        }
        private void CreateHistoryUI()
        {
            DataContext = this;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
    }
    
}
