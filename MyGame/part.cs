using GameEngine;
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
    public class Part : GameObject
    {
        private const float Speed = 5.0f;
        private readonly Sprite _sprit = new Sprite();
        const int scale = 50;
        float speed = 1;
        const float friction = 0.3f;
        GameScene scene = (GameScene)Game.CurrentScene;
        public Part(Vector2 pos, float rot)
        {
            _sprit.Origin = new Vector2f(pos.X, pos.Y);
            _sprit.Rotation = rot += new Random().Next(-30, 30);
            
        }
        public override void Update(Time elapsed)
        {

            var a = _sprit.Origin;
            var b = Game.RenderWindow.Size;

            _sprit.Origin += 
                new Vector2f(
                    Speed * MathF.Cos((float)(_sprit.Rotation * (Math.PI / 180.0f))),
                    Speed * MathF.Sin((float)(_sprit.Rotation * (Math.PI / 180.0f))));

            if (speed > 0) { speed -= friction; } else { speed = 0; }

            scene.AddGameObject(
                new LineC(new Vector2(a.X, a.Y),
                new Vector2(a.X, a.Y),
                Color.Blue,
                "part",
                this,
                3,
                false
                ));
        }

    }
}

