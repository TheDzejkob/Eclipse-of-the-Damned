using Newtonsoft.Json;
using Eclipse_of_the_Damned.classy;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

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
        string json;
        string jsonFilePath;
        string basePath;
        public List<Item> AllItems ;


        public InGame(GameMaster gameMaster)
        {
            InitializeComponent();
            this.gameMaster = gameMaster;
            updateTime();

            // Initialize AllItems list
            AllItems = new List<Item>();

            // Deserialize JSON file into the AllItems list
            LoadItemsFromJson();

            // You can add the stick as an example item, or remove this if not needed
            //stick = new Item(0, "stick", "je to prostě stick", "material", 1, 1, 1, 1.00f, new List<Ability> { }, "/images/Items/sword.png");

            // Example of adding the stick to the player's inventory
            //gameMaster.Player.Inventory.Add(stick);
            //updateInventory();
        }
        private void LoadItemsFromJson()
        {
            // Provide the path to your JSON file
            jsonFilePath = "json/Items.json";  // Update with the actual path
            basePath = AppDomain.CurrentDomain.BaseDirectory;  // Base directory for the app

            // Read the JSON file
            try
            {
                json = File.ReadAllText(jsonFilePath);

                // Deserialize JSON string into a list of Item objects
                AllItems = JsonConvert.DeserializeObject<List<Item>>(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading items from JSON: " + ex.Message);
            }
        }

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



        // gl se v tomhle vyznat (for future jacob :D ) 
        // mrdky chyby
        //prave jsem stavil hodinu nad errorem ktery byl chbejici zavorka pač si rekl autodoplnovani ze zrovna ted nebude fungovat :) 
        //lano je cesta kamo

        private void test_Click(object sender, RoutedEventArgs e)
        {
            // Check if AllItems is not null and has at least one item
            if (AllItems != null && AllItems.Count > 0)
            {
                // Add the first item from AllItems to the player's inventory
                

                // Update the inventory UI to reflect the change
                List<int> itemIndexes = new List<int> { 0, 1, };
                addToInventory(itemIndexes);
                updateInventory();
                // Optionally show a confirmation message
                MessageBox.Show("Item added to inventory!");
            }
            else
            {
                MessageBox.Show("No items available in AllItems.");
            }
        }
        private void addToInventory(List<int> itemsIndexes)
        {
            foreach (int index in itemsIndexes)
            {
                if (index >= 0 && index < AllItems.Count)
                {
                    gameMaster.Player.Inventory.Add(AllItems[index]);
                }
                else
                {
                    MessageBox.Show("You got nothing :D.");
                }
            }
            MessageBox.Show("You got something.");
            updateInventory();
        }
        private void takeFromInventory(List<int> itemsIndexes)
        {
            foreach (var index in itemsIndexes)
            {
                gameMaster.Player.Inventory.Add(AllItems[index]);
            }
            updateInventory();
        }


        private void EquipItem()
        {
            switch (SelectedInventoryItem.Category)
            {
                case "Weapon":
                    gameMaster.EquipedWeapon = SelectedInventoryItem;
                    break;
                case "Helmet":
                    gameMaster.EquipedHelmet = SelectedInventoryItem;
                    break;
                case "Chestplate":
                    gameMaster.EquipedChestplate = SelectedInventoryItem;
                    break;
                case "Leggings":
                    gameMaster.EquipedLeggings = SelectedInventoryItem;
                    break;
                case "Boots":
                    gameMaster.EquipedBoots = SelectedInventoryItem;
                    break;
            }
        }


        private void CalculateDemage(int Demage)
        {
            int totalArmor = gameMaster.EquipedBoots.Abilities[0].Armor + gameMaster.EquipedLeggings.Abilities[0].Armor + gameMaster.EquipedChestplate.Abilities[0].Armor + gameMaster.EquipedHelmet.Abilities[0].Armor ;
            
            float DamageMultiplier = Demage / (Demage + totalArmor);
            float finallDmg = Demage * DamageMultiplier;

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
        //párno je modlidba

        private void InventorySlot_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton == null)
                return;

            var tag = clickedButton.Tag;
            int value = Convert.ToInt32(tag);

            // Ensure the inventory slot has an item assigned to it
            if (value >= 0 && value < gameMaster.Player.Inventory.Count)
            {
                SelectedInventoryItem = gameMaster.Player.Inventory[value];
                updateItemDetail(sender, e);
            }
            else
            {
                // Clear item details if the slot is empty
                SelectedInventoryItem = null;
                updateItemDetail(sender, e);
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
