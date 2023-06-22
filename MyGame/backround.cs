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
    class backround : GameObject
    {
        private int points = 20;
        public static Scene scene = (GameScene)Game.CurrentScene;
        List<Vector2> stars = new List<Vector2>();
        static Vector2 newRandom()
        {
            return new Vector2(new Random().Next(0, (int)Game.RenderWindow.Size.X), new Random().Next(0, (int)Game.RenderWindow.Size.Y));
        }

        public backround()
        {
            for (int i = 0; i <=points; i++)stars.Add(newRandom());
        }
        public override void Update(Time elapsed)
        {
            foreach (Vector2 x in stars)
                scene.AddGameObject(new LineC(x, x, Color.White, "star", this, 2));
        }
    }
}
