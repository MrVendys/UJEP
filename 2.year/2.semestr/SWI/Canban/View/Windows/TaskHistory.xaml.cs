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
        public string ColumnName { get; set; }
        public string TaskName { get; set; }
        public object PropertyName { get; set; }
        public object PropertyValue { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? When {  get; set; } 
    }

     public sealed partial class TaskHistory : Window
     {
         private static TaskHistory _instance;
        List<string> knownPrefixes = new List<string> { "Old", "New" };
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
            LoadCollectionData();
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
            db.Columns.Load();

            var taskHistories = db.TaskHistories;

            foreach (var history in taskHistories)
            {

                var properties = history.GetType().GetProperties();
                
                    foreach (var prop in properties)
                    {
                        if (prop.GetValue(history) != null && propertyList.Contains(prop.Name))
                        {
                            var value = prop.GetValue(history);
                            changes.Add(new Data
                            {
                                ColumnName = db.Columns.Find(db.Tasks.Find(history.TaskId).ColumnId).Name,
                                TaskName = history.OldName,
                                PropertyName = prop.Name.ToString(),
                                PropertyValue = value,
                                ChangedBy = history.MoveBy,
                                When = history.MoveAt
                            });
                        }
                    }
                
                


            }
            
              /*  Changes.Add(new Data
                {
                    Name = "Venca",
                    Value = 1
                });
            */

        }
        public void LoadNew()
        {
            db = new DatabaseContext();
            db.TaskHistories.Load();
            db.Tasks.Load();
            db.Columns.Load();
            var history = db.TaskHistories.OrderBy(x=>x.Id).Last();

            var properties = history.GetType().GetProperties();

            changes.Add(new Data
            {
            });
            foreach (var prop in properties)
            {
                if (prop.GetValue(history) != null && propertyList.Contains(GetRemainingPart(prop.Name, knownPrefixes)))
                {
                    var value = prop.GetValue(history);
                    changes.Add(new Data
                    {
                        ColumnName = db.Columns.Find(db.Tasks.Find(history.TaskId).ColumnId).Name,
                        TaskName = history.OldName,
                        PropertyName = prop.Name.ToString(),
                        PropertyValue = value,
                        ChangedBy = history.MoveBy,
                        When = history.MoveAt
                    });
                }
            }
        }
        static int GetPrefixOrder(string item, List<string> knownPrefixes)
        {
            foreach (var prefix in knownPrefixes)
            {
                if (item.StartsWith(prefix))
                {
                    return knownPrefixes.IndexOf(prefix);
                }
            }
            return knownPrefixes.Count; // Pokud se prefix neshoduje, dát nejvyšší hodnotu (neznámý prefix)
        }

        // Vrátíme zbytek textu po prefixu pro druhotné třídění
        static string GetRemainingPart(string item, List<string> knownPrefixes)
        {
            foreach (var prefix in knownPrefixes)
            {
                if (item.StartsWith(prefix))
                {
                    return item.Substring(prefix.Length); // Vrátíme část řetězce po prefixu (např. "Name", "Desc")
                }
            }
            return item;  // Pokud se prefix neshoduje, vrátíme celý řetězec
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
            if (propertyList.Contains("Desc"))
            {
                if (oldPropertyValue[propertyList.IndexOf("Desc")] == null)
                {
                    oldPropertyValue[propertyList.IndexOf("Desc")] = "";
                }
                if (newPropertyValue[propertyList.IndexOf("Desc")] == null)
                {
                    newPropertyValue[propertyList.IndexOf("Desc")] = "";
                }
            }
            
            if(propertyList != null)
            {
                var logUser = db.Users.Find(loggedUser.Id);
                TaskHistoryModel taskHistoryModel = new TaskHistoryModel()
                {
                    TaskId = newTask.Id,
                    OldName = propertyList.Contains("Name") ? oldPropertyValue[propertyList.IndexOf("Name")].ToString() : null,
                    NewName = propertyList.Contains("Name") ? newPropertyValue[propertyList.IndexOf("Name")].ToString() : null,
                    OldDesc = propertyList.Contains("Desc") ? oldPropertyValue[propertyList.IndexOf("Desc")].ToString() : null,
                    NewDesc = propertyList.Contains("Desc") ? newPropertyValue[propertyList.IndexOf("Desc")].ToString() : null,
                    OldDeadline = propertyList.Contains("Deadline") ? (DateTime)oldPropertyValue[propertyList.IndexOf("Deadline")] : oldTask.Deadline,
                    NewDeadline = propertyList.Contains("Deadline") ? (DateTime)newPropertyValue[propertyList.IndexOf("Deadline")] : oldTask.Deadline,
                    OldColumnId = propertyList.Contains("ColumnId") ? (int)oldPropertyValue[propertyList.IndexOf("ColumnId")] : null,
                    NewColumnId = propertyList.Contains("ColumnId") ? (int)newPropertyValue[propertyList.IndexOf("ColumnId")] : null,
                    MoveAt = DateTime.Now,
                    MoveBy = logUser.Name,
                };
                //db.ChangeTracker.Clear();
                db.Add(taskHistoryModel);
                db.SaveChanges();

                db.ChangeTracker.Clear();
                LoadNew();
            }
            
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
