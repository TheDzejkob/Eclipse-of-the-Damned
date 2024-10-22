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

        public Button SelectedRaceButton;
        public Button _previouslySelectedRaceButton;

        public Button SelectedClassButton;
        public Button _previouslySelectedClassButton;

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

        private void RaceButton_Click(object sender, RoutedEventArgs e) 
        {
            if (sender is Button clickedButton)
            {
                SelectedRaceButton = clickedButton; 
                if (_previouslySelectedRaceButton != null)
                {
                    _previouslySelectedRaceButton.BorderBrush = Brushes.Transparent;
                    _previouslySelectedRaceButton.BorderThickness = new Thickness(0);
                }

                clickedButton.BorderBrush = Brushes.White;
                clickedButton.BorderThickness = new Thickness(3);

                _previouslySelectedRaceButton = clickedButton;
            }    
        }

        private void ClassButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton)
            {
                SelectedClassButton = clickedButton;
                if (_previouslySelectedClassButton != null)
                {
                    _previouslySelectedClassButton.BorderBrush = Brushes.Transparent;
                    _previouslySelectedClassButton.BorderThickness = new Thickness(0);
                }

                clickedButton.BorderBrush = Brushes.White;
                clickedButton.BorderThickness = new Thickness(3);

                _previouslySelectedClassButton = clickedButton;
            }
        }
    }
}
