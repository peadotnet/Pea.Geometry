using Pea.Geometry.General;
using System;
using System.Collections.Generic;

namespace Pea.Geometry2D.Shapes
{
	public class Rectangle : ShapeBase
	{
		public double Width { get; }
		public double Height { get; }
		public double Area { get; }
		public double Ratio { get; }

		private double _left = double.NaN;
		public double Left
		{
			get
			{
				if (double.IsNaN(_left)) _left = Center.X - Width / 2;
				return _left;
			}
		}

		private double _right = double.NaN;
		public double Right
		{
			get
			{
				if (double.IsNaN(_right)) _right = Center.X + Width / 2;
				return _right;
			}
		}

		private double _top = double.NaN;
		public double Top
		{
			get
			{
				if (double.IsNaN(_top)) _top = Center.Y + Height / 2;
				return _top;
			}
		}

		private double _bottom = double.NaN;
		public double Bottom
		{
			get
			{
				if (double.IsNaN(_bottom)) _bottom = Center.Y - Height / 2;
				return _bottom;
			}
		}

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
				new Vector2D(Center.X - Width / 2, Center.Y + Height / 2),
				new Vector2D(Center.X - Width / 2, Center.Y - Height / 2)
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

		public override IShape2D DeepClone()
		{
			return new Rectangle(Center.X, Center.Y, Width, Height);
		}

		public override void Invalidate()
		{
			_left = double.NaN;
			_right = double.NaN;
			_top = double.NaN;
			_bottom = double.NaN;
			_points = null;
		}

		public override IShape2D DoOffset(double marginWidth)
		{
			return new Rectangle(this.Center.X, this.Center.Y, this.Width + 2 * marginWidth, this.Height + 2 * marginWidth);
		}
	}
}
