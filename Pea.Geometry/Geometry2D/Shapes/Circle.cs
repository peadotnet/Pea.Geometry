namespace Pea.Geometry2D.Shapes
{
	public class Circle : ShapeBase
	{
		private double _radius;
		public double Radius 
		{
			get => _radius;
			set
			{
				_radius = value;
				Invalidate();
			}
		}

		public Circle(Vector2D center, double radius)
		{
			Center = center;
			Radius = radius;
		}

		public override void DoTransform()
		{
			var transformation = ComposeTransformations();
			Center = (Vector2D)transformation.Apply(Center);
			ResetTransformations();
		}

		public override IShape2D DeepClone()
		{
			return new Circle(Center.DeepClone(), Radius);
		}

		public override void Invalidate()
		{
			_boundingRectangle = null;
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

		protected override Rectangle CreateBoundingRectangle()
		{
			return new Rectangle(Center.X, Center.Y, 2 * Radius, 2 * Radius);
		}
	}
}
