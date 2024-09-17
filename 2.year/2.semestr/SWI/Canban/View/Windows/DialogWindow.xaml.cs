using Microsoft.IdentityModel.Tokens;
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

namespace Canban.View.Windows
{
    /// <summary>
    /// Interakční logika pro DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        private string InputName;
        public string inputName { get { return InputName; } set { } }
        private string ColorName;
        public string colorName { get { return ColorName; } set { } }
        public DialogWindow()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            InputName = InputTextBox.Text;
            ColorName = InputColorPicker.SelectedColor.ToString();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
