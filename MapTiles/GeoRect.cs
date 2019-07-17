using System;

namespace iDispatch.MapTiles
{
    // TODO Document me
    public sealed class GeoRect<Point> where Point: IGeoPoint
    {
        public double North { get; }
        public double East { get; }
        public double South { get; }
        public double West { get; }
        public Point FirstPoint { get; }
        public Point SecondPoint { get; }
        public double Width { get
            {
                return Math.Abs(East - West);
            }
        }
        public double Height { get
            {
                return Math.Abs(North - South);
            }
        }
        public GeoRect(Point p1, Point p2)
        {
            FirstPoint = p1;
            SecondPoint = p2;
            if (p1.X < p2.X)
            {
                West = p1.X;
                East = p2.X;
            } else
            {
                West = p2.X;
                East = p1.X;
            }

            if (p1.Y < p2.Y)
            {
                South = p1.Y;
                North = p2.Y;
            } else
            {
                South = p2.Y;
                North = p1.Y;
            }
        }

        public bool Contains(Point p)
        {
            return West <= p.X && East >= p.X && South <= p.Y && North >= p.Y;
        }
    }
}
