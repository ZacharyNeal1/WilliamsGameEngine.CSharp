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
    class Metor1 : GameObject
    {
        float Speed = 8.0f;
        int rot;
        private readonly Sprite _sprit = new Sprite();
        private bool toPlayer = false;
        const int scale = 37;
        GameScene scene = (GameScene)Game.CurrentScene;
        Vector2[] pos =
        {
            new Vector2(scale, scale),
            new Vector2(scale*2, scale),
            new Vector2(scale*3, scale*2),
            new Vector2(scale*2, scale *3),
            new Vector2(scale, scale*3),
            new Vector2(0, scale)
        };
        public Metor1()
        {
            _sprit.Origin = Conv.ToVect2f(Conv.ToVect2(Game.RenderWindow.Size)/2);
            var a = Game.RenderWindow.Size;
            AssignTag("meteor");
            SetCollisionCheckEnabled(false);
            rot = new Random().Next(1, 360);
            scene = (GameScene)Game.CurrentScene;
        }
        public override void Update(Time elapsed)
        {
            //toPlayer = true;
            var a = _sprit.Origin;
            var b = Game.RenderWindow.Size;
            Vector2f c = Conv.ToVect2f(Ship.GetPos());
            Vector2f d = _sprit.Origin;


            float x = Speed * MathF.Cos((float)(rot * (Math.PI / 180.0f)));
            float y = Speed * MathF.Sin((float)(rot * (Math.PI / 180.0f)));

            _sprit.Origin += new Vector2f(x,y);

            _sprit.Rotation += 0.05f;
            if (toPlayer)
            {
                rot = (int)-(MathF.Atan2(c.X - d.X, c.Y - d.Y) * (180 / MathF.PI));
                rot += 90;

                pos[0] = CirPos(pos[0], _sprit.Rotation, -30, _sprit.Origin, scale);
                pos[1] = CirPos(pos[1], _sprit.Rotation, 30, _sprit.Origin, scale);
                pos[2] = CirPos(pos[2], _sprit.Rotation, 90, _sprit.Origin, scale);
                pos[3] = CirPos(pos[3], _sprit.Rotation, 150, _sprit.Origin, scale);
                pos[4] = CirPos(pos[4], _sprit.Rotation, 210, _sprit.Origin, scale);
                pos[5] = CirPos(pos[5], _sprit.Rotation, 270, _sprit.Origin, scale);

                foreach (Vector2 e in pos)
                scene.AddGameObject(new LineC(e, pos[Find(e, pos)], Color.Yellow, "meteor", this));

                //if ((a.X > b.X+200 || a.X < -200 || a.Y > b.X+200 || a.Y < -200)) MakeDead();

            }
            else
                if ((a.X > b.X || a.X < 1 || a.Y > b.X || a.Y < 1))
                {
                    toPlayer = true;

                    Speed = 3.0f;
                }
        }
        static int Find(Vector2 e, Vector2[] pos)
        {
            if (pos.ToList().IndexOf(e) == pos.Length-1) return 0; else return pos.ToList().IndexOf(e)+1;
        }
        static Vector2 CirPos(Vector2 currentPos, float rota, float extraRot, Vector2f org, int rad)
        {
            return new Vector2(
                currentPos.X = (float)(rad * Math.Cos(rota + extraRot * (Math.PI / 180F)) + org.X),
                currentPos.Y = (float)(rad * Math.Sin(rota + extraRot * (Math.PI / 180F)) + org.Y));
        }
    }
}

