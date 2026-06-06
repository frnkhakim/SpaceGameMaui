using System;
using System.Collections.Generic;

namespace SpaceGameMaui
{
    public class GameState
    {
        private const int EnemyRows = 2;
        private const int EnemyColumns = 5;
        private const double EnemyStartX = 0.3;
        private const double EnemyStartY = 1.5;
        private const double EnemyRowSpacing = 0.3;
        private const double EnemyColumnSpacing = 0.3;

        public Ship Ship { get; set; } = new Ship();
        public List<Minion> Minions { get; set; } = new List<Minion>();
        public List<Missile> Missiles { get; set; } = new List<Missile>();

        public bool IsLeftPressed { get; set; }
        public bool IsRightPressed { get; set; }
        public bool IsGameOver { get; private set; }
        public bool IsWin { get; private set; }

        public int Score { get; set; }

        public GameState()
        {
            Reset();
        }

        public void Reset()
        {
            Ship = new Ship();
            Minions.Clear();
            Missiles.Clear();
            IsLeftPressed = false;
            IsRightPressed = false;
            IsGameOver = false;
            IsWin = false;
            Score = 0;

            for (int row = 0; row < EnemyRows; row++)
            {
                for (int col = 0; col < EnemyColumns; col++)
                {
                    Minions.Add(new Minion(
                        EnemyStartX + col * EnemyColumnSpacing,
                        EnemyStartY - row * EnemyRowSpacing
                    ));
                }
            }
        }

        public void Shoot()
        {
            if (IsGameOver)
                return;

            Missiles.Add(new Missile(Ship.X, Ship.Y + 0.1));
        }

        public void Update(double deltaTime)
        {
            if (IsGameOver)
                return;

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

            if (Minions.Count == 0)
            {
                IsGameOver = true;
                IsWin = true;
                return;
            }

            foreach (var minion in Minions)
            {
                if (minion.Y <= Ship.Y + 0.05)
                {
                    IsGameOver = true;
                    IsWin = false;
                    return;
                }
            }
        }
    }
}