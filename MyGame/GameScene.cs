using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using SFML.Graphics;
using SFML.Graphics.Glsl;
using SFML.System;
using SFML.Window;
using System.Drawing;
using static System.Formats.Asn1.AsnWriter;
using System.Security.Cryptography;
using Color = SFML.Graphics.Color;

namespace MyGame
{
    class GameScene : Scene
    {
        public bool shield = false;
        // -1 equals the points are returning, 0 equals false/disabled, 1 equals true/enabled
        public int shipEnable = 0;
        public GameScene()
        {
            AddGameObject(new Ship());
            AddGameObject(new metorSpawner());
        }
        public int vaporCount()
        {
            int a = 0;
            foreach(GameObject e in GetObjs())
            {
                if (e.HasTag("partv") || e.HasTag("partvi")) a++;
            }
            return a;
        }
    }
}