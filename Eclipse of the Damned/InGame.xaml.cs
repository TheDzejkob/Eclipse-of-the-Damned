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
            stick = new Item(0, "stick", "je to prostě stick", "material", 1, 1, 1, 1.00f, new List<Ability> { }, "/images/Items/sword.png");
            test = new Item(1, "test", "negr bagr", "material", 1, 1, 1, 1.00f, new List<Ability> { }, "/images/Arrow.png");


            gameMaster.Player.Inventory.Add(stick);
            gameMaster.Player.Inventory.Add(test);
            updateInventory();
        }
        //private void TestClick(object sender, RoutedEventArgs e)
        //{
        //    SelectedInventoryItem = stick;
        //    updateItemDetail(sender, e);
        //}
        private void updateItemDetail(object sender, RoutedEventArgs e)
        {
            itemDetail.Visibility = Visibility.Visible;
            if (SelectedInventoryItem != null)
            {
                // Update the item name
                ItemName.Text = SelectedInventoryItem.Name;

                // Update the item description
                Discription.Text = SelectedInventoryItem.Description;

                // Update the item image
                ItemDisplay.Source = new BitmapImage(new Uri(SelectedInventoryItem.ImagePath, UriKind.Relative));

                ItemCategory.Text = SelectedInventoryItem.Category;
                ItemWeight.Text = SelectedInventoryItem.Weight.ToString();
                ItemValue.Text = SelectedInventoryItem.Value.ToString();
                switch (SelectedInventoryItem.Rarity)
                {
                    case 1:
                        ItemRarity.Text = "Common";
                        break;
                    case 2:
                        ItemRarity.Text = "Rare";
                        break;
                    case 3:
                        ItemRarity.Text = "Epic";
                        break;
                    case 4:
                        ItemRarity.Text = "Legendary";
                        break;
                }
                ItemDurability.Text = SelectedInventoryItem.Durability * 100 + "%";



            }
            else
            {
                // Clear the fields if no item is selected
                ItemName.Text = "prazdny";
                Discription.Text = "prazdny";
                ItemDisplay.Source = null;
            }
        }

        private void addToInventory(List<Item> items)
        {
            foreach (var item in items)
            {
                gameMaster.Player.Inventory.Add(item);
            }
            updateInventory();
        }
        private void takeFromInventory(List<Item> items)
        {
            foreach (var item in items)
            {
                gameMaster.Player.Inventory.Remove(item);
            }
            updateInventory();
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
            int value = Convert.ToInt32(tag);
            if (tag == null)
            {
                SelectedInventoryItem = null;
            }

            SelectedInventoryItem = gameMaster.Player.Inventory[value];

            updateItemDetail(sender, e);

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

        private void InventoryOpenButton_Click(object sender, RoutedEventArgs e)
        {
            if (Inventory.Visibility == Visibility.Visible) { 

                Inventory.Visibility = Visibility.Hidden;
                itemDetail.Visibility = Visibility.Hidden;
            }
            else
                Inventory.Visibility = Visibility.Visible;
        }
    }
}
