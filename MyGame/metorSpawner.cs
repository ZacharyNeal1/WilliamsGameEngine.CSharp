using GameEngine;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class metorSpawner : GameObject
    {
        private float delay = 10000;
        private int timer;
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
            }
        }
    }
}
