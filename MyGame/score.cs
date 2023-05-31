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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static System.Formats.Asn1.AsnWriter;

namespace MyGame
{
    class Score : GameObject
    {
        private const float Speed = 5.0f;
        private readonly Sprite _sprit = new Sprite();
        const float scale = 25;
        const int partCount = 5;
        static Vector2 startingPos = new Vector2(1920/2-50, 25);

        Vector2[] text0 =              
            { // 0
                new Vector2(0,0),
                new Vector2(scale*1, 0),
                new Vector2(scale*1, scale*2),
                new Vector2(0, scale*2),
                new Vector2(0,0)
            };
        Vector2[] text1 =
            { // 1
                new Vector2(scale*1,0),
                new Vector2(scale*1, scale*2)
            };
        Vector2[] text2 =
            { // 2
                new Vector2(0, 0),
                new Vector2(scale * 1,0),
                new Vector2(scale*1, scale),
                new Vector2(0, scale),

                new Vector2(0, scale*2),
                new Vector2(scale*1,scale* 2)
            };
        Vector2[] text3 =
            {//3
                new Vector2(0, 0),
                new Vector2(scale * 1,0),
                new Vector2(scale * 1, scale),
                new Vector2(0, scale*1),

                new Vector2(scale * 1, scale),

                new Vector2(scale*1, scale*2),
                new Vector2(scale*1, scale),
                new Vector2(0,scale* 2),
                new Vector2(scale*1, scale*2)



            };
        Vector2[] text4 =
            { // 4
                new Vector2(0, 0),
                new Vector2(0,scale),
                new Vector2(scale*1, scale),
                new Vector2(scale*1, 0),

                new Vector2(scale*1, scale*2),
                new Vector2(scale*1,scale* 2)

            };
        Vector2[] text5 =
        { // 5
                new Vector2(scale, 0),
                new Vector2(0,0),
                new Vector2(0, scale),
                new Vector2(scale*1, scale),

                new Vector2(scale*1, scale*2),
                new Vector2(0,scale* 2)

            };
        Vector2[] text6 =
        { // 6
                new Vector2(scale, 0),
                new Vector2(0,0),
                new Vector2(0, scale*1),
                new Vector2(scale*1, scale*1),

                new Vector2(scale*1, scale*2),
                new Vector2(0,scale* 2),
                new Vector2(0,scale* 1)

        };
        Vector2[] text7 =
        { // 7
                new Vector2(0, 0),
                new Vector2(scale,0),
                new Vector2(scale, scale*2)

        };
        Vector2[] text8 =
         { // 8
                new Vector2(0, 0),
                new Vector2(scale,0),
                new Vector2(scale, scale*2),
                new Vector2(0, scale*2),
                new Vector2(0, 0),
                new Vector2(0, scale*1),
                new Vector2(scale, scale*1),

            };
        Vector2[] text9 =
         { // 9
                new Vector2(0, 0),
                new Vector2(scale,0),
                new Vector2(scale, scale*2),
                new Vector2(0, scale*2),
                new Vector2(scale, scale*2),
                new Vector2(scale, scale),
                new Vector2(0, scale),
                new Vector2(0, 0),
            };
        Vector2[] text10 =
        {// ,
                new Vector2((float)(scale*1.5), scale*2),
                new Vector2((float)(scale * 1.5), (float)(scale*2.5)),

            };
        public Score()
        {
            _sprit.Origin = new Vector2f(4, 4);
        }
        public override void Update(Time elapsed)
        {
            GameScene scene = (GameScene)Game.CurrentScene;

            if (scene.addedScore > 0)
            {
                if (scene.addedScore > 55)
                {
                    scene.addedScore -= 55;
                    scene.scorei += 55;
                }
                else {
                    scene.scorei += scene.addedScore;
                    scene.addedScore = 0;
                }
            }

            int len = scene.scorei.ToString().Length;
            for (int i = 0; i < scene.scorei.ToString().Length; i++)
            {
                if (i % 3 == 0 && i != scene.scorei.ToString().Length-1) Drawi(text10, i, len);
                switch (scene.scorei.ToString()[i])
                {
                    case '0': Drawi(text0,i,len); break;
                    case '1': Drawi(text1,i,len); break;
                    case '2': Drawi(text2,i, len); break;
                    case '3': Drawi(text3,i, len); break;
                    case '4': Drawi(text4,i, len); break;
                    case '5': Drawi(text5,i, len); break;
                    case '6': Drawi(text6,i, len); break;
                    case '7': Drawi(text7, i, len); break;
                    case '8': Drawi(text8, i, len); break;
                    case '9': Drawi(text9, i, len); break;
                }
            }
        }

        static void Drawi(Vector2[] e, int i, int len)
        {
            GameScene scene = (GameScene)Game.CurrentScene;
            Vector2 vector2 = new Vector2(i * (scale*2), 0);
            Vector2 Pos = startingPos - new Vector2(len * scale, 0);
            foreach (Vector2 a in e) scene.AddGameObject(new LineC(a + Pos+vector2, e[Find(a, e)] + Pos+vector2, Color.White, "scoreText", null, 1));
           
        }
        static int Find(Vector2 e, Vector2[] pos)
        {
            if (pos.ToList().IndexOf(e) == pos.Length -1) return pos.ToList().IndexOf(e); else return pos.ToList().IndexOf(e)+1;
        }
    }
}

