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
        private int secondLineWidth = 21, secondLineAlpha = 64;
        //second line width is made to be a uneven number so you have have a equal amout of pixels
        // on ethier side of the first line
        public LineC(Vector2 a, Vector2 b, Color c, string tag = "", GameObject parent = null, float line = 1)
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


            if (HasTag("scoreText")) secondLineWidth /= 2;
            if (HasTag("meteor2")) secondLineWidth /= 2;
            _sprite2.TextureRect = (IntRect)new FloatRect(new Vector2f(a.X, a.Y), new Vector2f(secondLineWidth, distance));
            _sprite2.Origin = new Vector2f(_sprite2.TextureRect.Width / 2, 0);
            _sprite2.Texture = Game.GetTexture("Resources/pixel.png");
            _sprite2.Position = new Vector2f(a.X, a.Y);
            _sprite2.Rotation = (float)-(MathF.Atan2(b.X - a.X, b.Y - a.Y) * (180 / MathF.PI));
            _sprite2.Color = new Color(c.R, c.G, c.B, (byte)secondLineAlpha);
            if (tag == "laser") _sprite.Scale = new Vector2f(2,2);
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
            return _sprite.GetGlobalBounds();
        }
        public override void HandleCollision(GameObject otherGameObject)
        {
            if (otherGameObject.HasTag("meteor") && HasTag("shield"))
            {
                LineC a = (LineC)otherGameObject;
                
                GameScene scene = (GameScene)Game.CurrentScene;
                if (!a.parent.IsDead())
                scene.shieldPower -= 25;
                a.parent.MakeDead();
            }
            if (otherGameObject.HasTag("ship") && HasTag("meteor"))
            {
                GameScene scene = (GameScene)Game.CurrentScene;
                if (scene.shipEnable == 1 && !parent.IsDead() && !otherGameObject.IsDead()) 
                {
                    scene.AddGameObject(new explode(Conv.ToVect2f(Ship.GetPos()), 30, 55, 4.5f,30));
                    parent.MakeDead();
                } else scene.addedScore += 10;
            }
            if (otherGameObject.HasTag("laser") && HasTag("meteor"))
            {
                LineC a = (LineC)otherGameObject;
                var scene = (GameScene)Game.CurrentScene;
                GameObject temp = otherGameObject;
                if (!parent.IsDead() && !a.parent.IsDead())
                {
                    scene.addedScore += 1000;
                    scene.AddGameObject(new explode(new Vector2f( temp.GetCollisionRect().Left,temp.GetCollisionRect().Top), 15, 5, 3.5f, 10));
                    switch (parent)
                    {
                        case Metor1: scene.addedScore += 500; break;
                        case Metor2: scene.addedScore += 9999; break;
                    }

                }
                parent.MakeDead();
                a.parent.MakeDead();
            }
            if (HasTag("partv") && otherGameObject.HasTag("ship"))
            {
                parent.MakeDead();
                GameScene scene = (GameScene)Game.CurrentScene;
                if (scene.vaporCount() < 1) scene.shipEnable = 1;
            }
        }
    }

}
