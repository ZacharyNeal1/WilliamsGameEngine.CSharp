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
    class Metor3 : GameObject
    {
        float Speed = 3f;
        int rot = 1;
        private readonly Sprite _sprit = new Sprite();
        private bool toPlayer = false;
        int scale = 80;
        public bool child = false;
        GameScene scene = (GameScene)Game.CurrentScene;
        int tim = 5;
        Vector2[] pos =
        {
            new Vector2(1,1)
        };
        Color[] colors =
        {
            Color.Magenta,
            Color.Cyan,
            Color.Yellow,
            Color.White,
            Color.Green
            
        };

        public Metor3(Vector2 b,bool c = false)
        {
            _sprit.Origin = Conv.ToVect2f(b);
            var a = Game.RenderWindow.Size;
            AssignTag("meteor");
            SetCollisionCheckEnabled(false);
            if (c)
            {
                scale = 40;
                Speed = 6;
                child = true;
                AssignTag("child");
                toPlayer = true;
            }
            pos = new Vector2[]
            { 
            new Vector2(scale* 3, scale),
            new Vector2((float)(scale * 1.5), scale),
            new Vector2((float)(scale * 1.5),0),
            new Vector2((float)(scale * 1.5), scale*3),
            new Vector2((float)(scale * 1.5), scale)
             };
            rot = new Random().Next(1, 360);
        }
        public override void Update(Time elapsed)
        {
            var a = _sprit.Origin;
            var b = Game.RenderWindow.Size;
            float x = Speed * MathF.Cos((float)(rot * (Math.PI / 180.0f)));
            float y = Speed * MathF.Sin((float)(rot * (Math.PI / 180.0f)));
            _sprit.Rotation += 0.1f;
            _sprit.Origin = new Vector2f(_sprit.Origin.X + x, _sprit.Origin.Y+ y);
            if (toPlayer)
            {
                pos[0] = CirPos(pos[0], _sprit.Rotation, 0, scale, _sprit.Origin);
                pos[1] = CirPos(pos[1], _sprit.Rotation, 180, scale, _sprit.Origin);
                pos[2] = CirPos(pos[2], _sprit.Rotation, 0, 0, _sprit.Origin);
                pos[3] = CirPos(pos[3], _sprit.Rotation, 90, scale, _sprit.Origin);
                pos[4] = CirPos(pos[4], _sprit.Rotation, -90, scale, _sprit.Origin);
                if (tim > 0) tim--; else
                foreach (Vector2 e in pos)
                scene.AddGameObject(new LineC(e, pos[Find(e, pos)], colors[new Random().Next(0, colors.Count())], "meteor", this));

                if (_sprit.Origin.X < -10) _sprit.Origin = new Vector2f(Game.RenderWindow.Size.X, _sprit.Origin.Y);
                if (_sprit.Origin.X > Game.RenderWindow.Size.X + 10) _sprit.Origin = new Vector2f(0, _sprit.Origin.Y);
                if (_sprit.Origin.Y < -10) _sprit.Origin = _sprit.Origin = new Vector2f(_sprit.Origin.X, Game.RenderWindow.Size.Y);
                if (_sprit.Origin.Y > Game.RenderWindow.Size.Y + 10) _sprit.Origin = new Vector2f(Game.RenderWindow.Size.X, 0);
            }
            else
                if ((a.X > b.X || a.X < 1 || a.Y > b.X || a.Y < 1))
                {
                    toPlayer = true;
                    rot = new Random().Next(1, 360);
                }
        }
        static int Find(Vector2 e, Vector2[] pos)
        {
            if (pos.ToList().IndexOf(e) == pos.Length-1) return pos.ToList().IndexOf(e); else return pos.ToList().IndexOf(e)+1;
        }
        static Vector2 CirPos(Vector2 currentPos,float rot,float extraRot, float rad , Vector2f org)
        {
            return new Vector2(
                currentPos.X = (float)(rad * Math.Cos(rot + extraRot * (Math.PI / 180F)) + org.X),
                currentPos.Y = (float)(rad * Math.Sin(rot + extraRot * (Math.PI / 180F)) + org.Y));
        }
    }
}

