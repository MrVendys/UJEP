using Canban.DB.Models;
using Canban.View.UserControls;
using Canban.View.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
namespace Canban
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DatabaseContext db;
        UserModel loggedUser;
        BoardControl boardControl;
        public MainWindow(UserModel userModel)
        {
            db = new DatabaseContext();
            loggedUser = userModel;

            
            InitializeComponent();
            LoadBoardControls();
        }

        private void LoadBoardControls()
        {
            db.Boards.Load();
            if (db.Boards.Any())
            {
                List<BoardModel> boards = new List<BoardModel>();
                foreach (var board in db.Boards.Where(x=>x.UserId == loggedUser.Id))
                {
                    boards.Add(board);
                }
                var tasks = db.Tasks.Include(x => x.Users);
                
                
                List<int> boardsId = new List<int>();
                foreach (var task in tasks) {
                    var user = task.Users.Where(x => x.Id == loggedUser.Id).FirstOrDefault();
                    BoardModel board = null;
                    if(user != null)
                        board = db.Boards.Find((db.Columns.Find(task.ColumnId).BoardId));
                    if (!boards.Contains(board) && board != null)
                    {
                        boards.Add((board));
                    }
                }
                foreach (var board in boards)
                {
                    CreateBoardControl(board, board.Name);
                }
            }
            
           
        }

        private void BoardWrapPanel_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            /*
            boardControl = new BoardControl(loggedUser);
            boardControl.OpenButton.Click += OnBoardControlButton_Click;
            BoardWrapPanel.Children.Add(boardControl);*/
        }

        private void CreateBoardControl(BoardModel boardModel, string name)
        {

            boardControl = new BoardControl(name, boardModel);
            boardControl.OpenButton.Click += OnBoardControlButton_Click;
            BoardWrapPanel.Children.Insert(0, boardControl);
        }
        private void OnBoardControlButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO 
            //BoardControlWindow pro vytvoreni BoardWindow a control
            BoardWindow boardWindow = new BoardWindow(boardControl.boardModel.Id);
            boardWindow.Show();
            this.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new DialogWindow();
            bool? result = dialog.ShowDialog();

            if (result == true) {
                if (!dialog.NameTextBox.Text.IsNullOrEmpty())
                {
                    db.Boards.Load();
                    db.Users.Load();
                    db.SaveChanges();

                    var user = db.Users.Where(x => x.Id == loggedUser.Id).FirstOrDefault();
                    BoardModel boardModel = new BoardModel()
                    {
                        Name = dialog.boardName,
                        UserId = user.Id,
                        CreatedBy = user
                    };

                    db.Add(boardModel);
                    db.SaveChanges();
                    CreateBoardControl(boardModel, dialog.boardName);
                }
            }
        }
    }
}