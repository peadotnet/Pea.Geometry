using Pea.Core;
using System;
using System.Collections.Generic;

namespace Pea.Geometry2D.Shapes
{
	public class Rectangle : ShapeBase, IDeepCloneable<Rectangle>
	{
		public double Width { get; }
		public double Height { get; }
		public double Area { get; }
		public double Ratio { get; }

		public double Left => Center.X - Width / 2;
		public double Right => Center.X + Width / 2;
		public double Top => Center.Y + Height / 2;
		public double Bottom => Center.Y - Height / 2;

		private List<Vector2D> _points = null;
		public override List<Vector2D> Points
		{
			get
			{
				if (_points == null) _points = CreatePoints();
				return _points;
			}
		}

		private List<Vector2D> CreatePoints()
		{
			var points = new List<Vector2D>(4)
			{
				new Vector2D(Center.X - Width / 2, Center.Y - Height / 2),
				new Vector2D(Center.X + Width / 2, Center.Y - Height / 2),
				new Vector2D(Center.X + Width / 2, Center.Y + Height / 2),
				new Vector2D(Center.X - Width / 2, Center.Y + Height / 2)
			};
			return points;
		}

		public Rectangle(double centerX, double centerY, double width, double height)
		{
			Center = new Vector2D(centerX, centerY);
			Width = width;
			Height = height;
			Area = width * height;
			Ratio = height / width;
		}

		public Rectangle DeepClone()
		{
			return new Rectangle(Center.X, Center.Y, Width, Height);
		}
	}
}
