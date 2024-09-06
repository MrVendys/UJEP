using Canban.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Canban.View.UserControls
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

        private void NameTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TaskWindow tw = new TaskWindow();
            tw.Show();
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                PlaceHolder.Visibility = Visibility.Visible;
            }
            else
            {
                PlaceHolder.Visibility = Visibility.Collapsed;
            }
        }
    }
}
