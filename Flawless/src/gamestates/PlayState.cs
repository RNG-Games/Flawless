﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using _Flawless.actors;

namespace _Flawless.gamestates
{
    class PlayState : GameState
    {
        //private Player _player;
        private List<IActable> actors = new List<IActable>();
        private float escPause = 2f;
        private float time = 0;

        public PlayState() : this("")
        {
            
        }

        public PlayState(string StagePath)
        {
            actors.Add(Resources.GetPlayer());
        }

        public override void Draw(RenderWindow _window)
        {
            foreach (var act in actors.Where(a => a.StartTime() <= time))
            {
                act.Draw(_window);
            }
          
        }

        public override void Update(float _deltaTime)
        {
            escPause -= _deltaTime;
            time += _deltaTime;

            actors = actors.Where(n => !n.IsExpired()).ToList();
            foreach (var act in actors.Where(a => a.StartTime() <= time))
            {
                act.Update(_deltaTime);
            }
            if (escPause <= 0f && Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                Program.states.Push(new PauseState());
                escPause = 2f;
            }
        }
    }
}
