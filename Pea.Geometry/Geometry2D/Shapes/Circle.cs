using Pea.Geometry.General;

namespace Pea.Geometry2D.Shapes
{
	public class Circle : ShapeBase
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

		public override IShape2D DeepClone()
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

		public override IShape2D DoOffset(double marginWidth)
		{
			return new Circle(this.Center.DeepClone(), this.Radius + marginWidth);
		}
	}
}
