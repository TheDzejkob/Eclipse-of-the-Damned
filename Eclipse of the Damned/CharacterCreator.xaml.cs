using Eclipse_of_the_Damned.classy;
using System;
using System.Security.Cryptography.X509Certificates;
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
            human = new Race("Human",1);
            elf = new Race("Elf",2);
            orc = new Race("Orc",3);
            dwarf = new Race("Dwarf",4);
            goblin = new Race("Goblin",5);
            corvum = new Race("Corvum",6);

            gameMaster.Player = player;

        }


        private void CreateCharacter_Click(object sender, RoutedEventArgs e)
        {
            player.EntityName = NameTextBox.Text;
            int classesIndex = Convert.ToInt32(SelectedClassButton.Tag);
            player.EntityClass = classes[classesIndex];
             

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
