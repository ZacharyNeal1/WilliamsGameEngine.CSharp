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
    class Shield : GameObject
    {
        private const float Speed = 5.0f;
        private readonly Sprite _sprit = new Sprite();
        const int scale = 50;
        const int partCount = 5;
        GameScene scene = (GameScene)Game.CurrentScene;
        static Vector2[] pos =
        {
            new Vector2(scale, scale),
            new Vector2(scale*2, scale),
            new Vector2(scale*3, scale*2),
            new Vector2(scale*2, scale *3),
            new Vector2(scale, scale*3),
            new Vector2(0, scale)
        };
        public Shield()
        {
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprit);
        }
        public override void Update(Time elapsed)
        {
            if (scene.shieldPower > 2)
            {
                _sprit.Origin = new Vector2f(Ship.GetPos().X, Ship.GetPos().Y);
                var a = _sprit.Position;
                var b = Game.RenderWindow.Size;
                if (scene.shield == false) MakeDead();
                _sprit.Rotation += 0.05f;
                pos[0] = CirPos(pos[0], _sprit.Rotation, -30, _sprit.Origin, scale);
                pos[1] = CirPos(pos[1], _sprit.Rotation, 30, _sprit.Origin, scale);
                pos[2] = CirPos(pos[2], _sprit.Rotation, 90, _sprit.Origin, scale);
                pos[3] = CirPos(pos[3], _sprit.Rotation, 150, _sprit.Origin, scale);
                pos[4] = CirPos(pos[4], _sprit.Rotation, 210, _sprit.Origin, scale);
                pos[5] = CirPos(pos[5], _sprit.Rotation, 270, _sprit.Origin, scale);
                var color = new SFML.Graphics.Color(0, 0, (byte)scene.shieldPower);
                foreach (Vector2 e in pos)
                    scene.AddGameObject(new LineC(e, pos[Find(e, pos)], color, "shield", this, 3));
            }
        }
        static int Find(Vector2 e, Vector2[] pos)
        {
            if (pos.ToList().IndexOf(e) == pos.Length - 1) return 0; else return pos.ToList().IndexOf(e) + 1;
        }
        static Vector2 CirPos(Vector2 currentPos, float rot, float extraRot , Vector2f org, int rad)
        {
            return new Vector2(
                currentPos.X = (float)(rad * Math.Cos(rot + extraRot * (Math.PI / 180F)) + org.X),
                currentPos.Y = (float)(rad * Math.Sin(rot + extraRot * (Math.PI / 180F)) + org.Y));
        }

    }
}

