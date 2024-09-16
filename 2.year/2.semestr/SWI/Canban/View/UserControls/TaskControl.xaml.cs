using Canban.DB.Models;
using Canban.View.Windows;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Canban.View.UserControls
{
    /// <summary>
    /// Interakční logika pro TaskControl.xaml
    /// </summary>
    public partial class TaskControl : UserControl
    {
        public event EventHandler<EventArgs> DeleteRequested;

        public StackPanel StackPanel;
        public TaskModel taskModel;
        private DatabaseContext db = new DatabaseContext();

        private string taskName = "Novy Task";
        public string TaskName { 
            get { return taskName; } 
            set { taskName = value; } 
        }
        UserModel loggedUser;
        public TaskControl(TaskModel newTaskModel, UserModel loggedUser)
        {
            InitializeComponent();
            this.loggedUser = loggedUser;
            this.taskModel = newTaskModel;
            DataContext = this;
        }
        private void NameTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new TaskWindow(taskModel.Id, loggedUser).ShowDialog();
            db.Tasks.Load();
            var task = db.Tasks.Where(x => x.Id == taskModel.Id).FirstOrDefault();
            TaskName = task.Name;
            NameTextBox.Text = TaskName;
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            DeleteRequested?.Invoke(this, EventArgs.Empty);
        }
   
    }
}
