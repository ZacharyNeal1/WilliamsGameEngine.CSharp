using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
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

    class explode : GameObject
    {
        private readonly Sprite _sprit = new Sprite();
        const int scale = 75;
        GameScene scene = (GameScene)Game.CurrentScene;
        List<Vector2[]> lines = new List<Vector2[]>();
        int lifetime = 0;
        float decay = 0.97f;
        Color col = new Color(255,128,0);
        string tag = "explode";
        float blue = 0;

        public explode(Vector2f pos, int lineCount, float cirScale, float speed, int life)
        {
            lifetime = life;
            col = new Color(255, (byte)new Random().Next(100, 140), 0);
            for (int i = 0; i <= lineCount; i++)
            {
                int random = new Random().Next(1,360);
                Vector2 e = CirPos(random, cirScale, Conv.ToVect2(pos));
                Vector2 item = new Vector2(e.X, e.Y);
                lines.Add(new Vector2[] { item, item, new Vector2(new Random().Next((int)(speed/1.5f),(int)(speed*1.5f)),random) });
            }
        }
        public override void Update(Time elapsed)
        {
            blue += 0.25f;
            if (lifetime <= 0) col = new Color((byte)(col.R - 4), (byte)(col.G-2), (byte)(blue)); else lifetime--;
            if (col.R < 16) MakeDead();
            if (col.G < 16) col = new Color(col.R, 3, 0);
            lines.ForEach(x => { x[1] = CirPos(x[2].Y, Dist(x[0], x[1]) + x[2].X, x[0]); x[2] = new Vector2(x[2].X* decay, x[2].Y); });
            foreach (Vector2[] e in lines) scene.AddGameObject(new LineC(e[0], e[1], col, tag, this));
        }
        static Vector2 CirPos(float rot, float rad, Vector2 center)
        {
            return new Vector2(
                center.X + rad * MathF.Cos(rot * (MathF.PI / 180)),
                center.Y + rad * MathF.Sin(rot * (MathF.PI / 180))
                );
        }
        static float Dist(Vector2 pos1, Vector2 pos2)
        {
            return (float)Math.Sqrt(Math.Abs(Math.Pow((pos2.X - pos1.X), 2) + Math.Pow((pos2.Y - pos1.Y), 2)));
        }
    }
}

