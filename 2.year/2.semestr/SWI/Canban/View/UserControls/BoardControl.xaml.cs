using Canban.DB.Models;
using Canban.View.Windows;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Canban.View.UserControls
{
    /// <summary>
    /// Interakční logika pro BoardControl.xaml
    /// </summary>
    public partial class BoardControl : UserControl, INotifyPropertyChanged
    {
        public BoardModel boardModel;
        public event PropertyChangedEventHandler? PropertyChanged;
        private string boardName = "Kanban";
        public string BoardName
        {
            get { return boardName; }
            set
            {
                boardName = value;
                OnPropertyChanged("BoardName");
                }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public BoardControl(BoardModel boardModel)
        {
            boardName = boardModel.Name;
            this.boardModel = boardModel;
            InitializeComponent();
            DataContext = this;
        }

    }
}
