using Pea.Core;
using System;

namespace Pea.Geometry2D.Shapes
{
	public class Circle : ShapeBase, IDeepCloneable<Circle>
	{
		public double Radius { get; set; }

		public Circle(Vector2D center, double radius, int polygonSides = 8)
		{
			if (polygonSides < 1) throw new ArgumentException(nameof(polygonSides));

			Center = center;
			Radius = radius;
			for(int p = 0;p < polygonSides; p++)
			{
				Points.Add(new Vector2D(2 * p * Math.PI / polygonSides) * radius);
			}
		}

		public override void DoTransform()
		{
			base.DoTransform();
			Radius = (Points[0] - Center).GetLength();
		}

		public Circle DeepClone()
		{
			return new Circle(Center.DeepClone(), Radius, Points.Count);
		}
	}
}
