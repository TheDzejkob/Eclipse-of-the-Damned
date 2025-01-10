using System;

namespace Eclipse_of_the_Damned.classy
{
    public class GameMaster
    {
        public Entity Player { get; set; }
        public Item EquipedWeapon { get; set; }
        public Item EquipedHelmet { get; set; }
        public Item EquipedChestplate { get; set; }
        public Item EquipedLeggings { get; set; }
        public Item EquipedBoots { get; set; }
        public CombatManager CombatManager { get; set; }

        private float masterVolume;
        private float musicVolume;

        public float MasterVolume
        {
            get => masterVolume;
            set => masterVolume = Math.Clamp(value, 0f, 1f);
        }

        public float MusicVolume
        {
            get => musicVolume;
            set => musicVolume = Math.Clamp(value, 0f, 1f); 
        }

        public bool Fullscreen { get; set; }

        public GameMaster(Entity player, Item equipedWeapon, Item equipedHelmet, Item equipedChestplate, Item equipedLeggings, Item equipedBoots, CombatManager combatManager, float masterVolume, float musicVolume, bool fullscreen)
        {
            Player = player;
            EquipedWeapon = equipedWeapon;
            EquipedHelmet = equipedHelmet;
            EquipedChestplate = equipedChestplate;
            EquipedLeggings = equipedLeggings;
            EquipedBoots = equipedBoots;
            CombatManager = combatManager;
            MasterVolume = masterVolume; 
            MusicVolume = musicVolume;   
            Fullscreen = fullscreen;
        }
    }
}
