/* 
 * Smallest enclosing circle - Library (C#)
 * 
 * Copyright (c) 2020 Project Nayuki
 * https://www.nayuki.io/page/smallest-enclosing-circle
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public License
 * along with this program (see COPYING.txt and COPYING.LESSER.txt).
 * If not, see <http://www.gnu.org/licenses/>.
 */

using Pea.Geometry2D;
using Pea.Geometry2D.Shapes;
using System;
using System.Collections.Generic;


public sealed class BoundingCircleFinder
{

	/* 
	 * Returns the smallest circle that encloses all the given points. Runs in expected O(n) time, randomized.
	 * Note: If 0 points are given, a circle of radius -1 is returned. If 1 point is given, a circle of radius 0 is returned.
	 */
	// Initially: No boundary points known
	public static Circle MakeCircle(IList<Vector2D> points)
	{
		// Clone list to preserve the caller's data, do Durstenfeld shuffle
		List<Vector2D> shuffled = new List<Vector2D>(points);
		Random rand = new Random();
		for (int i = shuffled.Count - 1; i > 0; i--)
		{
			int j = rand.Next(i + 1);
			Vector2D temp = shuffled[i];
			shuffled[i] = shuffled[j];
			shuffled[j] = temp;
		}

		// Progressively add points to circle or recompute circle
		Circle c = Invalid;
		for (int i = 0; i < shuffled.Count; i++)
		{
			Vector2D p = shuffled[i];
			if (c.Radius < 0 || !c.Contains(p))
				c = MakeCircleOnePoint(shuffled.GetRange(0, i + 1), p);
		}
		return c;
	}


	// One boundary point known
	private static Circle MakeCircleOnePoint(List<Vector2D> points, Vector2D p)
	{
		Circle c = new Circle(p, 0);
		for (int i = 0; i < points.Count; i++)
		{
			Vector2D q = points[i];
			if (!c.Contains(q))
			{
				if (c.Radius == 0)
					c = MakeDiameter(p, q);
				else
					c = MakeCircleTwoPoints(points.GetRange(0, i + 1), p, q);
			}
		}
		return c;
	}

	private static Circle Invalid = new Circle(new Vector2D(0, 0), -1);
	// Two boundary points known
	private static Circle MakeCircleTwoPoints(List<Vector2D> points, Vector2D p, Vector2D q)
	{
		Circle circ = MakeDiameter(p, q);
		Circle left = Invalid;
		Circle right = Invalid;

		// For each point not in the two-point circle
		Vector2D pq = q - p;
		foreach (Vector2D r in points)
		{
			if (circ.Contains(r))
				continue;

			// Form a circumcircle and classify it on left or right side
			double cross = Cross(r - p, pq);
			Circle c = MakeCircumcircle(p, q, r);
			if (c.Radius < 0)
				continue;
			else if (cross > 0 && (left.Radius < 0 || Cross(c.Center - p, pq) > Cross(left.Center - p, pq)))
				left = c;
			else if (cross < 0 && (right.Radius < 0 || Cross(c.Center - p, pq) < Cross(right.Center - p, pq)))
				right = c;
		}

		// Select which circle to return
		if (left.Radius < 0 && right.Radius < 0)
			return circ;
		else if (left.Radius < 0)
			return right;
		else if (right.Radius < 0)
			return left;
		else
			return left.Radius <= right.Radius ? left : right;
	}


	public static Circle MakeDiameter(Vector2D a, Vector2D b)
	{
		Vector2D c = new Vector2D((a.X + b.X) / 2, (a.Y + b.Y) / 2);
		return new Circle(c, Math.Max(Distance(a, c), Distance(b, c)));
	}


	public static Circle MakeCircumcircle(Vector2D a, Vector2D b, Vector2D c)
	{
		// Mathematical algorithm from Wikipedia: Circumscribed circle
		double ox = (Math.Min(Math.Min(a.X, b.X), c.X) + Math.Max(Math.Max(a.X, b.X), c.X)) / 2;
		double oy = (Math.Min(Math.Min(a.Y, b.Y), c.Y) + Math.Max(Math.Max(a.Y, b.Y), c.Y)) / 2;
		double ax = a.X - ox, ay = a.X - oy;
		double bx = b.X - ox, by = b.X - oy;
		double cx = c.X - ox, cy = c.Y - oy;
		double d = (ax * (by - cy) + bx * (cy - ay) + cx * (ay - by)) * 2;
		if (d == 0)
			return Invalid;
		double x = ((ax * ax + ay * ay) * (by - cy) + (bx * bx + by * by) * (cy - ay) + (cx * cx + cy * cy) * (ay - by)) / d;
		double y = ((ax * ax + ay * ay) * (cx - bx) + (bx * bx + by * by) * (ax - cx) + (cx * cx + cy * cy) * (bx - ax)) / d;
		Vector2D p = new Vector2D(ox + x, oy + y);
		double r = Math.Max(Math.Max(Distance(a, p), Distance(b, p)), Distance(c, p));
		return new Circle(p, r);
	}

	public static double Distance(Vector2D p, Vector2D q)
	{
		double dx = q.X - p.X;
		double dy = q.Y - p.Y;
		return Math.Sqrt(dx * dx + dy * dy);
	}

	// Signed area / determinant thing
	public static double Cross(Vector2D p, Vector2D q)
	{
		return q.X * p.Y - q.Y * p.X;
	}
}



//public struct Circle
//{

//	public static readonly Circle INVALID = new Circle(new Point(0, 0), -1);

//	private const double MULTIPLICATIVE_EPSILON = 1 + 1e-14;


//	public Point c;   // Center
//	public double r;  // Radius


//	public Circle(Point c, double r)
//	{
//		this.c = c;
//		this.r = r;
//	}


//	public bool Contains(Point p)
//	{
//		return c.Distance(p) <= r * MULTIPLICATIVE_EPSILON;
//	}


//	public bool Contains(ICollection<Point> ps)
//	{
//		foreach (Point p in ps)
//		{
//			if (!Contains(p))
//				return false;
//		}
//		return true;
//	}

//}



//public struct Point
//{

//	public double x;
//	public double y;


//	public Point(double x, double y)
//	{
//		this.x = x;
//		this.y = y;
//	}


//	public Point Subtract(Point p)
//	{
//		return new Point(x - p.x, y - p.y);
//	}




//	// Signed area / determinant thing
//	public double Cross(Point p)
//	{
//		return x * p.y - y * p.x;
//	}

//}