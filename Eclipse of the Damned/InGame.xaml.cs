using Eclipse_of_the_Damned.classy;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interakční logika pro InGame.xaml
    /// </summary>
    public partial class InGame : Window
    {
        private GameMaster gameMaster;

        public InGame(GameMaster gameMaster)
        {
            InitializeComponent();
            this.gameMaster = gameMaster;
            test.Content = gameMaster.Player.EntityName + " " + gameMaster.Player.EntityClass.ClassName + " " + gameMaster.Player.EntityRace.RaceName;
            updateTime();
        }

        private void updateTime()
        {
            var enUs = new CultureInfo("en-US");
            Time.Content = DateTime.Now.ToString("HH:mm");
            Day.Content = DateTime.Now.ToString("Day 1. / " + "dddd", enUs);
        }

        private void GearMenu_Click(object sender, RoutedEventArgs e)
        {
            if (GearMenu.Visibility == Visibility.Visible)
                GearMenu.Visibility = Visibility.Hidden;
            else
                GearMenu.Visibility = Visibility.Visible;
        }


        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MainMenuButton_Click_1(object sender, RoutedEventArgs e)
        {

            MainWindow mainMenu = new MainWindow();
            mainMenu.Show();
            this.Close();
        }
    }
}
