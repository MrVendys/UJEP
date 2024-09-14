using Canban.DB.Models;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;

namespace Canban.View.UserControls
{
    /// <summary>
    /// Interakční logika pro TasksGrid.xaml
    /// </summary>
    public partial class TasksGrid : UserControl
    {
        DatabaseContext db = new DatabaseContext();
        public event PropertyChangedEventHandler? PropertyChanged;
        private string columnName = "Column";
        public string ColumnName
        {
            get { return columnName; }
            set
            {
                columnName = value;
                OnPropertyChanged("ColumnName");
            }
        }
        public int id { get { return ID; } set { ID = value; } }
        private int ID;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public TasksGrid(int boardId, string name)
        {
            columnName = name;
            db.Tasks.Load();
            db.Columns.Load();
            ColumnModel column = new ColumnModel()
            {
                Name = columnName,
                BoardId = boardId,
                Board = db.Boards.Where(x => x.Id == boardId).First()

            };
            db.Add(column);
            db.SaveChanges();
            this.id = column.Id;
            db.ChangeTracker.Clear();
            InitializeComponent();
           
            DataContext = this;
            LoadComponents();
        }
        public TasksGrid(int id)
        {
            columnName = db.Columns.Find(id).Name;
            this.id=id;
            InitializeComponent();

            DataContext = this;
            LoadComponents();
        }

        private void LoadComponents()
        {
            foreach (var task in db.Tasks.Where(x => x.ColumnId == id)) {
                CreateTaskUI(task);
            }
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
                db.Tasks.Remove(db.Tasks.Where(x => x.Id == userControl.taskModel.Id).FirstOrDefault());
                db.SaveChanges();
                TaskStackPanel.Children.Remove(userControl);
            }
        }

        private void CreateTask()
        {
            TaskModel taskModel = new TaskModel()
            {
                Name = "Novy task",
                ColumnId = id
            };
            db.Add(taskModel);
            db.SaveChanges();
            db.ChangeTracker.Clear();
            CreateTaskUI(taskModel);
        }
        private void CreateTaskUI(TaskModel taskModel)
        {
            TaskControl task = new TaskControl(taskModel);
            task.NameTextBox.Text = taskModel.Name;
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
                sourceUserControl.taskModel.ColumnId = id;
                db.UpdateTasks(sourceUserControl.taskModel.Id, new { ColumnId = id });
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
                            try
                            {
                                DragDrop.DoDragDrop(userControl, userControl, DragDropEffects.Move);

                            }
                            catch { }


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
