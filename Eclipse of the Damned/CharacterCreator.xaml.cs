using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Eclipse_of_the_Damned
{
    /// <summary>
    /// Interakční logika pro CharacterCreator.xaml
    /// </summary>
    public partial class CharacterCreator : Window
    {
        private Button _previouslySelectedButton;
        public Button SelectedGenderButton;

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

        private void GenderButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton)
            {

                SelectedGenderButton = clickedButton;

                if (_previouslySelectedButton != null)
                {
                    _previouslySelectedButton.BorderBrush = Brushes.Transparent;
                    _previouslySelectedButton.BorderThickness = new Thickness(0);
                }

                clickedButton.BorderBrush = Brushes.White;
                clickedButton.BorderThickness = new Thickness(3);

                _previouslySelectedButton = clickedButton;

                
            }
        }
    }
}
