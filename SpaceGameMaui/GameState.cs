using System;
using System.Collections.Generic;

namespace SpaceGameMaui
{
    public class GameState
    {
        public Ship Ship { get; set; } = new Ship();
        public List<Minion> Minions { get; set; } = new List<Minion>();
        public List<Missile> Missiles { get; set; } = new List<Missile>();

        public bool IsLeftPressed { get; set; }
        public bool IsRightPressed { get; set; }

        public int Score { get; set; }

        public GameState()
        {
            // Spawn enemies
            for (int i = 0; i < 5; i++)
                Minions.Add(new Minion(0.3 + i * 0.3, 1.5));

            for (int i = 0; i < 5; i++)
                Minions.Add(new Minion(0.3 + i * 0.3, 1.2));
        }

        public void Shoot()
        {
            Missiles.Add(new Missile(Ship.X, Ship.Y + 0.1));
        }

        public void Update(double deltaTime)
        {
            // Cap large frame gaps to keep gameplay stable after app stalls.
            deltaTime = Math.Min(deltaTime, 0.05);

            Ship.Update(deltaTime, IsLeftPressed, IsRightPressed);

            // Move enemies
            foreach (var m in Minions)
                m.Move(deltaTime);

            // Move missiles
            for (int i = Missiles.Count - 1; i >= 0; i--)
            {
                Missiles[i].Update(deltaTime);

                if (Missiles[i].IsOffScreen())
                    Missiles.RemoveAt(i);
            }

            // Collision detection
            for (int i = Minions.Count - 1; i >= 0; i--)
            {
                for (int j = Missiles.Count - 1; j >= 0; j--)
                {
                    double dx = Minions[i].X - Missiles[j].X;
                    double dy = Minions[i].Y - Missiles[j].Y;

                    double distance = Math.Sqrt(dx * dx + dy * dy);

                    if (distance < 0.1)
                    {
                        Minions.RemoveAt(i);
                        Missiles.RemoveAt(j);
                        Score++;
                        break;
                    }
                }
            }
        }
    }
}