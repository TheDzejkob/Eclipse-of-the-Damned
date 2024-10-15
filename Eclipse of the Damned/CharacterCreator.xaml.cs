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

namespace Eclipse_of_the_Damned
{
    /// <summary>
    /// Interakční logika pro CharacterCreator.xaml
    /// </summary>
    public partial class CharacterCreator : Window
    {
        public CharacterCreator()
        {
            InitializeComponent();
        }

        private void NameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text == "Enter your name...")
            {
                NameTextBox.Text = "";
                NameTextBox.Foreground = new SolidColorBrush(Colors.White);
            }
        }

        private void NameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                NameTextBox.Text = "Enter your name...";
                NameTextBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
    }

}
