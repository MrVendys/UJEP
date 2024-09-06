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
using System.Windows.Shapes;

namespace GUI___Canban.View.Windows
{
    /// <summary>
    /// Interakční logika pro TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        public TaskWindow()
        {
            InitializeComponent();
        }

        private void StatusCombobox_Selected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(StatusCombobox.SelectedValue.ToString());
        }
    }
}
