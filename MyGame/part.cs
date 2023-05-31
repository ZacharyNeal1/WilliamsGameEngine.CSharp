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
        Vector2[] forces = null;
        GameScene scene = (GameScene)Game.CurrentScene;
        string tag = "part";
        bool cycle = false;
        public Part(Vector2 pos, float rot,string typ = "", Vector2[] tempAr = null)
        {
            int constantRot = new Random().Next(-60, 60);
            type = typ;
            forces = tempAr;

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
            if (typ == "vapor") if (scene.vaporCount() == 1) tag = "partvi"; else
                {
                    tag = "partv";
                    speed = (float)new Random().NextDouble() * 10;
                }



            if (typ == "jet")
            {
                speed += 4;
                friction = 0.4f;
                _sprit.Color = new SFML.Graphics.Color(255, 0, 0, 255);
                constantRot = new Random().Next(-15, 15);
            }

            _sprit.Origin = new Vector2f(pos.X, pos.Y);
            _sprit.Rotation = rot += constantRot;

            _sprit.Origin +=
            new Vector2f(
            15 * MathF.Cos((float)(_sprit.Rotation * (Math.PI / 180.0f))),
            15 * MathF.Sin((float)(_sprit.Rotation * (Math.PI / 180.0f))));

        }
        public override void Update(Time elapsed)
        {

            var a = _sprit.Origin;

            //makes sure the particles dont go off screen
            var orgin = _sprit.Origin;
            if (_sprit.Origin.X < -50) _sprit.Origin = new Vector2f(Game.RenderWindow.Size.X, _sprit.Origin.Y);
            if (_sprit.Origin.X > Game.RenderWindow.Size.X) _sprit.Origin = new Vector2f(0, _sprit.Origin.Y);
            if (_sprit.Origin.Y < -50) _sprit.Origin = new Vector2f(_sprit.Origin.X, Game.RenderWindow.Size.Y);
            if (_sprit.Origin.Y > Game.RenderWindow.Size.Y) _sprit.Origin = new Vector2f(_sprit.Origin.X, 0);

            //copies the velocity of the ship
            if (forces != null) foreach (Vector2 e in forces)
                    _sprit.Origin +=
                     new Vector2f
                       ((float)(e.X * Math.Cos((e.Y + 90) * (0.01745329)))
                        , (float)(e.X * Math.Sin((e.Y + 90) * (0.01745329))));

            //gives the velocity from the orginial rotation
            _sprit.Origin += 
                new Vector2f(
                    speed * MathF.Cos((float)(_sprit.Rotation * (Math.PI / 180.0f))),
                    speed * MathF.Sin((float)(_sprit.Rotation * (Math.PI / 180.0f))));

            // makes the parts slow down over time
            if (speed > 0) speed -= friction;
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
                Vector2 c = Conv.ToVect2(_sprit.Origin), d = Ship.GetPos();
                if (cycle == true)
                {
                    _sprit.Rotation = (float)-((MathF.Atan2(c.X - d.X, c.Y - d.Y)+90) *(180 / MathF.PI));
                    if (speed < 13) speed = 13f;
                    friction = 0.1f;
                    foreach (Vector2 e in forces) forces[forces.ToList().IndexOf(e)].X /= 1.01f;
                }
                if (scene.shipEnable == -1) cycle = true;
                //if the main particle returned to ship (called by the line class colliders) kill the stranglers
                if (scene.shipEnable == 1) MakeDead();
            }

            //insure there isnt too many particles already
            if (scene.vaporCount() > 40 &&tag == "partv")
            {
                MakeDead();
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

    }
}

