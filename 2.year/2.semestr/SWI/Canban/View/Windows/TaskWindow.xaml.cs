using Canban.DB.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
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
        DatabaseContext db = new DatabaseContext();
        TaskModel newTaskModel;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newTask">TaskControl, which was doubleclicked on</param>
        public TaskWindow(TaskModel newTaskModel)
        {
            this.newTaskModel = newTaskModel;
            db.Users.Load();
            db.Tasks.Load();



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
            //TODO: Check null values
            var task = db.Tasks.Include(x => x.Users).FirstOrDefault(x => x.Id == newTaskModel.Id);
       
            task.Name = TitleTextbox.Text;
            foreach (var user in SelectedItems)
            {

                    UserModel userModel = db.Users.Where(x => x.Name == user).First();
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
            task.Started = StartedDatePicker.SelectedDate;
                task.Deadline = DeadlineDatePicker.SelectedDate;
                task.Desc = new TextRange(DescRTextBox.Document.ContentStart, DescRTextBox.Document.ContentEnd).Text;
            db.SaveChanges();
            /*List<UserModel> users = new List<UserModel>();
            foreach (var user in SelectedItems)
            {
                UserModel userModel = db.Users.Where(x => x.Name == user).First();
                db.Users.Find(userModel.Id).TaskModels.Add(newTaskModel);
                db.ChangeTracker.Clear();
                db.SaveChanges();
                users.Add(userModel);
            }
            db.UpdateTasks(newTaskModel.Id, new { 
                    Name = TitleTextbox.Text, 
                    Users = users, 
                    Started = StartedDatePicker.SelectedDate, 
                    Deadline = DeadlineDatePicker.SelectedDate,
                    Desc = new TextRange(DescRTextBox.Document.ContentStart, DescRTextBox.Document.ContentEnd).Text
            });
                
            */
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var task = db.Tasks.Include(x => x.Users).FirstOrDefault(x => x.Id == newTaskModel.Id);
            TitleTextbox.Text = task.Name;
            //StatusCombobox.SelectedValue = task.StatusModel != null ? task.StatusModel.Name : null;
            if (task.Users != null) {
                foreach (var user in task.Users)
                {
                    SelectedItems.Add(user.Name);
                }
            }
            
            StartedDatePicker.SelectedDate = task.Started;
            DeadlineDatePicker.SelectedDate = task.Deadline;
            FlowDocument myFlowDoc = new FlowDocument(new Paragraph(new Run(task.Desc)));
            DescRTextBox.Document = myFlowDoc;
            db.ChangeTracker.Clear();
        }
    }
}
