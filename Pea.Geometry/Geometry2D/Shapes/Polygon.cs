using Pea.Geometry.Geometry2D.Operations;
using System;
using System.Collections.Generic;

namespace Pea.Geometry2D.Shapes
{
	public class Polygon : ShapeBase
	{
		private bool? _convex;
		public bool IsConvex
		{ 
			get
			{
				if (!_convex.HasValue) _convex = Convex();
				return _convex.Value;
			}
		}

		private Rectangle _boundingRectangle = null;
		public Rectangle BoundingRectangle
		{
			get
			{
				if (_boundingRectangle == null) _boundingRectangle = CreateBoundingRectangle();
				return _boundingRectangle;
			}
		}

		private Polygon _offset;
		public Polygon Offset
		{
			get
			{
				if (_offset == null) _offset = GetOffset(MarginWidth);
				return _offset;
			}
		}

		private Polygon GetOffset(double marginWidth)
		{
			if (marginWidth == 0) return this.DeepClone() as Polygon;

			var normals = GetSideNormals(Points);
			var newSides = CreateSlidedSides(Points, normals);
			var polygon = new Polygon(newSides);
			return polygon;
		}

		internal Polygon(List<Vector2D> points)
		{
			int count = points.Count;
			if (count < 3) throw new ArgumentException($"The polygon has to have at least 3 points!");

			double sumX = 0;
			double sumY = 0;

			Points = new List<Vector2D>();

			for (int i=0; i< points.Count; i++)
			{
				Points.Add(new Vector2D(points[i].X, points[i].Y));
				sumX += points[i].X;
				sumY += points[i].Y;
			}

			Center = new Vector2D(sumX / count, sumY / count);

			var first = Points[0];
			var last = Points[count - 1];
			if (first.X != last.X || last.Y != last.Y)
			{
				Points.Add(new Vector2D(first.X, first.Y));
			}
		}

		public Rectangle CreateBoundingRectangle()
		{
			double xMin = double.MaxValue;
			double xMax = double.MinValue;
			double yMin = double.MaxValue;
			double yMax = double.MinValue;

			for (int i = 0; i < Points.Count; i++)
			{
				if (Points[i].X < xMin) xMin = Points[i].X;
				if (Points[i].X > xMax) xMax = Points[i].X;
				if (Points[i].Y < yMin) yMin = Points[i].Y;
				if (Points[i].Y > yMax) yMax = Points[i].Y;
			}

			return new Rectangle((xMin + xMax) / 2, (yMin + yMax) / 2, xMax - xMin, yMax - yMin);
		}

		public double GetArea()
		{
			return Math.Abs(SignedArea());
		}

		public bool IsCounterClockwise()
		{
			return (SignedArea() < 0);
		}

		protected double SignedArea()
		{
			double area = 0;
			int previous = 0;

			for (int current = 1; current < Points.Count; current++)
			{
				area += Points[previous].X * Points[current].Y - Points[previous].Y * Points[current].X;
			}
			return area / 2;
		}

		protected bool Convex()
		{
			bool gotLeft = false;
			bool gotRight = false;

			for (int a = 0; a < Points.Count; a++)
			{

				var left = IsLeftTurn(a);

				if (left) gotLeft = true;
				if (!left) gotRight = true;

				if (gotLeft && gotRight) return false;
			}

			return true;
		}

		protected bool IsLeftTurn(int index)
		{
			var count = Points.Count;
			return (CrossProductLength(Points[(index - 1) % count], Points[index], Points[(index + 1) % count]) < 0);
		}

		protected List<Vector2D> GetSideNormals(List<Vector2D> points)
		{
			var normals = new List<Vector2D>();
			for (int i = 0; i < points.Count - 1; i++)
			{
				var sideVector = points[i + 1] - points[i];
				normals.Add(sideVector.GetNormal());
			}
			return normals;
		}

		protected List<Vector2D> CreateSlidedSides(List<Vector2D> points, List<Vector2D> normals)
		{
			var newSides = new List<Vector2D>();
			var previousSlide = normals[0] * MarginWidth;
			for (int i = 1; i < points.Count; i++)
			{
				var slide = normals[i % normals.Count] * MarginWidth;
				if (IsLeftTurn(i))
				{
					var corner = (slide + previousSlide);
					corner = corner * MarginWidth * (1 / corner.GetLength());
					newSides.Add(points[i] + previousSlide);
					newSides.Add(points[i] + corner);
					newSides.Add(points[i] + slide);
				}
				else
				{
					var p0 = points[i - 1] + previousSlide;
					var p1 = points[i] + previousSlide;
					var p2 = points[i] + slide;
					var p3 = points[(i +1) % (Points.Count - 1)] + slide;
					
					if (Geometry.Geometry2D.Operations.VectorHelper.DoLinesIntersect(p0, p1, p2, p3, out Vector2D intersection))
					{
						newSides.Add(intersection);
					}
					else
					{
						bool whatTheF = false;
					}
				}
				previousSlide = slide;
			}

			return newSides;
		}

		public double DistanceOfPoint(Vector2D point)
		{
			double minDistance = double.MaxValue;
			for(int i=0; i < Points.Count - 1; i++)
			{
				var distance = VectorHelper.LinePointDistance(Points[i], Points[i + 1], point);
				if (distance < minDistance) minDistance = distance;
			}
			return minDistance;
		}

		public override void Invalidate()
		{
			_boundingRectangle = null;
			_offset = null;
			//_convex not changes during transformations
		}

		public override IShape2D DoOffset(double marginWidth)
		{
			return DeepClone();
		}

		public override IShape2D DeepClone()
		{
			return new Polygon(Points);
		}
	}
}
