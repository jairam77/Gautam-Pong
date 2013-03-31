using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pong
{
    public class AI
    {
        int counter;
        Paddle AIPaddle;
        int Difficulty;
        public AI(Paddle P, int D)
        {
            AIPaddle = P;
            counter = 0;
            Difficulty = D;
        }

        public void updatepaddle(Ball ball,Paddle P)
        {
            counter++;
            AIPaddle = P;
            Vector2 PaddleCenter = new Vector2(AIPaddle.Position.X, AIPaddle.Position.Y + (AIPaddle.Height) / 2);
            if (counter != Difficulty)
            {

                if (ball.Location.Y > (PaddleCenter.Y) && ball.Motion.X < 0)
                {
                    AIPaddle.computerinput(10);
                }
                else if (ball.Location.Y < (PaddleCenter.Y) && ball.Motion.X < 0)
                {
                    AIPaddle.computerinput(-10);
                }

                else if (ball.Motion.X < 0 && PaddleCenter.Y < 250)
                {
                    AIPaddle.computerinput(10);
                }
                else if (ball.Motion.X < 0 && PaddleCenter.Y > 250)
                {
                    AIPaddle.computerinput(-10);
                }
            }
            else { counter = 0; }
            
        }
    }
}
