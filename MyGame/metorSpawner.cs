﻿using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Security;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static System.Formats.Asn1.AsnWriter;

namespace MyGame
{
    class MetorSpawner : GameObject
    {
        private float delay = 15000;
        private int timer;
        GameScene scene = (GameScene)Game.CurrentScene;
        public MetorSpawner ()
        {
            

            delay = 15000;


        }
        public override void Update(Time elapsed)
        {
            scene = (GameScene)Game.CurrentScene;
            timer -= elapsed.AsMilliseconds();
            if (timer <= 0)
            {
                delay -= 0.5f;
                timer = (int)delay;
                int count = 4;
                for (int i = 0; i < count; i ++)
                switch (new Random().Next(1, 4)) 
                {
                    case 1:
                scene.AddGameObject(new Metor()); break;
                    case 2:
                scene.AddGameObject(new Metor1()); break;
                    case 3:
                scene.AddGameObject(new Metor2()); break;
                    case 4:
                scene.AddGameObject(new Metor3(Conv.ToVect2(Game.RenderWindow.Size) / 2)); break;
            }
            }
        }
    }
}
