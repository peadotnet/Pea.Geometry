using Pea.Core;
using System;

namespace Pea.Geometry2D.Shapes
{
	public class Circle : ShapeBase, IDeepCloneable<Circle>
	{
		public double Radius { get; set; }

		public Circle(Vector2D center, double radius)
		{
			Center = center;
			Radius = radius;
		}

		public override void DoTransform()
		{
			base.DoTransform();
			Radius = (Points[0] - Center).GetLength();
		}

		public Circle DeepClone()
		{
			return new Circle(Center.DeepClone(), Radius);
		}

		public override void Invalidate()
		{
		}

		public bool Contains(Vector2D point)
		{
			var dx = point.X - Center.X;
			var dy = point.Y - Center.Y;
			return (dx * dx) + (dy * dy) <= Radius * Radius;
		}
	}
}
