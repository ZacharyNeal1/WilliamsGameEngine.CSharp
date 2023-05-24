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
    class Metor : GameObject
    {
        private const float Speed = 5.0f;
        private readonly Sprite _sprit = new Sprite();
        private bool toPlayer = false;
        const int scale = 50;
        GameScene scene = (GameScene)Game.CurrentScene;
        Vector2[] pos =
        {
            new Vector2(0, scale),
            new Vector2(scale* 3, scale),
            new Vector2((float)(scale * 1.5), scale),
            new Vector2((float)(scale * 1.5),0),
            new Vector2((float)(scale * 1.5), scale*3),
            new Vector2((float)(scale * 1.5), scale)
        };
        public Metor()
        {
            _sprit.Origin = new Vector2f(scale*2, 2*(float)(scale * 1.5));
            var a = Game.RenderWindow.Size;
            AssignTag("meteor");
            SetCollisionCheckEnabled(false);
            _sprit.Rotation = new Random().Next(1, 360);
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprit);
        }
        public override void Update(Time elapsed)
        {
            float x = Speed * MathF.Cos((float)(_sprit.Rotation * (Math.PI / 180.0f)));
            float y = Speed * MathF.Sin((float)(_sprit.Rotation * (Math.PI / 180.0f)));

            var a = _sprit.Position;
            var b = Game.RenderWindow.Size;
            _sprit.Origin += new Vector2f(1.0f, 1.0f);
            Vector2 p = new Vector2(_sprit.Position.X, _sprit.Position.Y);
            _sprit.Rotation += 0.2f;
            pos[0] = CirPos(pos[0], _sprit.Rotation, 0,scale,_sprit.Origin);
            pos[1] = CirPos(pos[1],_sprit.Rotation,180,scale, _sprit.Origin);
            pos[2] = CirPos(pos[2], _sprit.Rotation, 0,0, _sprit.Origin);
            pos[3] = CirPos(pos[3], _sprit.Rotation, 90,scale, _sprit.Origin);
            pos[4] = CirPos(pos[4], _sprit.Rotation , -90,scale , _sprit.Origin);
            pos[5] = CirPos(pos[5], _sprit.Rotation, 0,0, _sprit.Origin);
            foreach (Vector2 e in pos)
            scene.AddGameObject(new LineC(e, pos[Find(e, pos)], Color.Red, "meteor", this));
            if ((a.X > b.X || a.X < 1 || a.Y > b.X || a.Y < 1) && toPlayer == true)
            {
                if (_sprit.Origin.X < -100) _sprit.Origin = new Vector2f(Game.RenderWindow.Size.X,_sprit.Origin.Y);
                if (_sprit.Origin.X > Game.RenderWindow.Size.X + 100) _sprit.Origin = new Vector2f(0, _sprit.Origin.Y);
                if (_sprit.Origin.Y < -100) _sprit.Origin = _sprit.Origin = new Vector2f(_sprit.Origin.X, Game.RenderWindow.Size.Y);
                if (_sprit.Origin.Y > Game.RenderWindow.Size.Y + 100) _sprit.Origin = new Vector2f(Game.RenderWindow.Size.X, 0);
            }
            if ((a.X > b.X || a.X < 1 || a.Y > b.X || a.Y < 1) && toPlayer == false)
            {
                toPlayer = true;
                _sprit.Rotation = new Random().Next(1, 360);
            }
        }
        static int Find(Vector2 e, Vector2[] pos)
        {
            if (pos.ToList().IndexOf(e) == pos.Length-1) return 0; else return pos.ToList().IndexOf(e)+1;
        }
        static Vector2 CirPos(Vector2 currentPos,float rot,float extraRot, float rad , Vector2f org)
        {
            return new Vector2(
                currentPos.X = (float)(rad * Math.Cos(rot + extraRot * (Math.PI / 180F)) + org.X),
                currentPos.Y = (float)(rad * Math.Sin(rot + extraRot * (Math.PI / 180F)) + org.Y));
        }
        static float Distance(Vector2 a, Vector2 b)
        {
            return (float)Math.Sqrt(Math.Abs(Math.Pow((b.X - a.X), 2) + Math.Pow((b.Y - a.Y), 2)));
        }

    }
}

