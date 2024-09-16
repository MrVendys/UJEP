using Canban.DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.VisualBasic.ApplicationServices;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Canban.View.Windows
{
    /// <summary>
    /// Interakční logika pro TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        public ObservableCollection<string> AvailableItems { get; set; }
        public ObservableCollection<string> SelectedItems { get; set; }
        public string SelectedItem { get; set; }
        private string taskName = "Novy Task";
        public string TaskName
        {
            get { return taskName; }
            set { taskName = value; }
        }
        DatabaseContext db = new DatabaseContext();
        private int taskId;
        TaskHistory thistory;
        UserModel loggedUser;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newTask">TaskControl, which was doubleclicked on</param>
        public TaskWindow(int taskId, UserModel loggedUser)
        {
            this.taskId = taskId;
            this.loggedUser = loggedUser;
            db.Users.Load();
            db.Tasks.Load();

            thistory = TaskHistory.GetInstance();

            // Initialize the available items for the ComboBox
            AvailableItems = new ObservableCollection<string>();
            foreach (var x in db.Users)
            {
                AvailableItems.Add(x.Name);
            }

            // Initialize the collection for selected items
            SelectedItems = new ObservableCollection<string>();
           
            db.ChangeTracker.Clear();
            

            InitializeComponent();
            FlowDocument myFlowDoc = new FlowDocument(new Paragraph(new Run("")));
            DescRTextBox.Document = myFlowDoc;
            this.DataContext = this;
        }


        private void StatusCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }


        private void AssigneeComboBox_DropDownClosed(object sender, EventArgs e)
        {
            /*if (AssigneeComboBox.SelectedItem != null)
            {
                /*
                if (!AssigneeListBox.Items.Contains(AssigneeComboBox.SelectedItem))
                    AssigneeListBox.Items.Add(AssigneeComboBox.SelectedItem.ToString());
                else
                    AssigneeListBox.Items.Remove(AssigneeComboBox.SelectedItem.ToString());
            
                if (!ItemsControl.Contains(AssigneeComboBox.SelectedItem.ToString()))
                {
                    ItemsControl.Add(AssigneeComboBox.SelectedItem.ToString());
                }
                else
                {
                    ItemsControl.Remove(AssigneeComboBox.SelectedItem.ToString());
                }
            }
            // Add the selected item to the SelectedItems collection
            */
        }
        /// <summary>
        /// Adding selected item from combo box to Bind list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssigneeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedItem != null && !SelectedItems.Contains(SelectedItem))
            {
                SelectedItems.Add(SelectedItem);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            TaskName = TitleTextbox.Text;
            string desc = new TextRange(DescRTextBox.Document.ContentStart, DescRTextBox.Document.ContentEnd).Text;
            UpdateTasks(taskId, new
            {
                Name = TitleTextbox.Text,
                Started = StartedDatePicker.SelectedDate,
                Deadline = DeadlineDatePicker.SelectedDate,
                Completed = EndedDatePicker.SelectedDate,
                Desc = desc == "\r\n" ? null : desc,
            });
            db.SaveChanges();
            
            
        }
        private void UpdateTasks(int id, object updatedValues)
        {
            db.Users.Load();
            db.Tasks.Load();
            var oldTask = db.Tasks.Find(id);
            db.ChangeTracker.Clear();

            db.Users.Load();
            db.Tasks.Load();
            var task = db.Tasks.Find(id);
            
            foreach (var user in SelectedItems)
            {
                UserModel userModel = db.Users.Where(x => x.Name == user).First();
                //db.Users.Find(userModel.Id).TaskModels.Add(newTaskModel);
                if (userModel != null && !task.Users.Any(c => c.Id == userModel.Id))
                {
                    task.Users.Add(userModel);

                }
                if (task != null && !userModel.TaskModels.Any(c => c.Id == task.Id))
                {
                    userModel.TaskModels.Add(task);
                }

            }
            db.SaveChanges();
            if (task != null)
            {
               db.Entry(task).CurrentValues.SetValues(updatedValues);
               db.SaveChanges();
               thistory.CreateData(loggedUser, oldTask, db.Tasks.Find(id),DateTime.Now);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db.Tasks.Load();
            var task = db.Tasks.Include(x => x.Users).FirstOrDefault(x => x.Id == taskId);
            
            //StatusCombobox.SelectedValue = task.StatusModel != null ? task.StatusModel.Name : null;
            if (task.Users != null) {
                foreach (var user in task.Users)
                {
                    SelectedItems.Add(user.Name);
                }
            }
            TaskName = task.Name;
            TitleTextbox.Text = TaskName;
            StartedDatePicker.SelectedDate = task.Started;
            DeadlineDatePicker.SelectedDate = task.Deadline;
            EndedDatePicker.SelectedDate = task.Completed;
            FlowDocument myFlowDoc = new FlowDocument(new Paragraph(new Run(task.Desc)));
            DescRTextBox.Document = myFlowDoc;
            db.ChangeTracker.Clear();
        }
    }
}
