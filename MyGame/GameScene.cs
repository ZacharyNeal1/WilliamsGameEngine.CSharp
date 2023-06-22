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
        //
        public int diff = 1; // 0 - easy | 1 - normal | 2 - hard

        public bool scoreToLiveOverFlow = true; // when you hit the score set below you gain a life
        public int scoreOverflow = 25000; // will only be used if above is true

        public bool color = false; //invert color

        public bool thin = false; // thiner lines - better for preformance

        public bool mutatorMode = false; // chaos

        
        //

        public int shipEnable = 1; // -1 equals the points are returning, 0 equals false/disabled, 1 equals true/enabled

        public bool shield = false;
        public float shieldPower = 1020;

        public int scorei = 0;
        public int addedScore = 0;
        public int lives = 3;

        public bool
        tripleShot = false
        , fastRotate = false
        , messyScore = false
        , Parts = false
        , track = false
        , messyMeteors = false
        , bigShield = false
        , fastShoot = false
        , split = false
        , lifeFromShot = false
        , small = false;
        public GameScene()
        {
            AddGameObject(new Ship());
            AddGameObject(new MetorSpawner());
            AddGameObject(new backround());

            AddGameObject(new Score());
            shipEnable = 1;
        }
        public int vaporCount()
        {
            int a = 0;
            foreach(GameObject e in GetObjs())
            {
                if (e.HasTag("vapor")) a++;
            }
            return a;
        }
    }
}