using Canban.View.UserControls;
using System.Windows;

namespace Canban
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            TaskControl t = new TaskControl();
            TaskStackPanel.Children.Add(t);
        }
    }
}