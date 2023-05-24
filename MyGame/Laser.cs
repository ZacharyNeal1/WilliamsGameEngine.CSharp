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
        private readonly Sprite _sprite = new Sprite();
        static int _timeBeforeFollow;
        GameScene scene = (GameScene)Game.CurrentScene;

        public Laser(Vector2f pos, float rot, int timeBeforeFollow=1, int stay = 60)
        {
            _timeBeforeFollow = timeBeforeFollow;
            _sprite.Origin = new Vector2f(stay,0);
            _sprite.Position = pos;
            _sprite.Rotation = rot;
            //_sprite.Origin = new Vector2f(_sprite.TextureRect.Width / 2, _sprite.TextureRect.Height / 2);
        }
        public override void Draw()
        {
            //Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            if (_sprite.Origin.X >= 0) _sprite.Origin-=new Vector2f(1,0); else MakeDead();
            Vector2 pos = new Vector2(_sprite.Position.X, _sprite.Position.Y);
            var v1 = new LineC(pos, pos, Color.Green, "laser" ,this);
            
                if (pos.X < -100) pos.X = Game.RenderWindow.Size.X;
                if (pos.X > Game.RenderWindow.Size.X + 100) pos.X = 0;
                if (pos.Y < -100) pos.Y = Game.RenderWindow.Size.Y;
                if (pos.Y > Game.RenderWindow.Size.Y + 100) pos.Y = 0;



                scene.AddGameObject(v1);
                scene.AddGameObject(new LineC(new Vector2(pos.X,pos.Y), pos, Color.Green, "laser", this, 3));
                float x = Speed * MathF.Cos((float)(_sprite.Rotation * (Math.PI / 180.0f)));
                float y = Speed * MathF.Sin((float)(_sprite.Rotation * (Math.PI / 180.0f)));
                if (_timeBeforeFollow-- < 0)
                _sprite.Position = new Vector2f(pos.X + x, pos.Y + y);
            
        }
    }
}


