using Pea.Geometry2D;
using System;

namespace Pea.Geometry.Geometry2D.Operations
{
	public class VectorHelper
	{
		public static bool OnSegment(Vector2D p, Vector2D q, Vector2D r)
		{
			if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
				q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
				return true;

			return false;
		}

		public static int WhichSide(Vector2D p, Vector2D q, Vector2D r)
		{
            double cross = CrossProduct(p, r, q, p);
            if (cross == 0) return 0;
			return (cross > 0) ? 1 : -1;
		}

        public static double DotProduct(Vector2D p0, Vector2D p1, Vector2D q0, Vector2D q1)
		{
            return (p1.X - p0.X + q1.X - q0.X) * (p1.Y - p0.Y + q1.Y - q0.Y);
		}

        public static double CrossProduct(Vector2D p0, Vector2D p1, Vector2D q0, Vector2D q1)
		{
            return (p1.X - p0.X) * (q1.Y - q0.Y) - (q1.X - q0.X) * (p1.Y - p0.Y);
        }

        public static Boolean DoIntersect(Vector2D p1, Vector2D q1, Vector2D p2, Vector2D q2)
        {
            int o1 = WhichSide(p1, q1, p2);
            int o2 = WhichSide(p1, q1, q2);
            int o3 = WhichSide(p2, q2, p1);
            int o4 = WhichSide(p2, q2, q1);

            if (o1 != o2 && o3 != o4) return true;

            if (o1 == 0 && OnSegment(p1, p2, q1)) return true;
            if (o2 == 0 && OnSegment(p1, q2, q1)) return true;
            if (o3 == 0 && OnSegment(p2, p1, q2)) return true;
            if (o4 == 0 && OnSegment(p2, q1, q2)) return true;

            return false;
        }

        public static bool DoLinesIntersect(Vector2D p0, Vector2D p1, Vector2D q0, Vector2D q1, out Vector2D intersection)
        {
            intersection = null;

            double d = CrossProduct(p0, p1, q0, q1);
            if (d == 0) return false;

            double n_a = (q1.X - q0.X) * (p0.Y - q0.Y) - (p0.X - q0.X) * (q1.Y - q0.Y);
            double n_b = (p1.X - p0.X) * (p0.Y - q0.Y) - (p1.Y - p0.Y) * (p0.X - q0.X);

            double ua = n_a / d;
            double ub = n_b / d;

            double xint = p0.X + (ua * (p1.X - p0.X));
            double yint = p0.Y + (ua * (p1.Y - p0.Y));
            intersection = new Vector2D(xint, yint);

            return true;
        }


    }
}
