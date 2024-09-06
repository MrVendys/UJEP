using GUI___Canban.View.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUI___Canban.View.UserControls
{
    /// <summary>
    /// Interakční logika pro Task.xaml
    /// </summary>
    public partial class Task : UserControl
    {
        public Task()
        {
            InitializeComponent();
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text)) { 
                PlaceHolder.Visibility = Visibility.Visible;
            }
            else
            {
                PlaceHolder.Visibility = Visibility.Collapsed;
            }
        }

        private void NameTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TaskWindow tw = new TaskWindow();
            tw.Show();
        }
    }
}
