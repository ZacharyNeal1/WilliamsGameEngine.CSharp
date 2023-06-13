using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net.Security;
using System.Numerics;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Schema;
using static System.Formats.Asn1.AsnWriter;

namespace MyGame
{
    public class Part : GameObject
    {
        private readonly Sprite _sprit = new Sprite();
        float speed = 5;
        float friction = 0.2f;
        string type = "";
        List<Vector2> forces = null;
        GameScene scene = (GameScene)Game.CurrentScene;
        string tag = "part";
        public Part(Vector2 pos, float rot,string typ = "", List<Vector2> tempAr=null)
        {
            int constantRot = new Random().Next(-60, 60);
            type = typ;

            if (tempAr != null)
            {
                forces = tempAr;
                //foreach (Vector2 e in tempAr) forces[tempAr.ToList().IndexOf(e) - 1] = new Vector2(e.X/5, e.Y - 90);
            }
            // quick overview
            //
            // when pressing E (from the ship) it makes 3 of instances of this class
            // this gives the "ship enable" a value of 1
            // the first part (instance) is given a "partvi" tag standing for "Part Vapor I" instead of "partv"
            //
            // the partvi is given a static speed of 5
            // 
            // after all parts are fired out (defined by the max amount shown by the ship class and lowwer down on this class)
            // and after E is released the parts come to the ship giving the "ship enable" a value of -1 allowing the parts to gain speed to the ship
            //
            // once the "partvi" part returns the ship enable goes to 0 and the ship's collion is renabled as well as all other parts killed

            _sprit.Color = new SFML.Graphics.Color(0, 255, 0);
            if (typ == "vapor")
            {
                AssignTag("vapor");
                {
                    tag = "partv";
                    speed = (float)new Random().NextDouble() * 10;
                    //insure there isnt too many particles already
                    if (scene.vaporCount() >= 60)
                    {
                        MakeDead();
                    }
                }
            }




            if (typ == "jet")
            {
                speed += 6;
                friction = 0.4f;
                _sprit.Color = new SFML.Graphics.Color(255, 0, 0, 255);
                constantRot = new Random().Next(-25, 25);
            }

            _sprit.Origin = new Vector2f(pos.X, pos.Y);
            _sprit.Rotation = rot + constantRot;

            _sprit.Origin +=
            new Vector2f(
            25 * MathF.Cos((float)(_sprit.Rotation * (Math.PI / 180.0f))),
            25 * MathF.Sin((float)(_sprit.Rotation * (Math.PI / 180.0f))));

            speed = (float)new Random().NextDouble() * 10;
            
        }
        public override void Update(Time elapsed)
        {
            var a = _sprit.Origin;
            //makes sure the particles dont go off screen
            var orgin = _sprit.Origin;
            if (_sprit.Origin.X < -10) _sprit.Origin = new Vector2f(Game.RenderWindow.Size.X, _sprit.Origin.Y);
            if (_sprit.Origin.X > Game.RenderWindow.Size.X) _sprit.Origin = new Vector2f(0, _sprit.Origin.Y);
            if (_sprit.Origin.Y < -10) _sprit.Origin = new Vector2f(_sprit.Origin.X, Game.RenderWindow.Size.Y);
            if (_sprit.Origin.Y > Game.RenderWindow.Size.Y) _sprit.Origin = new Vector2f(_sprit.Origin.X, 0);


            //gives the velocity from the orginial rotation
            _sprit.Origin += 
                new Vector2f(
                    speed * MathF.Cos((float)(_sprit.Rotation * (Math.PI / 180.0f))),
                    speed * MathF.Sin((float)(_sprit.Rotation * (Math.PI / 180.0f))));

            // makes the parts slow down over time
            if (speed > 1) speed -= friction;
            else {
                speed = 0;
                if (type == "jet") MakeDead();
            }


            //psuedo code :
            //
            // if (came from the ship pressing e) 
            //
            // if (this object has already been dirrected at the ship)
            // rotate it to the ship (for reassuance)
            // give it speed
            // dont allow the past velocity of the ship to affect it
            //
            // if (its time for the particles to return to the ship)
            // allow the if statement above to run
            //
            if (type == "vapor")
            {
                //copies the velocity of the ship
                if (forces != null) foreach (Vector2 e in forces)
                {
                    _sprit.Origin += new Vector2f(
                    e.X/5 * MathF.Cos((float)(e.Y * (Math.PI / 180.0f))),
                    e.X/5 * MathF.Sin((float)(e.Y * (Math.PI / 180.0f))));
                }
                if (scene.shipEnable == -1)
                {
                    Vector2 c = Conv.ToVect2(_sprit.Origin), d = Ship.GetPos();
                    int side = 0; //-1 left | 1 right
                    Vector2 wrap = new Vector2(0, 0);

                    if (Math.Abs(_sprit.Origin.X - Ship.GetPos().X) > Game.RenderWindow.Size.X / 2)
                    {
                        if (_sprit.Origin.X > Math.Abs(Game.RenderWindow.Size.X - _sprit.Origin.X))
                            side = (int)Game.RenderWindow.Size.X + 1;
                        else side = -11;


                        d = new Vector2(side, d.Y);
                    }
                    if (Math.Abs(_sprit.Origin.Y - Ship.GetPos().Y) > Game.RenderWindow.Size.Y / 2)
                    {
                        if (_sprit.Origin.Y > Math.Abs(Game.RenderWindow.Size.Y - _sprit.Origin.Y))
                            side = (int)Game.RenderWindow.Size.Y + 1;
                        else side = -11;

                        d = new Vector2(d.X, side);
                    }

                    _sprit.Origin +=
                        new Vector2f(
                            11 * MathF.Cos((float)((float)-((MathF.Atan2(c.X - d.X, c.Y - d.Y) + 90) * (180 / MathF.PI)) * (Math.PI / 180.0f))),
                            11 * MathF.Sin((float)((float)-((MathF.Atan2(c.X - d.X, c.Y - d.Y) + 90) * (180 / MathF.PI)) * (Math.PI / 180.0f))));

                }
                //if the main particle returned to ship (called by the line class colliders) kill the stranglers
                if (scene.shipEnable == 1) MakeDead();
            }



            // this is a badly formatted way of saying make a small line 
            scene.AddGameObject(
            new LineC(new Vector2(a.X, a.Y),
            new Vector2(a.X, a.Y),
            _sprit.Color,
            tag,
            this,
            3
            ));

        }
        static float Distance(Vector2 a, Vector2 b)
        {
            return (float)Math.Sqrt(Math.Abs(Math.Pow((b.X - a.X), 2) + Math.Pow((b.Y - a.Y), 2)));
        }

    }
}

