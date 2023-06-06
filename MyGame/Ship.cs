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
    class Ship : GameObject
    {
        public static readonly Sprite _sprit = new Sprite();
        const int scale = 25;
        GameScene scene = (GameScene)Game.CurrentScene;
        List<Vector2> forces = new List<Vector2>();
        Vector2[] pos =
        {
            new Vector2(0, 0),
            new Vector2(scale*2.2f, scale * 0.5f),
            new Vector2(0, scale)
        };
        public Ship()
        {
            scene = (GameScene)Game.CurrentScene;
        }
        public static Vector2 GetPos ()
        {
            return Conv.ToVect2(_sprit.Origin);
        }
        public override void Update(Time elapsed)

        {
            scene = (GameScene)Game.CurrentScene;
            foreach (Vector2 e in forces)
            {
                _sprit.Origin += new Vector2f (
                e.X/3 * MathF.Cos((float)(e.Y * (Math.PI / 180.0f))),
                e.X/3 * MathF.Sin((float)(e.Y * (Math.PI / 180.0f))));
            }
            for (int i = 0; i < forces.Count(); i++) if (forces[i].X < 0.01f) forces.RemoveAt(i);
                else forces[i] = new Vector2((float)Math.Pow(forces[i].X, 0.5)-0.01f, forces[i].Y);

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                {
                forces.Add(new Vector2(0.1f, _sprit.Rotation));
                    scene.AddGameObject(new Part(Conv.ToVect2(_sprit.Position), _sprit.Rotation, "jet", forces.ToArray()));
                }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) { }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) { _sprit.Rotation += -3; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) { _sprit.Rotation += 3; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape)) { Environment.Exit(0); }

            pos[0] = CirPos(pos[0], _sprit.Rotation, 0, scale, _sprit.Origin);
                pos[1] = CirPos(pos[1], _sprit.Rotation, 180, scale, _sprit.Origin);
                pos[2] = CirPos(pos[2], _sprit.Rotation, 0, 0, _sprit.Origin);

                foreach (Vector2 e in pos)
                    scene.AddGameObject(new LineC(e, pos[Find(e, pos)], Color.Green, "ship", this));

                if (_sprit.Origin.X < -10) _sprit.Origin = new Vector2f(Game.RenderWindow.Size.X, _sprit.Origin.Y);
                if (_sprit.Origin.X > Game.RenderWindow.Size.X + 10) _sprit.Origin = new Vector2f(0, _sprit.Origin.Y);
                if (_sprit.Origin.Y < -10) _sprit.Origin = _sprit.Origin = new Vector2f(_sprit.Origin.X, Game.RenderWindow.Size.Y);
                if (_sprit.Origin.Y > Game.RenderWindow.Size.Y + 10) _sprit.Origin = new Vector2f(Game.RenderWindow.Size.X, 0);
        }
        static int Find(Vector2 e, Vector2[] pos)
        {
            if (pos.ToList().IndexOf(e) == pos.Length - 1) return 0; else return pos.ToList().IndexOf(e);
        }
        static Vector2 CirPos(Vector2 currentPos, float rot, float extraRot, float rad, Vector2f org)
        {
            return new Vector2(
                currentPos.X = (float)(rad * Math.Cos(rot + extraRot * (Math.PI / 180F)) + org.X),
                currentPos.Y = (float)(rad * Math.Sin(rot + extraRot * (Math.PI / 180F)) + org.Y));
        }
    }
}

