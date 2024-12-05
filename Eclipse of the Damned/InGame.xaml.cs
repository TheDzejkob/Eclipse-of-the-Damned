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
        Item SelectedInventoryItem;
        Item stick;
        Item test;
        private GameMaster gameMaster;

        public InGame(GameMaster gameMaster)
        {
            InitializeComponent();
            this.gameMaster = gameMaster;
            updateTime();
            stick = new Item(0, "stick", "je to prostě stick", "material", 1, "/images/Items/sword.png");
            test = new Item(0, "test", "test more", "weapon", 0.1f, "/images/Items/Arrow.png");

            gameMaster.Player.Inventory.Add(stick);
            gameMaster.Player.Inventory.Add(test);
            updateInventory();
        }

        private void updateItemDetail()
        {
            if (SelectedInventoryItem != null)
            {
                // Update the item name
                ItemName.Text = SelectedInventoryItem.Name;

                // Update the item description
                Discription.Text = SelectedInventoryItem.Description;

                // Update the item image
                ItemDisplay.Source = new BitmapImage(new Uri(SelectedInventoryItem.ImagePath, UriKind.Relative));
            }
            else
            {
                // Clear the fields if no item is selected
                ItemName.Text = string.Empty;
                Discription.Text = string.Empty;
                ItemDisplay.Source = null;
            }
        }

        private void updateInventory()
        {
            // Clear all inventory slots first
            for (int i = 0; i < 24; i++)
            {
                var slotImage = (Image)this.FindName($"InventorySlot{i}");
                if (slotImage != null)
                {
                    slotImage.Source = null;
                }
            }

            // Populate the inventory slots with item images
            for (int i = 0; i < gameMaster.Player.Inventory.Count; i++)
            {
                var item = gameMaster.Player.Inventory[i];
                var slotImage = (Image)this.FindName($"InventorySlot{i}");

                if (slotImage != null && item != null)
                {
                    // Set the image source for each slot
                    slotImage.Source = new BitmapImage(new Uri(item.ImagePath, UriKind.Relative));
                }
            }
        }

        private void InventorySlot_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton == null)
                return;

            var tag = clickedButton.Tag;
            if (tag == null)
                return;

            if (int.TryParse(tag.ToString(), out int index))
            {
                if (index >= 0 && index < gameMaster.Player.Inventory.Count)
                {
                    // Set the selected inventory item
                    SelectedInventoryItem = gameMaster.Player.Inventory[index];

                    // Update the item details in the UI
                    updateItemDetail();
                }
            }
        }

        private void updateTime()
        {
            // Update the time display
            Time.Content = DateTime.Now.ToString("HH:mm");
            var enUs = new CultureInfo("en-US");
            Day.Content = "Day 1. / " + DateTime.Now.ToString("dddd", enUs);
        }

        // Menu toggle methods
        private void BookMenu_Click(object sender, RoutedEventArgs e)
        {
            if (BookMenu.Visibility == Visibility.Visible)
                BookMenu.Visibility = Visibility.Hidden;
            else
                BookMenu.Visibility = Visibility.Visible;
        }

        private void GearMenu_Click(object sender, RoutedEventArgs e)
        {
            if (GearMenu.Visibility == Visibility.Visible)
                GearMenu.Visibility = Visibility.Hidden;
            else
                GearMenu.Visibility = Visibility.Visible;
        }

        // Close the game window
        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Navigate to the main menu
        private void MainMenuButton_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainMenu = new MainWindow();
            mainMenu.Show();
            this.Close();
        }
    }
}
