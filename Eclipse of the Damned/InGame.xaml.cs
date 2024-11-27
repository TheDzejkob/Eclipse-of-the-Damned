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
    }
}
