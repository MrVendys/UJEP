using Canban.DB.Models;
using Canban.View.UserControls;
using Canban.View.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
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
        /// <summary>
        /// Načtení z databáze již vytvořených UserControls pro jednotilvé tabule
        /// </summary>
        private void LoadBoardControls()
        {
            db.Boards.Load();
            db.Tasks.Load();
            if (db.Boards.Any())
            {
                List<BoardModel> boards = new List<BoardModel>();
                foreach (var board in db.Boards.Where(x=>x.UserId == loggedUser.Id))
                {
                    boards.Add(board);
                }
                
                var tasksWithUsers = db.Tasks.Include(x => x.Users);
                foreach (var task in tasksWithUsers) {
                    var user = task.Users.Where(x => x.Id == loggedUser.Id).FirstOrDefault();
                    BoardModel board = null;
                    if(user != null)
                        board = db.Boards.Find((db.Columns.Find(task.ColumnId).BoardId));
                    if (!boards.Contains(board) && board != null)
                    {
                        boards.Add((board));
                    }
                }
                db.ChangeTracker.Clear();
                foreach (var board in boards)
                {
                    CreateBoardControl(board);
                }
            }
           
        }
        /// <summary>
        /// Vytvoreni UserControl prvku -> BoardControl
        /// </summary>
        /// <param name="boardModel"></param>
        /// <param name="name"></param>
        private void CreateBoardControl(BoardModel boardModel)
        {

            boardControl = new BoardControl(boardModel);
            boardControl.OpenButton.Click += OnBoardControlButton_Click;
            BoardWrapPanel.Children.Insert(0, boardControl);
        }
        /// <summary>
        /// Vytvoreni BoardModelu, ulozeni do databaze 
        /// a zavolani funkce na vytvoreni BoardControlu
        /// </summary>
        /// <param name="boardName"></param>
        private void CreateBoard(string boardName)
        {
            db.Boards.Load();
            db.Users.Load();

            var user = db.Users.Where(x => x.Id == loggedUser.Id).FirstOrDefault();
            BoardModel boardModel = new BoardModel()
            {
                Name = boardName,
                UserId = user.Id,
                CreatedBy = user
            };

            db.Add(boardModel);
            db.SaveChanges();
            db.ChangeTracker.Clear();
            CreateBoardControl(boardModel);
        }
        
        private void ShowWindow(Window window)
        {
            window.Show();
            this.Close();
        }
        private void OnBoardControlButton_Click(object sender, RoutedEventArgs e)
        {
            BoardWindow boardWindow = new BoardWindow(boardControl.boardModel.Id, loggedUser);
            ShowWindow(boardWindow);
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            ShowWindow(loginWindow);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new DialogWindow();
            bool? result = dialog.ShowDialog();

            if (result == true) {
                if (!dialog.InputTextBox.Text.IsNullOrEmpty())
                {
                    CreateBoard(dialog.inputName);
                }
            }
        }
    }
}