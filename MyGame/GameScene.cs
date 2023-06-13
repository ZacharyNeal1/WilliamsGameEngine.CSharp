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
        public int shipEnable = 1; // -1 equals the points are returning, 0 equals false/disabled, 1 equals true/enabled

        public bool shield = false;
        public int shieldPower = 1020;

        public int scorei = 0;
        public int addedScore = 0;
        public GameScene()
        {
            AddGameObject(new Ship());
            AddGameObject(new metorSpawner());

            AddGameObject(new Score());
            shipEnable = 1;
        }
        public int vaporCount()
        {
            int a = 0;
            foreach(GameObject e in GetObjs())
            {
                if (e.HasTag("vapor")&&!e.IsDead()) a++;
            }
            return a;
        }
        public bool CountMainPart()
        {
            foreach (GameObject e in GetObjs())
            {
                if (e.HasTag("partvi") && !e.IsDead()) return true;
            }
            return false;
        }
    }
}