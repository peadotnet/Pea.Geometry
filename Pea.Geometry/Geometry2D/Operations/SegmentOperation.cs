using Pea.Geometry2D;
using System;

namespace Pea.Geometry.Geometry2D.Operations
{
	public class SegmentOperation
	{
		public static bool OnSegment(Vector2D p, Vector2D q, Vector2D r)
		{
			if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
				q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
				return true;

			return false;
		}

		public static int Orientation(Vector2D p, Vector2D q, Vector2D r)
		{
			double cross = (q.Y - p.Y) * (r.X - q.X) -
					(q.X - p.X) * (r.Y - q.Y);

			if (cross == 0) return 0;
			return (cross > 0) ? 1 : -1;
		}

        public static Boolean DoIntersect(Vector2D p1, Vector2D q1, Vector2D p2, Vector2D q2)
        {
            int o1 = Orientation(p1, q1, p2);
            int o2 = Orientation(p1, q1, q2);
            int o3 = Orientation(p2, q2, p1);
            int o4 = Orientation(p2, q2, q1);

            if (o1 != o2 && o3 != o4) return true;

            if (o1 == 0 && OnSegment(p1, p2, q1)) return true;
            if (o2 == 0 && OnSegment(p1, q2, q1)) return true;
            if (o3 == 0 && OnSegment(p2, p1, q2)) return true;
            if (o4 == 0 && OnSegment(p2, q1, q2)) return true;

            return false;
        }
    }
}
