using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = SFML.Graphics.Color;
using SFML.Graphics.Glsl;
using System.Numerics;

namespace MyGame
{
    class LineC : GameObject
    {

        private readonly Sprite _sprite = new Sprite();
        private readonly Sprite _sprite2 = new Sprite();
        private GameObject parent;
        private int stay = 1;
        private const int secondLineWidth = 21, secondLineAlpha = 64;
        //second line width is made to be a uneven number so you have have a equal amout of pixels
        // on ethier side of the first line
        public LineC(Vector2 a, Vector2 b, Color c, string tag = "", GameObject parent = null, int line = 1, bool col = true)
        {
            float distance = line + (float)Math.Sqrt(Math.Abs(Math.Pow((b.X - a.X), 2) + Math.Pow((b.Y - a.Y), 2)));
            AssignTag(tag);
            this.parent = parent;



            _sprite.TextureRect = (IntRect)new FloatRect(new Vector2f(a.X, a.Y), new Vector2f(1, distance));
            _sprite.Texture = Game.GetTexture("Resources/pixel.png");
            _sprite.Origin = new Vector2f(_sprite.TextureRect.Width / 2, 0);
            _sprite.Position = new Vector2f(a.X, a.Y);
            _sprite.Rotation = (float)-(MathF.Atan2(b.X - a.X, b.Y - a.Y) * (180 / MathF.PI));
            _sprite.Color = new Color(c.R, c.G, c.B, 255);

            _sprite2.TextureRect = (IntRect)new FloatRect(new Vector2f(a.X, a.Y), new Vector2f(secondLineWidth, distance));
            _sprite2.Origin = new Vector2f(_sprite2.TextureRect.Width / 2, 0);
            _sprite2.Texture = Game.GetTexture("Resources/pixel.png");
            _sprite2.Position = new Vector2f(a.X, a.Y);
            _sprite2.Rotation = (float)-(MathF.Atan2(b.X - a.X, b.Y - a.Y) * (180 / MathF.PI));
            _sprite2.Color = new Color(c.R, c.G, c.B, secondLineAlpha);
            //Console.WriteLine(distance + "dist");
            if (col == true)
            SetCollisionCheckEnabled(true);
        }

        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
            Game.RenderWindow.Draw(_sprite2);
        }

        public override void Update(Time elapsed)
        {
            if (stay != 0) stay--; else MakeDead();
        }
        public override FloatRect GetCollisionRect()
        {
            return _sprite2.GetGlobalBounds();
        }
        public override void HandleCollision(GameObject otherGameObject)
        {
            if (otherGameObject.HasTag("meteor") && HasTag("shield"))
            {
                LineC a = (LineC)otherGameObject;
                a.parent.MakeDead();
            }
            if (otherGameObject.HasTag("ship") && HasTag("meteor"))
            {
                LineC a = (LineC)otherGameObject;
                parent.MakeDead();
            }
            if (otherGameObject.HasTag("meteor") && HasTag("laser"))
            {
                parent.MakeDead();
                LineC a = (LineC)otherGameObject;
                a.parent.MakeDead();
            }

        }
    }

}
