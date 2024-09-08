using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Canban.View.UserControls
{
    /// <summary>
    /// Interakční logika pro TasksGrid.xaml
    /// </summary>
    public partial class TasksGrid : UserControl
    {
        public TasksGrid()
        {
            InitializeComponent();
        }

        private void AddTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateTask();
        }
        private void UserControl_DeleteRequested(object sender, EventArgs e)
        {
            var userControl = sender as TaskControl;
            if (userControl != null)
            {
                TaskStackPanel.Children.Remove(userControl);
            }
        }

        public void CreateTask()
        {
            TaskControl task = new TaskControl();
            task.DeleteRequested += UserControl_DeleteRequested;
            task.MouseMove += UserControl_MouseMove;
            task.MouseDown += UserControl_MouseDown;
            task.StackPanel = TaskStackPanel;
            TaskStackPanel.Children.Add(task);
        }

        private void TaskStackPanel_Drop(object sender, DragEventArgs e)
        {
            var sourceUserControl = e.Data.GetData(typeof(TaskControl)) as TaskControl;
            if (sourceUserControl != null)
            {
                var stackPanel = sender as StackPanel;


                // Remove from the source and add to the target

                sourceUserControl.StackPanel.Children.Remove(sourceUserControl as TaskControl);
                stackPanel.Children.Add(sourceUserControl as TaskControl);
                sourceUserControl.StackPanel = TaskStackPanel;
            }
            /*var data = e.Data.GetData("DraggedUserControlData") as DraggedTaskControlData;
            if (data != null)
            {
                var stackPanel = sender as StackPanel;


                // Remove from the source and add to the target

                data.Control.StackPanel.Children.Remove(data.Control as TaskControl);
                stackPanel.Children.Add(data.Control as TaskControl);
                data.Control.StackPanel = TaskStackPanel;
                var textBox = data.Control.FindName("NameTextBox") as TextBox;
                if (textBox != null)
                {
                    textBox.Text = data.TextBoxContent;
                }
            }
            */
        }

        private Point _startPoint;

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(null);
        }
        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var currentPosition = e.GetPosition(null);
                Vector diff = _startPoint - currentPosition;
                bool a = Math.Abs(diff.X) > 1;
                // Zajistíme, že uživatel posunul myš alespoň o určitou vzdálenost
                if (Math.Abs(diff.X) < SystemParameters.MinimumHorizontalDragDistance + 100)
                {
                    var userControl = sender as UserControl;

                    if (userControl != null)
                    {
                        Dispatcher.BeginInvoke(() =>
                        {
                            
                             DragDrop.DoDragDrop(userControl, userControl, DragDropEffects.Move);
                            
                        });
                    }
                }
            }
        }

        private void TaskStackPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                TaskControl taskControl = sender as TaskControl;
               
                
            }
        }

        private void UserControl_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var userControl = sender as TaskControl;
                if (userControl != null)
                {
                    DragDrop.DoDragDrop(userControl, userControl, DragDropEffects.Move);
                }
            }
        }
    }
}
