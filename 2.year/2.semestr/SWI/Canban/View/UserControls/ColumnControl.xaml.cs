using Canban.DB.Models;
using Canban.View.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        private string columnName = "New Column";
        public string ColumnName
        {
            get { return columnName; }
            set
            {
                columnName = value;
                OnPropertyChanged("ColumnName");
            }
        }
        private string columnColor = "White";
        public string ColumnColor
        {
            get { return columnColor; }
            set { columnColor = value; }
        }
        private ColumnModel columnModel;
        public int id { get { return ID; } set { ID = value; } }
        private int ID;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public UserModel loggedUser;
        public ColumnControl(ColumnModel columnModel, UserModel loggedUser)
        {
            InitializeComponent();
            this.loggedUser = loggedUser;
            this.columnModel = columnModel;
            this.columnName = columnModel.Name.IsNullOrEmpty() ? ColumnName : columnModel.Name;
            this.ColumnColor = columnModel.Color.IsNullOrEmpty() ? ColumnColor : columnModel.Color; 
            this.TaskControlGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(this.columnColor));
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
            TaskControl task = new TaskControl(taskModel, loggedUser);
            task.NameTextBox.Text = taskModel.Name;
            task.DeleteRequested += UserControl_DeleteRequested;
            task.MouseMove += UserControl_MouseMove;
            task.MouseDown += UserControl_MouseDown;
            task.StackPanel = TaskStackPanel;
            TaskStackPanel.MinHeight += task.MinHeight;
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
                TaskStackPanel.MinHeight -= userControl.MinHeight;
            }
        }

        private void TaskStackPanel_Drop(object sender, DragEventArgs e)
        {
            //Zkontrolovat drag&drop
            var sourceUserControl = e.Data.GetData(typeof(TaskControl)) as TaskControl;
            if (sourceUserControl != null)
            {
                var stackPanel = sender as StackPanel;

                TaskControl movingTask = sourceUserControl as TaskControl;
                sourceUserControl.StackPanel.Children.Remove(movingTask);
                sourceUserControl.StackPanel.MinHeight -= movingTask.MinHeight;
                stackPanel.MinHeight += movingTask.MinHeight;
                stackPanel.Children.Add(movingTask);
                sourceUserControl.StackPanel = TaskStackPanel;
                sourceUserControl.taskModel.ColumnId = id;
                db.SaveChanges();
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
                if (Math.Abs(diff.X) < SystemParameters.MinimumHorizontalDragDistance + 50)
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


        private void ColumnHeaderTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //TODO UpdateUI funkci
            DialogWindow dialog = new DialogWindow();
            bool? result = dialog.ShowDialog();
            db.Columns.Load();
            if (result == true)
            {
                if (!dialog.inputName.Equals(this.ColumnName) && !dialog.inputName.IsNullOrEmpty() )
                {
                    this.ColumnName = dialog.inputName;
                    db.Columns.Find(columnModel.Id).Name = dialog.inputName;
                    db.SaveChanges();
                   }
                else 
                {

                }
                if (!dialog.colorName.Equals(this.Background) && !dialog.colorName.IsNullOrEmpty())
                {
                    this.TaskControlGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(dialog.colorName));
                    db.Columns.Find(columnModel.Id).Color = dialog.colorName;
                    db.SaveChanges();
                }
            }
        }
    }
}
