using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interakční logika pro TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        public ObservableCollection<string> AvailableItems { get; set; }
        public ObservableCollection<string> SelectedItems { get; set; }
        public ObservableCollection<string> StatusItems { get; set; }
        public string SelectedItem { get; set; }
        public TaskWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            // Initialize the available items for the ComboBox
            AvailableItems = new ObservableCollection<string>
            {
                "Václav Pták", "Daniel Klein", "Radek Šmejkal", "Martin Formánek"
            };

            // Initialize the collection for selected items
            SelectedItems = new ObservableCollection<string>();

            StatusItems = new ObservableCollection<string> { 
                "To Do", "In Progress", "Done"
            };
        }


        private void StatusCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }


        private void AssigneeComboBox_DropDownClosed(object sender, EventArgs e)
        {
            /*if (AssigneeComboBox.SelectedItem != null)
            {
                /*
                if (!AssigneeListBox.Items.Contains(AssigneeComboBox.SelectedItem))
                    AssigneeListBox.Items.Add(AssigneeComboBox.SelectedItem.ToString());
                else
                    AssigneeListBox.Items.Remove(AssigneeComboBox.SelectedItem.ToString());
            
                if (!ItemsControl.Contains(AssigneeComboBox.SelectedItem.ToString()))
                {
                    ItemsControl.Add(AssigneeComboBox.SelectedItem.ToString());
                }
                else
                {
                    ItemsControl.Remove(AssigneeComboBox.SelectedItem.ToString());
                }
            }
            // Add the selected item to the SelectedItems collection
            */
        }
        /// <summary>
        /// Adding selected item from combo box to Bind list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssigneeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedItem != null && !SelectedItems.Contains(SelectedItem))
            {
                SelectedItems.Add(SelectedItem);
            }
        }
    }
}
