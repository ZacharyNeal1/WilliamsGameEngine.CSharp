using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Net;
using System.Net.Http.Headers;

namespace MyGame
{
    public class Laser : GameObject
    {
        private const float Speed = 20.0f;
        int stay = 60;
        private readonly Sprite _sprite = new Sprite();
        GameScene scene = (GameScene)Game.CurrentScene;

        public Laser(Vector2f pos, float rot)
        {
            _sprite.Origin = pos;
            _sprite.Rotation = rot;
        }

        public override void Update(Time elapsed)
        {
            if (stay != 0) stay--; else MakeDead();

            Vector2 pos = new Vector2(_sprite.Origin.X, _sprite.Origin.Y);
            
            if (pos.X < -100) pos.X = Game.RenderWindow.Size.X;
            if (pos.X > Game.RenderWindow.Size.X + 100) pos.X = 0;
            if (pos.Y < -100) pos.Y = Game.RenderWindow.Size.Y;
            if (pos.Y > Game.RenderWindow.Size.Y + 100) pos.Y = 0;

            scene.AddGameObject(new LineC(new Vector2(pos.X,pos.Y), pos, Color.Green, "laser", this, 3));
            float x = Speed * MathF.Cos((float)(_sprite.Rotation * (Math.PI / 180.0f)));
            float y = Speed * MathF.Sin((float)(_sprite.Rotation * (Math.PI / 180.0f)));

            _sprite.Origin = new Vector2f(pos.X + x, pos.Y + y);
        }
    }
}


