﻿using Newtonsoft.Json;
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
using System.Security.Cryptography;
using System.Reflection.Metadata;

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
        CombatManager combatManager;
        string json;
        string jsonFilePath;
        string basePath;
        public List<Item> AllItems;
        Entity SkeletonHalfling;

        int totalArmor;
        int startPlayerHealth;
        int startEnemyHealth;
        bool isBlurred;

        int Turn;
        public InGame(GameMaster gameMaster)
        {
            InitializeComponent();
            isBlurred = false;
            this.gameMaster = gameMaster;
            updateTime();
            combatManager = new CombatManager(null,0,true);
            gameMaster.CombatManager = combatManager;
            SkeletonHalfling = new Entity("Skeleton Halfling", 1,3,6,0,new List<Item>(), null,null, false, "/images/SkeletonHalfling.png");

            Turn = 1;
            AllItems = new List<Item>();

            LoadItemsFromJson();
        }
        private void LoadItemsFromJson()
        {

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
                ItemName.Text = SelectedInventoryItem.Name;

                Discription.Text = SelectedInventoryItem.Description;

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
        // prave jsem stavil hodinu nad errorem ktery byl chbejici zavorka pač si rekl autodoplnovani ze zrovna ted nebude fungovat :) 
        // lano je cesta kamo

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

        private void StartCombat()
        {
            CombatInterface.Visibility = Visibility.Visible;
            PlayerImage.Source = new BitmapImage(new Uri(gameMaster.Player.ImagePath, UriKind.Relative));
            EnemyImage.Source = new BitmapImage(new Uri(gameMaster.CombatManager.Enemy.ImagePath, UriKind.Relative));
            List<Ability> playerAbilities = new List<Ability>();
            startPlayerHealth = gameMaster.Player.Health;
            startEnemyHealth = gameMaster.CombatManager.Enemy.Health;

            CombatText.Text = "You have encountered a " + gameMaster.CombatManager.Enemy.EntityName + " " + "!";
            UpdateHp();
            if (gameMaster.EquipedWeapon == null)
            {
                Ability lightPunch = new Ability("Light Punch", "u just punched the mf in a face", 0, 3, 0, 2);
                Ability heavyPunch = new Ability("Heavy Punch", "u just punched the mf in a face", 0, 4, 0, 6);
                Ability handBlock = new Ability("Hand Block", "U are trying to block attack with your bare hands", 0, 0, 2, 4);
                playerAbilities.Add(lightPunch);
                playerAbilities.Add(heavyPunch);
            }
            else
            {
                playerAbilities = gameMaster.EquipedWeapon.Abilities;
            }

        }

        private void EndCombat()
        {
            gameMaster.CombatManager.Enemy = null;
            EnemyImage.Source = null;
            CombatInterface.Visibility = Visibility.Hidden;
        }

        private void UpdateHp()
        {
            PlayerHealth.Text ="Hp: " + gameMaster.Player.Health.ToString() + "/" + startPlayerHealth;
            EnemyHealth.Text = "Hp" + gameMaster.CombatManager.Enemy.Health.ToString() + "/" + startEnemyHealth;

            Turn++;
            gameMaster.CombatManager.Turn = Turn;
            TurnNumber.Text = "Turn: " + gameMaster.CombatManager.Turn.ToString();
        }

        private async void EnemyTurn()
        {
            ArrowLeft.Visibility = Visibility.Collapsed;
            ArrowRight.Visibility = Visibility.Visible;
            
            await Task.Delay(2000);
            // TODO aksuall dmg
            UpdateHp();
        }

        private void PlayerTurn()
        {
            ArrowRight.Visibility = Visibility.Collapsed;
            ArrowLeft.Visibility = Visibility.Visible;

            UpdateHp();
        }


        /*TODO: Death Player
                Death Enemy
                Attack Button based na abilities (chck pokud ma weapon)
                winScreen (POčet kol, total dmg, Enemy Name, Xp za zabití)
                Drops
                Demo Fights (alespoň 2 s dropy zbraně a armoru idk)
                pořešit Enemy dmg kde brát (asi jim dát item s abilitama (může to být i drop))
                AutoTimeUpdate
                Sound design (pokud bude čas)
         */  
        
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

        private void LoadCharacterWindow( object sender, RoutedEventArgs e)
        {
            if (CharacterPopUp.Visibility == Visibility.Visible) { 
            
                CharacterPopUp.Visibility = Visibility.Hidden;
            }
            else
            {
                CharacterPopUp.Visibility = Visibility.Visible;
            }

            CharacterName.Text = gameMaster.Player.EntityName;
            switch (gameMaster.Player.Gender)
            {
                case true:
                    CharacterGender.Text = "Male";
                    break;
                case false:
                    CharacterGender.Text = "Female";
                    break;

            }
            CharacterLevel.Text = "Level:" + " " + gameMaster.Player.Level.ToString();
            CharacterHealth.Text = "Health:" + " " + gameMaster.Player.Health.ToString();
            CharacterClass.Text = "Class:" + " " + gameMaster.Player.EntityClass.ClassName.ToString();
            CharacterRace.Text = "Race:" + " " + gameMaster.Player.EntityRace.RaceName.ToString();
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
        private void CalculateArmor()
        {
            totalArmor = gameMaster.EquipedBoots.Abilities[0].Armor + gameMaster.EquipedLeggings.Abilities[0].Armor + gameMaster.EquipedChestplate.Abilities[0].Armor + gameMaster.EquipedHelmet.Abilities[0].Armor;
        }

        private void CalculateDemage(int Demage)
        {
            CalculateArmor();

            float DamageMultiplier = Demage / (Demage + totalArmor);
            float finallDmg = Demage * DamageMultiplier;
            
        }
        private void updateInventory()
        {
            // Clear all inventory slots first
            for (int i = 0; i < 24; i++)
            {
                var slotImage = (System.Windows.Controls.Image)this.FindName($"InventorySlot{i}");
                if (slotImage != null)
                {
                    slotImage.Source = null;
                }
            }

            // Populate the inventory slots with item images
            for (int i = 0; i < gameMaster.Player.Inventory.Count; i++)
            {
                var item = gameMaster.Player.Inventory[i];
                var slotImage = (System.Windows.Controls.Image)this.FindName($"InventorySlot{i}");

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

        public static int GenerateRandomNumber()
        {
            Random random = new Random();
            return random.Next(0, 11); // Upper bound is exclusive, so use 11 to include 10
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

        private async void RunAwayButton_Click(object sender, RoutedEventArgs e)
        {
            if(gameMaster.CombatManager.PlayerTurn == true)
            {
                int randomNumber = GenerateRandomNumber();
                if(randomNumber > 7)
                {
                    CombatText.Text = "You have successfully run away!";
                    await Task.Delay(2000);
                    EndCombat();
                }
                else
                {
                    CombatText.Text = "You have failed to run away!";
                    await Task.Delay(2000);
                    EnemyTurn();
                }
                gameMaster.CombatManager.PlayerTurn = false;
            }
            else
            {

            }


        }
        private void ToggleBlur_Click(object sender, RoutedEventArgs e)
        {
            if (!isBlurred)
            {
                CombatBlur.Radius = 20; // Adjust this value to control blur intensity
            }
            else
            {
                CombatBlur.Radius = 0;
            }
            isBlurred = !isBlurred;
        }


        private void FightButton(object sender, RoutedEventArgs e)
        {
            AbilityButton1.Visibility = Visibility.Visible;
            AbilityButton2.Visibility = Visibility.Visible;
            AbilityButton3.Visibility = Visibility.Visible;
            AbilityButton4.Visibility = Visibility.Visible;
            BackFromCombat.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AbilityButton1.Visibility = Visibility.Hidden;
            AbilityButton2.Visibility = Visibility.Hidden;
            AbilityButton3.Visibility = Visibility.Hidden;
            AbilityButton4.Visibility = Visibility.Hidden;
            BackFromCombat.Visibility = Visibility.Hidden;
        }
    }
}
