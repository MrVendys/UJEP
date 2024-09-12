using Canban.DB.Models;
using Canban.View.UserControls;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Canban.View.Windows
{
    /// <summary>
    /// Interakční logika pro TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        public ObservableCollection<string> AvailableItems { get; set; }
        public ObservableCollection<string> SelectedItems { get; set; }
        public ObservableCollection<string> StatusItems { get; set; }
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
            db.Tasks.Load();
            db.Statuses.Load();
            db.Users.Load();



            // Initialize the available items for the ComboBox
            AvailableItems = new ObservableCollection<string>();
            foreach (var x in db.Users)
            {
                AvailableItems.Add(x.Name);
            }

            // Initialize the collection for selected items
            SelectedItems = new ObservableCollection<string>();
            StatusItems = new ObservableCollection<string>();
            foreach (var x in db.Statuses)
            {
                StatusItems.Add(x.Name);
            }

            

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
            /*try
            {
                var task = db.Tasks.Find(newTaskModel.Id);
                task.Name = TitleTextbox.Text;
                var status = db.Statuses.Where(x => x.Name == StatusCombobox.SelectedItem.ToString()).FirstOrDefault();
                //task.StatusId = status.Id;
                //task.StatusModel = status;
                
                task.Started = StartedDatePicker.SelectedDate;
                task.Deadline = DeadlineDatePicker.SelectedDate;
                task.Desc = new TextRange(DescRTextBox.Document.ContentStart, DescRTextBox.Document.ContentEnd).Text;
                db.SaveChanges();
            }
            catch (Exception ex) {
                MessageBox.Show("Něco se pokazilo, výsledek se neuložil");
            }*/
            List<UserModel> users = new List<UserModel>();
            foreach (var user in SelectedItems)
            {
                users.Add(db.Users.Where(x => x.Name == user).First());
            }
            db.UpdateTasks(newTaskModel.Id, new { 
                    Name = TitleTextbox.Text, 
                    Users = users, 
                    Started = StartedDatePicker.SelectedDate, 
                    Deadline = DeadlineDatePicker.SelectedDate,
                    Desc = new TextRange(DescRTextBox.Document.ContentStart, DescRTextBox.Document.ContentEnd).Text
            });
                
            
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var task = db.Tasks.Where(x => x.Id == newTaskModel.Id).FirstOrDefault();
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
        }
    }
}
