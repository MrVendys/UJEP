using Canban.DB.Models;
using Canban.View.UserControls;
using Canban.View.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            
        }

        private void BoardWrapPanel_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db.Boards.Load();
            foreach (var board in db.Boards) {
                BoardControl boardControl = new BoardControl(loggedUser);
                boardControl.OpenButton.Click += OnBoardControlButton_Click;
                BoardWrapPanel.Children.Insert(0,boardControl);
            }
            /*
            boardControl = new BoardControl(loggedUser);
            boardControl.OpenButton.Click += OnBoardControlButton_Click;
            BoardWrapPanel.Children.Add(boardControl);*/
        }
        private void CreateBoardControl(UserModel loggedUser, string name)
        {
            boardControl = new BoardControl(loggedUser);
            boardControl.boardName = name;
            boardControl.OpenButton.Click += OnBoardControlButton_Click;
            BoardWrapPanel.Children.Insert(0, boardControl);
        }
        private void OnBoardControlButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO 
            //BoardControlWindow pro vytvoreni BoardWindow a control
            BoardWindow boardWindow = new BoardWindow(boardControl.boardModel.Id);
            boardWindow.Show();
            this.Visibility = Visibility.Collapsed;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new DialogWindow();
            bool? result = dialog.ShowDialog();

            if (result == true) {
                if (!dialog.NameTextBox.Text.IsNullOrEmpty())
                {
                    CreateBoardControl(loggedUser, dialog.boardName);
                }
            }
        }
    }
}