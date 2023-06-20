using GameEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Security;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static System.Formats.Asn1.AsnWriter;

namespace MyGame
{
    class metorSpawner : GameObject
    {
        private float delay = 10000;
        private int timer;
        public static Scene scene = (GameScene)Game.CurrentScene;
        public metorSpawner ()
        {
        }
        public override void Update(Time elapsed)
        {
            timer -= elapsed.AsMilliseconds();
            if (timer <= 0)
            {
                delay -= 0.5f;
                timer = (int)delay;
                Vector2u size = Game.RenderWindow.Size;
                Game.CurrentScene.AddGameObject(new Metor());
                Game.CurrentScene.AddGameObject(new Metor1());
                Game.CurrentScene.AddGameObject(new Metor2());
                Game.CurrentScene.AddGameObject(new Metor3(Conv.ToVect2(Game.RenderWindow.Size) / 2));
            }
        }
    }
}
