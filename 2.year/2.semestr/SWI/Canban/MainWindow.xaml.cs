using Canban.DB.Models;
using Canban.View.UserControls;
using Canban.View.Windows;
using Microsoft.EntityFrameworkCore;
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
                boardControl.OpenButton.Click += OnBoardControl_Click;
                BoardWrapPanel.Children.Add(boardControl);
            }
            boardControl = new BoardControl(loggedUser);
            boardControl.OpenButton.Click += OnBoardControl_Click;
            BoardWrapPanel.Children.Add(boardControl);
        }
        private void OnBoardControl_Click(object sender, RoutedEventArgs e)
        {
            //TODO 
            //BoardControlWindow pro vytvoreni BoardWindow a control
            boardControl.boardWindow.Show();
            this.Visibility = Visibility.Collapsed;
        }
    }
}