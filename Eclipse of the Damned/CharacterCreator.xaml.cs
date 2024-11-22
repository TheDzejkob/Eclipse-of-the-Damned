using Eclipse_of_the_Damned.classy;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
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
        public Entity player;

        // Classy
        public Class warrior;
        public Class archer;
        public Class mage;
        public Class bard;
        public Class thief;
        public Class rogue;


        public Class selectedClass;

        //Rasy
        public Race human;
        public Race elf;
        public Race orc;
        public Race dwarf;
        public Race goblin;
        public Race corvum;

        public GameMaster gameMaster;

        private Button _previouslySelectedButton;
        public Button SelectedGenderButton;

        public Button SelectedRaceButton;
        public Button _previouslySelectedRaceButton;

        public Button SelectedClassButton;
        public Button _previouslySelectedClassButton;
        public List<Class> classes;
        public List<Race> races;

        int? classesIndex;
        int? racesIndex;

        public CharacterCreator(GameMaster gameMaster)
        {
            InitializeComponent();
            this.gameMaster = gameMaster;

            player = new Entity("ingác", 0, 0, 1, 0, null, null, null, true);

            //class
            warrior = new Class("Warrior", 0);
            archer = new Class("Archer", 1);
            mage = new Class("Mage", 2);
            bard = new Class("Bard", 3);
            thief = new Class("Thief", 4);
            rogue = new Class("Rogue", 5);

            classes = new List<Class>() { warrior, archer, mage, bard, thief, rogue};
            
            
            //race
            human = new Race("Human",0);
            elf = new Race("Elf",1);
            orc = new Race("Orc",2);
            dwarf = new Race("Dwarf",3);
            goblin = new Race("Goblin",4);
            corvum = new Race("Corvum",5);

            races = new List<Race>() { human, elf, orc, dwarf, goblin, corvum };

            gameMaster.Player = player;

        }


        private void CreateCharacter_Click(object sender, RoutedEventArgs e)
        {
            if (classesIndex == null || racesIndex == null || NameTextBox.Text == null || NameTextBox.Text == "Enter your name...")
            {
                WarningCanvas.Visibility = Visibility.Visible;
            }
            else
            {
                player.EntityName = NameTextBox.Text;
                player.EntityClass = classes[classesIndex.Value];
                player.EntityRace = races[racesIndex.Value];

                InGame window = new InGame();
                window.Show();
                this.Close();
            }
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
                racesIndex = Convert.ToInt32(SelectedRaceButton.Tag);
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
                classesIndex = Convert.ToInt32(SelectedClassButton.Tag);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WarningCanvas.Visibility = Visibility.Collapsed;
        }
    }
}
