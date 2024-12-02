﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Eclipse_of_the_Damned.classy
{
    public class GameMaster
    {
        public Entity Player { get; set; }
        public bool InCombat { get; set; }
        public bool PlayerTurn { get; set; }
        public float MasterVolume { get; set; }
        public float MusicVolume { get; set; }

        public bool Fullscreen { get; set; }

        public GameMaster(Entity player, bool inCombat, bool playerTurn, float masterVolume, float musicVolume, bool fullscreen)
        {
            Player = player;
            InCombat = inCombat;
            PlayerTurn = playerTurn;
            MasterVolume = masterVolume;
            MusicVolume = musicVolume;
            Fullscreen = fullscreen;
        }
    }
}

