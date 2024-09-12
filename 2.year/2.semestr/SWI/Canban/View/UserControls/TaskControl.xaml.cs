using Canban.DB.Models;
using Canban.View.Windows;
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
        public TaskControl(TaskModel newTaskModel)
        {
            InitializeComponent();
            this.taskModel = newTaskModel;
        }
        private void NameTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new TaskWindow(taskModel).ShowDialog();
            var task = db.Tasks.Where(x => x.Id == taskModel.Id).FirstOrDefault();
            NameTextBox.Text = task.Name;
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            DeleteRequested?.Invoke(this, EventArgs.Empty);
        }
        public void Save()
        {
            var task = db.Tasks.Find(taskModel.Id);
            db.Entry(task).CurrentValues.SetValues(taskModel);
        }
   
    }
}
