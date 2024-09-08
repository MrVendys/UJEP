using Canban.View.UserControls;
using System.Windows;
using System.Windows.Controls;

namespace Canban
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //CreateTask();
        }
        /// <summary>
        /// Calling method to create TaskControl
        /// </summary>
        /// <param name="sender">Add Task button</param>
        /// <param name="e"></param>
        /*
        private void AddTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateTask();
        }
        /// <summary>
        /// Deleting TaskControl from Stackpanel
        /// </summary>
        /// <param name="sender">TaskControl that will be deleted</param>
        /// <param name="e"></param>
        private void UserControl_DeleteRequested(object sender, EventArgs e)
        {
            var userControl = sender as TaskControl;
            if (userControl != null)
            {
                TaskStackPanel.Children.Remove(userControl);
            }
        }
        /// <summary>
        /// Creating and adding TaskControl to StackPanel
        /// Adding Delete method to event "DeleteRequested"
        /// </summary>
        /// </summary>
        public void CreateTask()
        {
            TaskControl task = new TaskControl();
            task.DeleteRequested += UserControl_DeleteRequested;
            TaskStackPanel.Children.Add(task);
        }
        */
    }
}