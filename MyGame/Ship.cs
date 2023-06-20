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

        const int delay = 30;
        int timer = delay;
        int vaporTimer = 60;

        int boostTimer = 100;

        bool show = true   ;

        const int scale = 25;
        GameScene scene = (GameScene)Game.CurrentScene;
        List<Vector2> forces = new List<Vector2>();
        Vector2[] pos =
        {
            new Vector2(0, 0),
            new Vector2(scale*1.2f, scale * 0.5f),
            new Vector2(0, scale)
        };
        public Ship()
        {
            scene = (GameScene)Game.CurrentScene;
            _sprit.Rotation = 90;
            _sprit.Origin = new Vector2f(0, scale*0.5f);
            
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
                e.X/5 * MathF.Cos((float)(e.Y * (Math.PI / 180.0f))),
                e.X/5 * MathF.Sin((float)(e.Y * (Math.PI / 180.0f))));
            }

            float decay = 0.995f;
            if (scene.shipEnable != 1) decay = 0.989f;
            for (int i = 0; i < forces.Count(); i++) if (forces[i].X < 0.01f) forces.RemoveAt(i);
                else forces[i] = new Vector2(forces[i].X * decay - 0.01f, forces[i].Y);

            if (timer > 0) timer--;
            float total = 0;
            foreach (Vector2 e in forces) total += e.X;
            if (scene.shieldPower < 1019) scene.shieldPower+=(0.25f + (total/100));
            //Console.WriteLine(total);
            if (vaporTimer > 0) vaporTimer--;

            //input
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                {
                if (scene.shipEnable == 1)
                forces.Add(new Vector2(2f, _sprit.Rotation+90));
                else
                    forces.Add(new Vector2(0.2f, _sprit.Rotation + 90));
                scene.AddGameObject(new Part(Conv.ToVect2(_sprit.Origin), _sprit.Rotation-90, "jet", forces));
                }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                if (scene.shieldPower > 0)
                {
                    scene.shieldPower -= 4;
                    if (scene.shield == false)
                    {
                        scene.shield = true;
                        scene.AddGameObject(new Shield());
                    }
                }
            }
            else scene.shield = false;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Q)) {
                if (boostTimer <= 0)
                {
                    boostTimer = 100;
                    float combined = 0;
                    foreach (Vector2 e in forces)
                    {
                        combined += e.X / 3;
                    }
                    forces.Add(new Vector2(combined, _sprit.Rotation + 90));
                }
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) {_sprit.Rotation += -3; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.R)) { scene.AddGameObject(new explode(_sprit.Origin,30,70,4.5f,60)); }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) {_sprit.Rotation += 3; }
            
                if (Keyboard.IsKeyPressed(Keyboard.Key.Space)) 
            {
                if (timer == 0) {
                    timer = delay;
                    scene.AddGameObject(new Laser(pos[1], _sprit.Rotation - 270));
                    forces.Add(new Vector2(10f, _sprit.Rotation + 270));
                }
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.E))
            {
                if ((scene.shipEnable == 1 || scene.vaporCount() <1 )&&vaporTimer <1)
                {
                    scene.shipEnable = 0;
                    forces.Add(new Vector2(40f, _sprit.Rotation + 90));
                    for (int i = 0; i < 20; i++)
                    {
                        int rotation = 0;
                        foreach (Vector2 e in pos)
                        {
                            scene.AddGameObject(new Part(e,rotation + _sprit.Rotation, "vapor",forces));
                            rotation += 90;
                        }
                    }
                        
                }
            }
            else
            {
                if (scene.shipEnable == -1 && scene.vaporCount() == 0) scene.shipEnable = 1;
                if (scene.shipEnable == 0)
                {
                    vaporTimer = 50;
                    scene.shipEnable = -1;
                }
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape)) { Environment.Exit(0); }

            pos[0] = CirPos(_sprit.Rotation,scale*0.5f, _sprit.Origin);
            pos[1] = CirPos(_sprit.Rotation+90, scale * 1.2f, _sprit.Origin);
            pos[2] = CirPos(_sprit.Rotation+180, scale * 0.5f, _sprit.Origin);

            Color col = new Color(0, 64, 0);
            if (scene.shipEnable == 1) col = new Color(0, 255, 0);

            foreach (Vector2 e in pos)
                    scene.AddGameObject(new LineC(e, pos[Find(e, pos)], col, "ship", this));

            if (show)
            {
                Vector2 pos1 = CirPos(_sprit.Rotation + 90, scale * 2.4f, _sprit.Origin);
                Vector2 pos2 = CirPos(_sprit.Rotation + 180, scale * 1f, _sprit.Origin);
                float dist = (float)Math.Sqrt(Math.Abs(Math.Pow((pos2.X - pos1.X), 2) + Math.Pow((pos2.Y - pos1.Y), 2)))-scale;
                Color color = new Color(0, 0, 255);
                //if (scene.shieldPower) color = new Color(128, 128, 255);
                scene.AddGameObject(new LineC(pos2, pos1, color, "", this, ((scene.shieldPower/1020f)*dist)-(scale*2.6f)));
            }

            if (_sprit.Origin.X < -10) _sprit.Origin = new Vector2f(Game.RenderWindow.Size.X, _sprit.Origin.Y);
            if (_sprit.Origin.X > Game.RenderWindow.Size.X + 10) _sprit.Origin = new Vector2f(0, _sprit.Origin.Y);
            if (_sprit.Origin.Y < -10) _sprit.Origin = _sprit.Origin = new Vector2f(_sprit.Origin.X, Game.RenderWindow.Size.Y);
            if (_sprit.Origin.Y > Game.RenderWindow.Size.Y + 10) _sprit.Origin = new Vector2f(Game.RenderWindow.Size.X, 0);

            if (_sprit.Origin.X < 600)
                scene.AddGameObject(new LineC(new Vector2(Game.RenderWindow.Size.X-50,_sprit.Origin.Y), new Vector2(Game.RenderWindow.Size.X, _sprit.Origin.Y), Color.Green));
            if (_sprit.Origin.X > Game.RenderWindow.Size.X - 600)
                scene.AddGameObject(new LineC(new Vector2(0, _sprit.Origin.Y), new Vector2(50, _sprit.Origin.Y), Color.Green));
            if (_sprit.Origin.Y > Game.RenderWindow.Size.Y - 600)
                scene.AddGameObject(new LineC(new Vector2(_sprit.Origin.X, 0), new Vector2(_sprit.Origin.X, 50), Color.Green));
            if (_sprit.Origin.Y < 600)
                scene.AddGameObject(new LineC(new Vector2(_sprit.Origin.X, Game.RenderWindow.Size.Y), new Vector2(_sprit.Origin.X, Game.RenderWindow.Size.Y-50), Color.Green));
        }
        static int Find(Vector2 e, Vector2[] pos)
        {
            if (pos.ToList().IndexOf(e) == pos.Length - 1) return 0; else return pos.ToList().IndexOf(e)+1;
        }
        static Vector2 CirPos(float rot, float rad, Vector2f center)
        {
            return new Vector2(
                center.X+rad * MathF.Cos(rot  * (MathF.PI / 180)),
                center.Y+rad * MathF.Sin(rot  * (MathF.PI / 180))
                );
        }
    }
}

