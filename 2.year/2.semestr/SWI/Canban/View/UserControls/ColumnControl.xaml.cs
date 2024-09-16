using Canban.DB.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Canban.View.UserControls
{
    /// <summary>
    /// Interakční logika pro ColumnControl.xaml
    /// </summary>
    public partial class ColumnControl : UserControl
    {
        private DatabaseContext db;
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
        private ColumnModel columnModel;
        public int id { get { return ID; } set { ID = value; } }
        private int ID;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ColumnControl(ColumnModel columnModel)
        {
            InitializeComponent();
            this.columnModel = columnModel;
            this.columnName = columnModel.Name;
            string a = System.Drawing.Color.White.ToArgb().ToString();
            if (columnModel.Color != null )
                this.TaskStackPanel.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(columnModel.Color));
            db = new DatabaseContext();

            DataContext = this;
            LoadComponents();
        }
        /// <summary>
        /// Nacteni tasku z databaze
        /// </summary>
        private void LoadComponents()
        {
            db.Tasks.Load();
            foreach (var task in db.Tasks.Where(x => x.ColumnId == id))
            {
                CreateTaskUI(task);
            }
        }
        /// <summary>
        /// Vytvoreni modelu do databaze tasku
        /// </summary>
        private void CreateTask()
        {
            TaskModel taskModel = new TaskModel()
            {
                Name = "Novy task",
                ColumnId = this.columnModel.Id
            };
            db.Add(taskModel);
            db.SaveChanges();
            db.ChangeTracker.Clear();
            CreateTaskUI(taskModel);
        }
        /// <summary>
        /// Vytvoreni UserControl tasku
        /// </summary>
        /// <param name="taskModel">Predani TaskModelu</param>
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
                db.ChangeTracker.Clear();
                TaskStackPanel.Children.Remove(userControl);
            }
        }

        private void TaskStackPanel_Drop(object sender, DragEventArgs e)
        {
            var sourceUserControl = e.Data.GetData(typeof(TaskControl)) as TaskControl;
            if (sourceUserControl != null)
            {
                var stackPanel = sender as StackPanel;

                sourceUserControl.StackPanel.Children.Remove(sourceUserControl as TaskControl);
                stackPanel.Children.Add(sourceUserControl as TaskControl);
                sourceUserControl.StackPanel = TaskStackPanel;
                sourceUserControl.taskModel.ColumnId = id;
                db.UpdateTasks(sourceUserControl.taskModel.Id, new { ColumnId = id });
            }
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
                double a = Math.Abs(diff.X);
                // Zajistíme, že uživatel posunul myš alespoň o určitou vzdálenost
                if (Math.Abs(diff.X) < SystemParameters.MinimumHorizontalDragDistance + 120)
                {
                    var userControl = sender as UserControl;

                    if (userControl != null)
                    {
                        Dispatcher.BeginInvoke(() =>
                        {
                            try
                            {
                                Console.WriteLine(a);
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
            if (e.LeftButton == MouseButtonState.Pressed)
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
