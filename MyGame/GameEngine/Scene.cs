﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameEngine
{
    // The Scene manages all the GameObjects currently in the game.
    public class Scene
    {
        // This holds our game objects.
        readonly List<GameObject> _gameObjects = new List<GameObject>();
        public bool pause = true;
        public List<GameObject> GetObjs()
        {
            return _gameObjects;
        }
        // Puts a GameObject into the scene.
        public void AddGameObject(GameObject gameObject)
        {
            // This adds the game object onto the back (the end) of the list of game objects.
            _gameObjects.Add(gameObject);
        }

        // Called by the Game instance once per frame.
        public void Update(Time time)
        {
            // Handle any keyboard, mouse events, etc. for our game window.
            Game.RenderWindow.DispatchEvents();
            if (Keyboard.IsKeyPressed(Keyboard.Key.Y)) pause = true;
            if (Keyboard.IsKeyPressed(Keyboard.Key.U)) pause = false;
            HandleCollisions();
            if (pause)
            {
                // Clear the window.
                Game.RenderWindow.Clear();

                // Go through our normal sequence of game loop stuff.


                HandleCollisions();

                UpdateGameObjects(time);
                RemoveDeadGameObjects();
                DrawGameObjects();

                // Draw the window as updated by the game objects.
                Game.RenderWindow.Display();
            }
        }

        // This method lets game objects respond to collisions.
        private void HandleCollisions()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                GameObject gameObject = _gameObjects[i];

                // Only check objects that ask to be checked.
                //if (!gameObject.IsCollisionCheckEnabled()) continue;

                FloatRect collisionRect = gameObject.GetCollisionRect();

                // Don't bother checking if this game object has a collision rectangle with no area.
                if (collisionRect.Height == 0 || collisionRect.Width == 0) continue;

                // See if this game object is colliding with any other game object.
                for (int j = 0; j < _gameObjects.Count; j++)
                {
                    var otherGameObject = _gameObjects[j];

                    // Don't check an object colliding with itself.
                    if (gameObject == otherGameObject) continue;

                    if (gameObject.IsDead()) return;

                    // When we find a collision, invoke the collision handler for both objects.
                    if (collisionRect.Intersects(otherGameObject.GetCollisionRect()))
                    {
                        gameObject.HandleCollision(otherGameObject);
                        otherGameObject.HandleCollision(gameObject);
                    }
                }
            }
        }

        // This function calls update on each of our game objects.
        private void UpdateGameObjects(Time time)
        {
            //_gameObjects.ForEach(x => { x.Update(time); });
            for (int i = 0; i < _gameObjects.Count; i++) _gameObjects[i].Update(time);
        }

        // This function calls draw on each of our game objects.
        private void DrawGameObjects()
        {
            foreach (GameObject e in _gameObjects) e.Draw();
        }

        // This function removes objects that indicate they are dead from the scene.
        private void RemoveDeadGameObjects()
        {
            // This is a "lambda", which is a fancy name for an anonymous function.
            // It's "anonymous" because it doesn't have a name. We've declared a variable
            // named "isDead", and that variable can be used to call the function, but the
            // function itself is nameless.
            Predicate<GameObject> isDead = gameObject => gameObject.IsDead();

            // Here we use the lambda declared above by passing it to the standard RemoveAll
            // method on List<T>, which calls our lambda once for each element in
            // gameObjects. If our lambda returns true, that game object ends up being
            // removed from our list.
            _gameObjects.RemoveAll(isDead);
        }
    }
}