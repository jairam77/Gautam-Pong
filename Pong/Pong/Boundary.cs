using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pong
{
    public class Boundary
    {
        public Rectangle boundary;
        public Rectangle CollisionWall { get { return boundary; } }
        int x, y, length, breadth;
        public Vector2 StartPoint { get{return new Vector2(x,y);} }
        public int Length { get { return length; } }
        public int Thickness { get { return breadth; } }
        public Boundary(int X, int Y, int Length, int Breadth){
            x = X;
            y = Y;
            length = Length;
            breadth = Breadth;
            boundary = new Rectangle(x, y, length, breadth);
            }
    }
    
}
